using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using zs.nn.NoshNavigatorServices.Application.Entity.Commands;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Commands;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient;
using zs.nn.NoshNavigatorServices.Application.Recipe.Queries;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Handlers
{
    /// <summary>
    /// The handler responsible for creating a new ingredient for a recipe.
    /// </summary>
    public class CreateIngredientHandler : IRequestHandler<CreateIngredientCommand, Guid>
    {
        private readonly ILogger<CreateIngredientHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IIngredientPersistenceCommands _commands;

        public CreateIngredientHandler(
            ILogger<CreateIngredientHandler> logger,
            IMediator mediator,
            IIngredientPersistenceCommands commands
            )
        {
            _logger = logger;
            _mediator = mediator;
            _commands = commands;
        }

        public async Task<Guid> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateIngredientHandler - RecipeId:{RecipeId}", request.RecipeId);

            await ValidateRecipe(request.RecipeId, cancellationToken);

            var entity = await BuildIngredient(request, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogTrace("Canceled CreateIngredientHandler");

                cancellationToken.ThrowIfCancellationRequested();
            }

            await _commands.Create(entity, cancellationToken);

            _logger.LogInformation("Ingredient created - RecipeId:{RecipeId}   IngredientId:{IngredientId}", entity.RecipeId, entity.Id);

            _logger.LogTrace("Exit CreateIngredientHandler - RecipeId:{RecipeId}", request.RecipeId);

            return entity.Id;
        }

        private async Task ValidateRecipe(Guid recipeId, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter ValidateRecipe - RecipeId:{RecipeId}", recipeId);

            _ = await _mediator.Send(new GetRecipeByIdQuery { Id = recipeId, MustExist = true }, cancellationToken);

            _logger.LogTrace("Exit ValidateRecipe - RecipeId:{RecipeId}", recipeId);
        }

        private async Task<Domain.Entity.Recipe.Ingredient> BuildIngredient(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter BuildIngredient - RecipeId:{RecipeId}", request.RecipeId);

            var entity = await _mediator.Send(new InitializeEntityCommand<Domain.Entity.Recipe.Ingredient>()
            { EntityCreationSourceId = request.RecipeId.ToString() }, cancellationToken);

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Amount = request.Amount;
            entity.RecipeId = request.RecipeId;

            _logger.LogTrace("Exit BuildIngredient - RecipeId:{RecipeId}   IngredientId:{IngredientId}", entity.RecipeId, entity.Id);

            return entity;
        }
    }
}
