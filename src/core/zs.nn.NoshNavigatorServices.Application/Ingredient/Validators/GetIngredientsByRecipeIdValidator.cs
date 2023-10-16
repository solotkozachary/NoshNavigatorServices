using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Queries;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Validators
{
    /// <summary>
    /// Validates queries to retrieve ingredients by recipe id.
    /// </summary>
    public class GetIngredientsByRecipeIdValidator : AbstractValidator<GetIngredientsByRecipeIdQuery>
    {
        public GetIngredientsByRecipeIdValidator()
        {
            RuleFor(x => x.RecipeId).NotEmpty();
        }
    }
}
