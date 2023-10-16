using MediatR;

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
        public string Id { get; set; }
    }
}
