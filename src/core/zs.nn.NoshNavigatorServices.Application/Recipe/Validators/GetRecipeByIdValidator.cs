using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.Recipe.Queries;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Validators
{
    /// <summary>
    /// Validates queries to retrieve a recipe by id.
    /// </summary>
    public class GetRecipeByIdValidator : AbstractValidator<GetRecipeByIdQuery>
    {
        public GetRecipeByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
