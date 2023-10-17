using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Commands;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Queries;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep;
using zs.nn.NoshNavigatorServices.Events.Recipe;
using zs.nn.NoshNavigatorServices.Events;

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
        private readonly INoshNavigatorEventService<InstructionStepUpdatedEvent, Domain.Entity.Recipe.InstructionStep> _eventService;

        public UpdateInstructionStepHandler(
            ILogger<UpdateInstructionStepHandler> logger,
            IInstructionStepPersistenceCommands commands,
            IMediator mediator,
            INoshNavigatorEventService<InstructionStepUpdatedEvent, Domain.Entity.Recipe.InstructionStep> eventService
            )
        {
            _logger = logger;
            _commands = commands;
            _mediator = mediator;
            _eventService = eventService;
        }

        public async Task<Guid> Handle(UpdateInstructionStepCommand request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter UpdateInstructionStepHandler - InstructionStepId:{InstructionStepId}", request.InstructionStepId);

            var entity = await _mediator.Send(new GetInstructionStepByIdQuery
            {
                Id = request.InstructionStepId,
                MustExist = true
            }, cancellationToken);

            entity.SequenceNumber = request.SequenceNumber;
            entity.Description = request.Description;

            await _commands.Update(entity, cancellationToken);

            await _eventService.PublishNoshNavigatorEvent(new InstructionStepUpdatedEvent(entity), cancellationToken);

            _logger.LogTrace("Exit UpdateInstructionStepHandler - InstructionStepId:{InstructionStepId}", request.InstructionStepId);

            return entity.Id;
        }
    }
}
