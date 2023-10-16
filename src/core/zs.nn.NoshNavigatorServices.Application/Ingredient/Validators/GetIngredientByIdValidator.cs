using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Queries;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Validators
{
    /// <summary>
    /// Validates queries to retrieve an ingredient by id.
    /// </summary>
    public class GetIngredientByIdValidator : AbstractValidator<GetIngredientByIdQuery>
    {
        public GetIngredientByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
