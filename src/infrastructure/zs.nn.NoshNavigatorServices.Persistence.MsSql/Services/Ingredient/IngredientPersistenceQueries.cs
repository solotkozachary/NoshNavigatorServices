using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace zs.nn.NoshNavigatorServices.Persistence.MsSql.Services.Ingredient
{
    public class IngredientPersistenceQueries : IIngredientPersistenceQueries
    {
        private readonly RecipeContext _context;
        private readonly ILogger<IngredientPersistenceQueries> _logger;

        public IngredientPersistenceQueries(RecipeContext context, ILogger<IngredientPersistenceQueries> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Domain.Entity.Recipe.Ingredient> GetById(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetIngredientById - IngredientId:{IngredientId}", id);

            var result =  await _context.Ingredients.FindAsync(new object[] { id }, cancellationToken);

            _logger.LogTrace("Exit GetIngredientById - IngredientId:{IngredientId}", id);

            return result;
        }

        public async Task<ICollection<Domain.Entity.Recipe.Ingredient>> GetPaginatedIngredientsByName(string name, int pageSize, int currentPage, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetPaginatedIngredientsByName - IngredientName:{IngredientName}", name);

            var result = await _context.Ingredients
                .Where(i => i.Name.Contains(name))
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            _logger.LogTrace("Exit GetPaginatedIngredientsByName - IngredientName:{IngredientName}", name);

            return result;
        }

        public async Task<ICollection<Domain.Entity.Recipe.Ingredient>> GetIngredientsByRecipeId(Guid recipeId, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetIngredientsByRecipeId - RecipeId:{RecipeId}", recipeId);

            var result =  await _context.Ingredients
                .Where(i => i.RecipeId == recipeId)
                .ToListAsync(cancellationToken);

            _logger.LogTrace("Exit GetIngredientsByRecipeId - RecipeId:{RecipeId}", recipeId);

            return result;
        }
    }

}
