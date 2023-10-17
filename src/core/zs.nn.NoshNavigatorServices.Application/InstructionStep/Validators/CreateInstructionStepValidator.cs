using FluentValidation;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Commands;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Validators
{
    /// <summary>
    /// Validates commands to create a new instruction step.
    /// </summary>
    public class CreateInstructionStepValidator : AbstractValidator<CreateInstructionStepCommand>
    {
        public CreateInstructionStepValidator()
        {
            RuleFor(x => x.SequenceNumber).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.RecipeId).NotEmpty();
        }
    }
}
