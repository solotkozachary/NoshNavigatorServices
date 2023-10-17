using MediatR;
using System;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Commands
{
    /// <summary>
    /// A command to update an instruction step.
    /// </summary>
    public class UpdateInstructionStepCommand : IRequest<Guid>
    {
        /// <summary>
        /// The unique identifier of the instruction atep.
        /// </summary>
        public Guid InstructionStepId { get; set; }

        /// <summary>
        /// Gets or sets the order or sequence number indicating the position of the step in the recipe instructions.
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the description or details of the instruction step.
        /// </summary>
        public string Description { get; set; }
    }
}
