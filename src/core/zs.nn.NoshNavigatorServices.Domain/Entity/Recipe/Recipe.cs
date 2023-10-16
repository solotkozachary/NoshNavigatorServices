using System.Collections.Generic;

namespace zs.nn.NoshNavigatorServices.Domain.Entity.Recipe
{
    /// <summary>
    /// A recipe.
    /// </summary>
    public class Recipe : NoshNavigatorEntity
    {
        /// <summary>
        /// Gets or sets the name of the recipe.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the recipe.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of ingredients associated with the recipe.
        /// </summary>
        public ICollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the collection of instruction steps for the recipe.
        /// </summary>
        public ICollection<InstructionStep> InstructionSteps { get; set; }
    }
}
