using System.Threading.Tasks;
using System.Threading;

namespace zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient
{
    /// <summary>
    /// A contract for a service that performs entity commands.
    /// </summary>
    public interface IIngredientPersistenceCommands
    {
        /// <summary>
        /// Create an ingredient.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        Task Create(Domain.Entity.Recipe.Ingredient entity, CancellationToken cancellationToken);
    }
}
