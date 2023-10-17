using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using zs.nn.NoshNavigatorServices.Domain.Entity;
using zs.nn.NoshNavigatorServices.Events;

namespace zs.nn.NoshNavigatorServices.Infrastructure.Services
{
    /// <summary>
    /// An implementation of a service that publishes events.
    /// </summary>
    public class NoshNavigatorEventService<T, U> : INoshNavigatorEventService<T, U>
        where U : NoshNavigatorEntity, new()
        where T : AbstractEvent<T, U>
    {
        private readonly ILogger<NoshNavigatorEventService<T, U>> _logger;

        public NoshNavigatorEventService(
            ILogger<NoshNavigatorEventService<T, U>> logger
            )
        {
            _logger = logger;
        }

        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <param name="applicationEvent">The event to publish.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        public Task PublishNoshNavigatorEvent(T applicationEvent, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter PublishEvent - EventId:{EventId}", applicationEvent.EventId);

            // Integrate with service bus here

            _logger.LogTrace("Exit PublishEvent - EventId:{EventId}", applicationEvent.EventId);

            return Task.CompletedTask;
        }
    }
}
