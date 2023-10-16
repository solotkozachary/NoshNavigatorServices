using System.Threading.Tasks;
using System.Threading;

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
        Task<Domain.Entity.Recipe.InstructionStep> GetById(string id, CancellationToken cancellationToken);
    }
}
