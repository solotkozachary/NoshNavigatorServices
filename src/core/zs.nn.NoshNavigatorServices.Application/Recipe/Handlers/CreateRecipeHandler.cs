using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using zs.nn.NoshNavigatorServices.Application.Entity.Commands;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Commands;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Commands;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Recipe;
using zs.nn.NoshNavigatorServices.Application.Recipe.Commands;
using zs.nn.NoshNavigatorServices.Events;
using zs.nn.NoshNavigatorServices.Events.Recipe;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Handlers
{
    /// <summary>
    /// The handler responsible for creating a new recipe.
    /// </summary>
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, Guid>
    {
        private readonly ILogger<CreateRecipeHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IRecipePersistenceCommands _commands;
        private readonly INoshNavigatorEventService<RecipeCreatedEvent, Domain.Entity.Recipe.Recipe> _eventService;

        public CreateRecipeHandler(
            ILogger<CreateRecipeHandler> logger,
            IMediator mediator,
            IRecipePersistenceCommands commands,
            INoshNavigatorEventService<RecipeCreatedEvent, Domain.Entity.Recipe.Recipe> eventService
            )
        {
            _logger = logger;
            _mediator = mediator;
            _commands = commands;
            _eventService = eventService;
        }

        public async Task<Guid> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateRecipeHandler");

            var entity = await BuildRecipe(request, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogTrace("Canceled CreateRecipeHandler");

                cancellationToken.ThrowIfCancellationRequested();
            }

            await _commands.Create(entity, cancellationToken);

            await _eventService.PublishNoshNavigatorEvent(new RecipeCreatedEvent(entity), cancellationToken);

            _logger.LogInformation("Ingredient created - RecipeId:{RecipeId}", entity.Id);

            await CreateRecipeMembers(entity.Id, request, cancellationToken);

            _logger.LogTrace("Exit CreateRecipeHandler - RecipeId:{RecipeId}", entity.Id);

            return entity.Id;
        }

        private async Task<Domain.Entity.Recipe.Recipe> BuildRecipe(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter BuildRecipe");

            var entity = await _mediator.Send(new InitializeEntityCommand<Domain.Entity.Recipe.Recipe>()
                { EntityCreationSourceId = nameof(CreateRecipeHandler) }, cancellationToken);

            entity.Name = request.Name;
            entity.Description = request.Description;

            _logger.LogTrace("Exit BuildRecipe - RecipeId:{RecipeId}", entity.Id);

            return entity;
        }

        private async Task CreateRecipeMembers(Guid id, CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateRecipeMembers - RecipeId:{RecipeId}", id);

            await Task.WhenAll(new List<Task>
            {
                CreateRecipeInstructionSteps(id, request.InstructionSteps, cancellationToken),

                CreateRecipeIngredients(id, request.Ingredients, cancellationToken)
            });

            _logger.LogTrace("Exit CreateRecipeMembers - RecipeId:{RecipeId}", id);
        }

        private async Task CreateRecipeIngredients(Guid id, ICollection<RecipeIngredient> ingredients, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateRecipeIngredients - RecipeId:{RecipeId}", id);

            await Task.WhenAll(ingredients.Select(x => CreateRecipeIngredient(id, x, cancellationToken)).ToList());

            _logger.LogTrace("Exit CreateRecipeIngredients - RecipeId:{RecipeId}", id);
        }

        private async Task CreateRecipeIngredient(Guid id, RecipeIngredient ingredient, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateRecipeIngredient - RecipeId:{RecipeId}", id);

            var ingredientId = await _mediator.Send(new CreateIngredientCommand 
                { Name = ingredient.Name, Description = ingredient.Description, Amount = ingredient.Amount, RecipeId = id }, cancellationToken);

            _logger.LogTrace("Exit CreateRecipeIngredient - RecipeId:{RecipeId}   IngredientId:{IngredientId}", id, ingredientId);
        }

        private async Task CreateRecipeInstructionSteps(Guid id, ICollection<RecipeInstructionStep> instructionSteps, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateRecipeInstructionSteps - RecipeId:{RecipeId}", id);

            foreach (var instructionStep in instructionSteps) // Allow auto-updating of sequence correct any invalid sequence numbers.
            {
                await CreateRecipeInstructionStep(id, instructionStep, cancellationToken);
            }

            await Task.WhenAll(instructionSteps.Select(x => CreateRecipeInstructionStep(id, x, cancellationToken)).ToList());

            _logger.LogTrace("Exit CreateRecipeInstructionSteps - RecipeId:{RecipeId}", id);
        }

        private async Task CreateRecipeInstructionStep(Guid id, RecipeInstructionStep instructionStep, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateRecipeInstructionStep - RecipeId:{RecipeId}", id);

            var instructionStepId = await _mediator.Send(new CreateInstructionStepCommand 
                { SequenceNumber = instructionStep.SequenceNumber, Description = instructionStep.Description, RecipeId = id }, cancellationToken);

            _logger.LogTrace("Exit CreateRecipeInstructionStep - RecipeId:{RecipeId}   InstructionStepId:{InstructionStepId}", id, instructionStepId);
        }
    }
}
