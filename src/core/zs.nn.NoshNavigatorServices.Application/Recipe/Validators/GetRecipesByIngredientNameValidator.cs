using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.Recipe.Queries;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Validators
{
    /// <summary>
    /// Validates queries to retrieve recipes by ingredient name.
    /// </summary>
    public class GetRecipesByIngredientNameValidator : AbstractValidator<GetRecipesByIngredientNameQuery>
    {
        public GetRecipesByIngredientNameValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CurrentPage).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
