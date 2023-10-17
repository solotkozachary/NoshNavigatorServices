using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Commands;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Queries;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Handlers
{
    /// <summary>
    /// The handler responsible for updating an instruction step.
    /// </summary>
    public class UpdateInstructionStepHandler : IRequestHandler<UpdateInstructionStepCommand, Guid>
    {
        private readonly ILogger<UpdateInstructionStepHandler> _logger;
        private readonly IInstructionStepPersistenceCommands _commands;
        private readonly IMediator _mediator;

        public UpdateInstructionStepHandler(
            ILogger<UpdateInstructionStepHandler> logger,
            IInstructionStepPersistenceCommands commands,
            IMediator mediator
            )
        {
            _logger = logger;
            _commands = commands;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(UpdateInstructionStepCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter UpdateInstructionStepHandler - InstructionStepId:{InstructionStepId}", request.InstructionStepId);

            var instructionStep = await _mediator.Send(new GetInstructionStepByIdQuery
            {
                Id = request.InstructionStepId,
                MustExist = true
            }, cancellationToken);

            instructionStep.SequenceNumber = request.SequenceNumber;
            instructionStep.Description = request.Description;

            await _commands.Update(instructionStep, cancellationToken);

            _logger.LogTrace("Exit UpdateInstructionStepHandler - InstructionStepId:{InstructionStepId}", request.InstructionStepId);

            return instructionStep.Id;
        }
    }
}
