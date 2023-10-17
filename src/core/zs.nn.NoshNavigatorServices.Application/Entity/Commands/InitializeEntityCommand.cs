using MediatR;

namespace zs.nn.NoshNavigatorServices.Application.Entity.Commands
{
    /// <summary>
    /// A command to initialize an entity.
    /// </summary>
    public class InitializeEntityCommand<T> : IRequest<T>
        where T : Domain.Entity.NoshNavigatorEntity
    {
        /// <summary>
        /// The unique identifier of the source that created the entity.
        /// </summary>
        public string EntityCreationSourceId { get; set; }
    }
}
