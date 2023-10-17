using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Services;

namespace zs.nn.NoshNavigatorServices.Infrastructure.Services
{
    /// <summary>
    /// An implementation of a service that generates entity ids.
    /// </summary>
    public class EntityIdGenerator : IEntityIdGenerator
    {
        private readonly ILogger<EntityIdGenerator> _logger;

        public EntityIdGenerator(
            ILogger<EntityIdGenerator> logger
            )
        {
            _logger = logger;
        }

        /// <summary>
        /// Generate a unique identifier for an entity.
        /// </summary>
        /// <returns>A unique identifier for an entity.</returns>
        public Task<Guid> GenerateId(CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GenerateId");

            // We have more control over our entity key generation if we need it. For demo purposes, just using a GUID.

            var result = Guid.NewGuid();

            _logger.LogTrace("Exit GenerateId - EntityKey;{EntityKey}", result);

            return Task.FromResult(result);
        }
    }
}
