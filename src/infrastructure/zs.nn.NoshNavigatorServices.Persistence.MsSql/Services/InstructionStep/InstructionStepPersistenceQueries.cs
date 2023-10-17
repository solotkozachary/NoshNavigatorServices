using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.InstructionStep;
using Microsoft.EntityFrameworkCore;

namespace zs.nn.NoshNavigatorServices.Persistence.MsSql.Services.InstructionStep
{
    public class InstructionStepPersistenceQueries : IInstructionStepPersistenceQueries
    {
        private readonly RecipeContext _context;
        private readonly ILogger<InstructionStepPersistenceQueries> _logger;

        public InstructionStepPersistenceQueries(RecipeContext context, ILogger<InstructionStepPersistenceQueries> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Domain.Entity.Recipe.InstructionStep> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.InstructionSteps.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<ICollection<Domain.Entity.Recipe.InstructionStep>> GetByRecipeId(Guid recipeId, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetByRecipeId - RecipeId:{RecipeId}", recipeId);

            var result = await _context.InstructionSteps
                .Where(i => i.RecipeId == recipeId)
                .ToListAsync(cancellationToken);

            _logger.LogTrace("Exit GetByRecipeId - RecipeId:{RecipeId}", recipeId);

            return result;
        }
    }

}
