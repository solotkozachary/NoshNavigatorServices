using System.Threading.Tasks;
using System.Threading;

namespace zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Recipe
{
    /// <summary>
    /// A contract for a service that performs entity commands.
    /// </summary>
    public interface IRecipePersistenceCommands
    {
        /// <summary>
        /// Create a recipe.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        Task Create(Domain.Entity.Recipe.Recipe entity, CancellationToken cancellationToken);
    }
}
