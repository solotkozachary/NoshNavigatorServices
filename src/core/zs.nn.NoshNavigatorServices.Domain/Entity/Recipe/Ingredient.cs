namespace zs.nn.NoshNavigatorServices.Domain.Entity.Recipe
{
    /// <summary>
    /// An ingredient in a recipe.
    /// </summary>
    public class Ingredient : NoshNavigatorEntity
    {
        /// <summary>
        /// Gets or sets the name of the ingredient.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the ingredient.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the quantity or measure of the ingredient for the recipe.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the foreign key property that links the ingredient to its associated recipe.
        /// </summary>
        public int RecipeId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the recipe to which the ingredient belongs.
        /// </summary>
        public Recipe Recipe { get; set; }
    }
}
