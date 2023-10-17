using MediatR;
using System;
using System.Collections.Generic;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Commands
{
    /// <summary>
    /// A command to create a recipe.
    /// </summary>
    public class CreateRecipeCommand : IRequest<Guid>
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
        public ICollection<RecipeIngredient> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the collection of instruction steps for the recipe.
        /// </summary>
        public ICollection<RecipeInstructionStep> InstructionSteps { get; set; }
    }

    public class RecipeInstructionStep
    {
        /// <summary>
        /// Gets or sets the order or sequence number indicating the position of the step in the recipe instructions.
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the description or details of the instruction step.
        /// </summary>
        public string Description { get; set; }
    }

    public class RecipeIngredient
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
    }
}
