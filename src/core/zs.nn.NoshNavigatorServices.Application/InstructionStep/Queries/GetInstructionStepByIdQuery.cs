using MediatR;
using System;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Queries
{
    /// <summary>
    /// The query to retrieve an instruction step by id.
    /// </summary>
    public class GetInstructionStepByIdQuery : IRequest<Domain.Entity.Recipe.InstructionStep>
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Flag to indicate entity must exist.
        /// </summary>
        public bool MustExist { get; set; }
    }
}
