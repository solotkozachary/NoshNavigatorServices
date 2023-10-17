using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Queries;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Handlers
{
    /// <summary>
    /// The handler responsible for retrieving a list of ingredients by recipe id.
    /// </summary>
    public class GetIngredientsByRecipeIdHandler : IRequestHandler<GetIngredientsByRecipeIdQuery, ICollection<Domain.Entity.Recipe.Ingredient>>
    {
        private readonly ILogger<GetIngredientsByRecipeIdHandler> _logger;
        private readonly IIngredientPersistenceQueries _queries;

        public GetIngredientsByRecipeIdHandler(
            ILogger<GetIngredientsByRecipeIdHandler> logger,
            IIngredientPersistenceQueries queries
            )
        {
            _logger = logger;
            _queries = queries;
        }

        public async Task<ICollection<Domain.Entity.Recipe.Ingredient>> Handle(GetIngredientsByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetIngredientsByRecipeIdHandler - RecipeId:{RecipeId}", request.RecipeId);

            var results = await _queries.GetIngredientsByRecipeId(request.RecipeId, cancellationToken);

            _logger.LogTrace("Exit GetIngredientsByRecipeIdHandler - RecipeId:{RecipeId}", request.RecipeId);

            return results;
        }
    }
}
