using MediatR;
using System;
using System.Collections.Generic;

namespace zs.nn.NoshNavigatorServices.Application.InstructionStep.Queries
{
    /// <summary>
    /// The query to retrieve instruction steps by recipe id.
    /// </summary>
    public class GetInstructionStepsByRecipeIdQuery : IRequest<ICollection<Domain.Entity.Recipe.InstructionStep>>
    {
        /// <summary>
        /// The unique identifier of the recipe.
        /// </summary>
        public Guid RecipeId { get; set; }
    }
}
