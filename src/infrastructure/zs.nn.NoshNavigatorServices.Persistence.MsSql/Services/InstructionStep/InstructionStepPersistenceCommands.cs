using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep;

namespace zs.nn.NoshNavigatorServices.Persistence.MsSql.Services.InstructionStep
{
    public class InstructionStepPersistenceCommands : IInstructionStepPersistenceCommands
    {
        private readonly NoshNavigatorServicesDbContext _context;
        private readonly ILogger<InstructionStepPersistenceCommands> _logger;

        public InstructionStepPersistenceCommands(NoshNavigatorServicesDbContext context, ILogger<InstructionStepPersistenceCommands> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(Domain.Entity.Recipe.InstructionStep entity, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter CreateInstructionStep - InstructionStepId:{InstructionStepId}", entity.Id);

            await _context.InstructionSteps.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogTrace("Exit CreateInstructionStep - InstructionStepId:{InstructionStepId}", entity.Id);
        }

        public async Task Update(Domain.Entity.Recipe.InstructionStep entity, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter UpdateInstructionStep - InstructionStepId:{InstructionStepId}", entity.Id);

            _context.InstructionSteps.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogTrace("Update UpdateInstructionStep - InstructionStepId:{InstructionStepId}", entity.Id);
        }
    }

}
