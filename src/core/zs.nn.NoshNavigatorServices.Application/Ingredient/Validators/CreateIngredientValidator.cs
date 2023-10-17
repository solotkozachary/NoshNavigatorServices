using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Commands;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Validators
{
    /// <summary>
    /// Validates commands to create a new ingredient.
    /// </summary>
    public class CreateIngredientValidator : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.RecipeId).NotEmpty();
        }
    }
}
