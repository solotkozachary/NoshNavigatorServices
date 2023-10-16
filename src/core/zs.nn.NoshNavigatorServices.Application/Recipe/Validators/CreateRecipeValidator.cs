using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.Recipe.Commands;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Validators
{
    /// <summary>
    /// Validates commands to create a new recipe.
    /// </summary>
    public class CreateRecipeValidator : AbstractValidator<CreateRecipeCommand>
    {
        public CreateRecipeValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
