using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient;

namespace zs.nn.NoshNavigatorServices.Persistence.MsSql.Services.Ingredient
{
    /// <summary>
    /// An implementation of a service that performs entity commands.
    /// </summary>
    public class IngredientPersistenceCommands : IIngredientPersistenceCommands
    {
        private readonly RecipeContext _context;
        private readonly ILogger<IngredientPersistenceCommands> _logger;

        public IngredientPersistenceCommands(
            RecipeContext context,
            ILogger<IngredientPersistenceCommands> logger
            )
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(Domain.Entity.Recipe.Ingredient entity, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter Create Ingredient - IngredientId:{IngredientId}", entity.Id);

            await _context.Ingredients.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync();

            _logger.LogTrace("Exit Create Ingredient - IngredientId:{IngredientId}", entity.Id);
        }
    }
}
