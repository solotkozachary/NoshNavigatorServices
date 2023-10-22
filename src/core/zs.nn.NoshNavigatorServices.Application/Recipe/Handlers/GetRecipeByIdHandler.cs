using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using zs.nn.NoshNavigatorServices.Application.Enumerations;
using zs.nn.NoshNavigatorServices.Application.Exceptions;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Recipe;
using zs.nn.NoshNavigatorServices.Application.Recipe.Queries;
using System.Collections.Generic;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Queries;
using System;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Queries;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Handlers
{
    /// <summary>
    /// The handler responsible for retrieving a recipe by Id.
    /// </summary>
    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdQuery, Domain.Entity.Recipe.Recipe>
    {
        private readonly ILogger<GetRecipeByIdHandler> _logger;
        private readonly IRecipePersistenceQueries _queries;
        private readonly IMediator _mediator;

        public GetRecipeByIdHandler(
            ILogger<GetRecipeByIdHandler> logger,
            IRecipePersistenceQueries queries,
            IMediator mediator
            )
        {
            _logger = logger;
            _queries = queries;
            _mediator = mediator;
        }

        public async Task<Domain.Entity.Recipe.Recipe> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetRecipeByIdHandler - RecipeId:{RecipeId}", request.Id);

            var entity = await _queries.GetById(request.Id, cancellationToken);

            if (request.MustExist && entity == null)
            {
                var errMsg = "No Recipe exists for provided id - RecipeId:{RecipeId}";

                _logger.LogError(errMsg, request.Id);

                _logger.LogTrace("Exit GetRecipeByIdHandler - RecipeId:{RecipeId}", request.Id);

                throw new NoshNavigatorException(ApplicationConcern.Entity, ApplicationRules.MustExist, errMsg.Replace("{RecipeId}", request.Id.ToString()));
            }

            if (entity != null)
            {
                entity.Ingredients = await GetIngredients(entity.Id, cancellationToken);
                entity.InstructionSteps = await GetInstructionSteps(entity.Id, cancellationToken);
            }

            _logger.LogTrace("Exit GetRecipeByIdHandler - RecipeId:{RecipeId}", request.Id);

            return entity;
        }

        private async Task<ICollection<Domain.Entity.Recipe.Ingredient>> GetIngredients(Guid recipeId, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetIngredients - RecipeId:{RecipeId}", recipeId);

            var resuts = await _mediator.Send(new GetIngredientsByRecipeIdQuery { RecipeId = recipeId }, cancellationToken);

            _logger.LogTrace("Exit GetIngredients - RecipeId:{RecipeId}", recipeId);

            return resuts;
        }

        private async Task<ICollection<Domain.Entity.Recipe.InstructionStep>> GetInstructionSteps(Guid recipeId, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetInstructionSteps - RecipeId:{RecipeId}", recipeId);

            var resuts = await _mediator.Send(new GetInstructionStepsByRecipeIdQuery { RecipeId = recipeId }, cancellationToken);

            _logger.LogTrace("Exit GetInstructionSteps - RecipeId:{RecipeId}", recipeId);

            return resuts;
        }
    }
}
