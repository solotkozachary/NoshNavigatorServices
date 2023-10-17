using MediatR;
using System;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Queries
{
    /// <summary>
    /// The query to retrieve a recipe by id.
    /// </summary>
    public class GetRecipeByIdQuery : IRequest<Domain.Entity.Recipe.Recipe>
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
