using MediatR;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Commands
{
    /// <summary>
    /// A command to create an instruction step.
    /// </summary>
    public class CreateInstructionStepCommand : IRequest<string>
    {
        /// <summary>
        /// Gets or sets the order or sequence number indicating the position of the step in the recipe instructions.
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the description or details of the instruction step.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the foreign key property that links the instruction step to its associated recipe.
        /// </summary>
        public string RecipeId { get; set; }
    }
}
