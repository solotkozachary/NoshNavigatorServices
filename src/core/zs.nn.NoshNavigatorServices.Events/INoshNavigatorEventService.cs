using System.Threading.Tasks;
using System.Threading;
using zs.nn.NoshNavigatorServices.Domain.Entity;

namespace zs.nn.NoshNavigatorServices.Events
{
    /// <summary>
    /// Contract for a service that publishes events.
    /// </summary>
    public interface INoshNavigatorEventService<T, U>
        where U : NoshNavigatorEntity, new()
        where T : INoshNavigatorEvent<T, U>
    {
        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <param name="applicationEvent">The event to publish.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        Task PublishNoshNavigatorEvent(T applicationEvent, CancellationToken cancellationToken);
    }
}
