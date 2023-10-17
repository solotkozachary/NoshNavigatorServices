using MediatR;
using zs.nn.NoshNavigatorServices.Application.Models;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Queries
{
    /// <summary>
    /// The query to retrieve a collection of ingredients by recipe id.
    /// </summary>
    public class GetIngredientsByNameQuery : IRequest<PaginatedResult<Domain.Entity.Recipe.Ingredient>>
    {
        /// <summary>
        /// Gets or sets the name of the ingredient.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of records to return in the page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The current page number to fetch.
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
