using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using zs.nn.NoshNavigatorServices.Application.Enumerations;
using zs.nn.NoshNavigatorServices.Application.Exceptions;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Queries;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Handlers
{
    /// <summary>
    /// The handler responsible for retrieving an instruction step by Id.
    /// </summary>
    public class GetInstructionStepByIdHandler : IRequestHandler<GetInstructionStepByIdQuery, Domain.Entity.Recipe.InstructionStep>
    {
        private readonly ILogger<GetInstructionStepByIdHandler> _logger;
        private readonly IInstructionStepPersistenceQueries _queries;

        public GetInstructionStepByIdHandler(
            ILogger<GetInstructionStepByIdHandler> logger,
            IInstructionStepPersistenceQueries queries
            )
        {
            _logger = logger;
            _queries = queries;
        }

        public async Task<Domain.Entity.Recipe.InstructionStep> Handle(GetInstructionStepByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetInstructionStepByIdHandler - StepId:{StepId}", request.Id);

            var entity = await _queries.GetById(request.Id, cancellationToken);

            if (request.MustExist && entity == null)
            {
                var errMsg = "No Instruction Step exists for provided id - StepId:{StepId}";

                _logger.LogError(errMsg, request.Id);

                _logger.LogTrace("Exit GetInstructionStepByIdHandler - StepId:{StepId}", request.Id);

                throw new NoshNavigatorException(ApplicationConcern.Entity, ApplicationRules.MustExist, errMsg.Replace("{StepId}", request.Id.ToString()));
            }

            _logger.LogTrace("Exit GetInstructionStepByIdHandler - StepId:{StepId}", request.Id);

            return entity;
        }
    }
}
