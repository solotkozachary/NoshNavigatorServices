using MediatR;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Queries
{
    /// <summary>
    /// The query to retrieve an ingredient recipe by id.
    /// </summary>
    public class GetIngredientByIdQuery : IRequest<Domain.Entity.Recipe.Ingredient>
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public string Id { get; set; }
    }
}
