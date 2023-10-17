using System.Threading.Tasks;
using System.Threading;

namespace zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep
{
    /// <summary>
    /// A contract for a service that performs entity commands.
    /// </summary>
    public interface IInstructionStepPersistenceCommands
    {
        /// <summary>
        /// Create an instruction step.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        Task Create(Domain.Entity.Recipe.InstructionStep entity, CancellationToken cancellationToken);

        /// <summary>
        /// Update an instruction step.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        Task Update(Domain.Entity.Recipe.InstructionStep entity, CancellationToken cancellationToken);
    }
}
