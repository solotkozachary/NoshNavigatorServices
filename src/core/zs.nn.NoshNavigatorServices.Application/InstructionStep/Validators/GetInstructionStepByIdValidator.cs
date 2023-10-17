using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Queries;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Validators
{
    /// <summary>
    /// Validates queries to retrieve an instruction step by id.
    /// </summary>
    public class GetInstructionStepByIdValidator : AbstractValidator<GetInstructionStepByIdQuery>
    {
        public GetInstructionStepByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
