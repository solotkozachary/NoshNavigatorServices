using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Commands;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Validators
{
    /// <summary>
    /// Validates commands to update a instruction step.
    /// </summary>
    public class UpdateInstructionStepValidator : AbstractValidator<UpdateInstructionStepCommand>
    {
        public UpdateInstructionStepValidator()
        {
            RuleFor(x => x.InstructionStepId).NotEmpty();
            RuleFor(x => x.SequenceNumber).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
