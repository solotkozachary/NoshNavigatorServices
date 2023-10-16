using MediatR;
using System;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Queries
{
    /// <summary>
    /// The query to retrieve an ingredient by id.
    /// </summary>
    public class GetIngredientByIdQuery : IRequest<Domain.Entity.Recipe.Ingredient>
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
