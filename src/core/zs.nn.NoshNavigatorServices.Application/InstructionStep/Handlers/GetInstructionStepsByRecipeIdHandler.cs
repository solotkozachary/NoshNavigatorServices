using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using zs.nn.NoshNavigatorServices.Application.InstructionStep.Queries;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Handlers
{
    /// <summary>
    /// The handler responsible for retrieving a list of instruction steps by recipe id.
    /// </summary>
    public class GetInstructionStepsByRecipeIdHandler : IRequestHandler<GetInstructionStepsByRecipeIdQuery, ICollection<Domain.Entity.Recipe.InstructionStep>>
    {
        private readonly ILogger<GetInstructionStepsByRecipeIdHandler> _logger;
        private readonly IInstructionStepPersistenceQueries _queries;

        public GetInstructionStepsByRecipeIdHandler(
            ILogger<GetInstructionStepsByRecipeIdHandler> logger,
            IInstructionStepPersistenceQueries queries
            )
        {
            _logger = logger;
            _queries = queries;
        }

        public async Task<ICollection<Domain.Entity.Recipe.InstructionStep>> Handle(GetInstructionStepsByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetInstructionStepsByRecipeIdHandler - RecipeId:{RecipeId}", request.RecipeId);

            var instructionSteps = await _queries.GetByRecipeId(request.RecipeId, cancellationToken);

            _logger.LogTrace("Exit GetInstructionStepsByRecipeIdHandler - RecipeId:{RecipeId}", request.RecipeId);

            return instructionSteps;
        }
    }

}
