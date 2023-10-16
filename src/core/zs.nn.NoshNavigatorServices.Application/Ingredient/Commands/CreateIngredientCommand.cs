using MediatR;
using System;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Commands
{
    /// <summary>
    /// A command to create an ingredient.
    /// </summary>
    public class CreateIngredientCommand : IRequest<string>
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
        public Guid RecipeId { get; set; }
    }
}
