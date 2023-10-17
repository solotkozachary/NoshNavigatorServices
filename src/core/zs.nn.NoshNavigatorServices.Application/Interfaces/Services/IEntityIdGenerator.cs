using System.Threading.Tasks;
using System.Threading;
using System;

namespace zs.nn.NoshNavigatorServices.Application.Interfaces.Services
{
    /// <summary>
    /// A contract for a service that generates entity ids.
    /// </summary>
    public interface IEntityIdGenerator
    {
        /// <summary>
        /// Generate a unique identifier for an entity.
        /// </summary>
        /// <returns>A unique identifier for an entity.</returns>
        Task<Guid> GenerateId(CancellationToken cancellationToken);
    }
}
