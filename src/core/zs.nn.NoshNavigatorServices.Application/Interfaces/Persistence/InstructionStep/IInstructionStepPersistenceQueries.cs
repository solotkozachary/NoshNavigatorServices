using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep
{
    /// <summary>
    /// A contract for a service that performs entity queries.
    /// </summary>
    public interface IInstructionStepPersistenceQueries
    {
        /// <summary>
        /// Retrieve an instruction step by id.
        /// </summary>
        /// <param name="id">The id to search by.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The entity found.</returns>
        Task<Domain.Entity.Recipe.InstructionStep> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve a list of instruction steps by recipe id.
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A list of entities found.</returns>
        Task<ICollection<Domain.Entity.Recipe.InstructionStep>> GetByRecipeId(Guid recipeId, CancellationToken cancellationToken);
    }
}
