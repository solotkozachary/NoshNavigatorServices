using MediatR;
using System;
using System.Collections.Generic;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Queries
{
    /// <summary>
    /// The query to retrieve a collection of ingredients by recipe id.
    /// </summary>
    public class GetIngredientsByRecipeIdQuery : IRequest<ICollection<Domain.Entity.Recipe.Ingredient>>
    {
        /// <summary>
        /// The unique identifier of the recipe.
        /// </summary>
        public Guid RecipeId { get; set; }
    }
}
