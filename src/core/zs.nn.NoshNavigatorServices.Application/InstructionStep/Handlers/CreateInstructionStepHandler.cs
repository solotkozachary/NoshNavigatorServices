using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using zs.nn.NoshNavigatorServices.Application.Entity.Commands;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Handlers;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Commands;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep;
using zs.nn.NoshNavigatorServices.Application.Recipe.Queries;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Handlers
{
    /// <summary>
    /// The handler responsible for creating a new ingredient for a recipe.
    /// </summary>
    public class CreateInstructionStepHandler : IRequestHandler<CreateInstructionStepCommand, Guid>
    {
        private readonly ILogger<CreateInstructionStepHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IInstructionStepPersistenceCommands _commands;

        public CreateInstructionStepHandler(
            ILogger<CreateInstructionStepHandler> logger,
            IMediator mediator,
            IInstructionStepPersistenceCommands commands
            )
        {
            _logger = logger;
            _mediator = mediator;
            _commands = commands;
        }

        public async Task<Guid> Handle(CreateInstructionStepCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateInstructionStepHandler - RecipeId:{RecipeId}", request.RecipeId);

            await ValidateRecipe(request.RecipeId, cancellationToken);





            _logger.LogTrace("Exit CreateInstructionStepHandler - RecipeId:{RecipeId}", request.RecipeId);

            throw new NotImplementedException();
        }

        private async Task ValidateRecipe(Guid recipeId, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter ValidateRecipe - RecipeId:{RecipeId}", recipeId);

            var existing = await _mediator.Send(new GetRecipeByIdQuery { Id = recipeId, MustExist = true });

            _logger.LogTrace("Exit ValidateRecipe - RecipeId:{RecipeId}", recipeId);
        }

        private async Task<Domain.Entity.Recipe.InstructionStep> BuildInstructionStep(CreateInstructionStepCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter BuildInstructionStep - RecipeId:{RecipeId}", request.RecipeId);

            var entity = await _mediator.Send(new InitializeEntityCommand<Domain.Entity.Recipe.InstructionStep>()
            { EntityCreationSourceId = request.RecipeId.ToString() });

            entity.SequenceNumber = request.SequenceNumber;
            entity.Description = request.Description;
            entity.RecipeId = request.RecipeId;

            _logger.LogTrace("Exit BuildInstructionStep - RecipeId:{RecipeId}   InstructionStepId:{InstructionStepId}", entity.RecipeId, entity.Id);

            return entity;
        }

        private ICollection<Tuple<Domain.Entity.Recipe.InstructionStep, bool>> AddAndUpdateInstructionSteps(
            ICollection<Domain.Entity.Recipe.InstructionStep> currentSteps,
            Domain.Entity.Recipe.InstructionStep newStep)
        {
            var result = new List<Tuple<Domain.Entity.Recipe.InstructionStep, bool>>();

            // If newStep's SequenceNumber is greater than all current steps or current steps are empty, 
            // add to the end and return.
            if (!currentSteps.Any() || newStep.SequenceNumber > currentSteps.Max(s => s.SequenceNumber))
            {
                result.AddRange(currentSteps.Select(s => new Tuple<Domain.Entity.Recipe.InstructionStep, bool>(s, false))); // No sequence numbers updated
                result.Add(new Tuple<Domain.Entity.Recipe.InstructionStep, bool>(newStep, false));
                return result;
            }

            // Otherwise, find where to insert the new step and adjust subsequent steps.
            var stepsList = currentSteps.OrderBy(s => s.SequenceNumber).ToList();

            // Identify the index where the new step should be inserted.
            int insertAtIndex = stepsList.FindIndex(s => s.SequenceNumber >= newStep.SequenceNumber);

            // Adjust sequence numbers for all subsequent steps.
            for (int i = insertAtIndex; i < stepsList.Count; i++)
            {
                stepsList[i].SequenceNumber++;
                result.Add(new Tuple<Domain.Entity.Recipe.InstructionStep, bool>(stepsList[i], true)); // Sequence number updated
            }

            // Insert the new step.
            stepsList.Insert(insertAtIndex, newStep);
            result.Insert(insertAtIndex, new Tuple<Domain.Entity.Recipe.InstructionStep, bool>(newStep, false)); // New step added, so sequence number not "updated"

            // Add the untouched steps to the result.
            for (int i = 0; i < insertAtIndex; i++)
            {
                result.Add(new Tuple<Domain.Entity.Recipe.InstructionStep, bool>(stepsList[i], false)); // Sequence numbers not updated
            }

            return result;
        }
    }
}
