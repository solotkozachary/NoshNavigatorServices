using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Queries;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Validators
{
    /// <summary>
    /// Validates queries to retrieve ingredients by name.
    /// </summary>
    public class GetIngredientsByNameValidator : AbstractValidator<GetIngredientsByNameQuery>
    {
        public GetIngredientsByNameValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CurrentPage).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
