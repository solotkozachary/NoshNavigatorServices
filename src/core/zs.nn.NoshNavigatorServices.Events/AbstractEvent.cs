using System.Collections.Generic;
using System.Reflection;
using System;
using zs.nn.NoshNavigatorServices.Events.Util;
using zs.nn.NoshNavigatorServices.Domain.Entity;

namespace zs.nn.NoshNavigatorServices.Events
{
    /// <summary>
    /// An abstract class to reinforce event model definition.
    /// </summary>
    /// <typeparam name="T">The type of the event.</typeparam>
    /// <typeparam name="U">The type of entity the event is for.</typeparam>
    public class AbstractEvent<T, U> : INoshNavigatorEvent<T, U>
        where U : NoshNavigatorEntity, new()
        where T : INoshNavigatorEvent<T, U>
    {
        public AbstractEvent(U data)
        {
            EventPayload = data;

            // Set up static utils to provide application context to events
            EventId = Guid.NewGuid().ToString();
            CorrelationId = Guid.NewGuid().ToString();
            Source = Assembly.GetExecutingAssembly().GetName().Name;
            EventDate = DateTime.Now;
        }

        /// <summary>
        /// The unique identifier of the event. To be assigned by event service provider.
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// The version of the event, which follows the version of the domain.
        /// </summary>
        public string Version { get { return DomainVersionProvider.DomainVersion; } }

        /// <summary>
        /// Event correlation id.
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// The event source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The date of the event.
        /// </summary>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// The data payload of the event.
        /// </summary>
        public U EventPayload { get; }

        /// <summary>
        /// Event metadata.
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; }
    }
}
