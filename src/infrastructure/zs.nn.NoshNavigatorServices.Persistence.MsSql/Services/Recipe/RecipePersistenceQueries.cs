using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Recipe;

namespace zs.nn.NoshNavigatorServices.Persistence.MsSql.Services.Recipe
{
    public class RecipePersistenceQueries : IRecipePersistenceQueries
    {
        private readonly RecipeContext _context;
        private readonly ILogger<RecipePersistenceQueries> _logger;

        public RecipePersistenceQueries(RecipeContext context, ILogger<RecipePersistenceQueries> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Domain.Entity.Recipe.Recipe> GetById(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetRecipeById - RecipeId:{RecipeId}", id);

            var result = await _context.Recipes.FindAsync(new object[] { id }, cancellationToken);

            _logger.LogTrace("Exit GetRecipeById - RecipeId:{RecipeId}", id);

            return result;
        }
    }

}
