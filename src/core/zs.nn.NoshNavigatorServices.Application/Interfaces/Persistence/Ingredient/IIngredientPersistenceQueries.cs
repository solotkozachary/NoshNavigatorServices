using System.Threading.Tasks;
using System.Threading;
using System;

namespace zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient
{
    /// <summary>
    /// A contract for a service that performs entity queries.
    /// </summary>
    public interface IIngredientPersistenceQueries
    {
        /// <summary>
        /// Retrieve an ingredient by id.
        /// </summary>
        /// <param name="id">The id to search by.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The entity found.</returns>
        Task<Domain.Entity.Recipe.Ingredient> GetById(Guid id, CancellationToken cancellationToken);
    }
}
