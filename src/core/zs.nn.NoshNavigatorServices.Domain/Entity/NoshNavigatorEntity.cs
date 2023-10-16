using System;

namespace zs.nn.NoshNavigatorServices.Domain.Entity
{
    /// <summary>
    /// An entity in the Nosh Navigator domain.
    /// </summary>
    public class NoshNavigatorEntity
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A flag to indicate if the entity record is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The date the entity was created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Who created the entity.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// The date the entity was updated on.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Who updated the entity.
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}
