using System.Threading.Tasks;
using System.Threading;
using System;

namespace zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Recipe
{
    /// <summary>
    /// A contract for a service that performs entity queries.
    /// </summary>
    public interface IRecipePersistenceQueries
    {
        /// <summary>
        /// Retrieve a recipe by id.
        /// </summary>
        /// <param name="id">The id to search by.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The entity found.</returns>
        Task<Domain.Entity.Recipe.Recipe> GetById(Guid id, CancellationToken cancellationToken);
    }
}
