using System;

namespace zs.nn.NoshNavigatorServices.Domain.Entity.Recipe
{
    public class InstructionStep : NoshNavigatorEntity
    {
        /// <summary>
        /// Gets or sets the order or sequence number indicating the position of the step in the recipe instructions.
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the description or details of the instruction step.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the foreign key property that links the instruction step to its associated recipe.
        /// </summary>
        public Guid RecipeId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the recipe to which the instruction step belongs.
        /// </summary>
        public Recipe Recipe { get; set; }
    }
}
