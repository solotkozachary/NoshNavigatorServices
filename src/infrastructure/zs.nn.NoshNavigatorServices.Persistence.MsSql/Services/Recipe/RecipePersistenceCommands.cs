using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Recipe;
using zs.nn.NoshNavigatorServices.Domain.Entity.Recipe;

namespace zs.nn.NoshNavigatorServices.Persistence.MsSql.Services.Recipe
{
    public class RecipePersistenceCommands : IRecipePersistenceCommands
    {
        private readonly NoshNavigatorServicesDbContext _context;
        private readonly ILogger<RecipePersistenceCommands> _logger;

        public RecipePersistenceCommands(NoshNavigatorServicesDbContext context, ILogger<RecipePersistenceCommands> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(Domain.Entity.Recipe.Recipe entity, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateRecipe - RecipeId:{RecipeId}", entity.Id);

            await _context.Recipes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogTrace("Exit CreateRecipe - RecipeId:{RecipeId}", entity.Id);
        }
    }

}
