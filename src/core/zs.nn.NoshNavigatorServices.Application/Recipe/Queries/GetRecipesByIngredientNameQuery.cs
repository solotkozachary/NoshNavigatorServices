using MediatR;
using zs.nn.NoshNavigatorServices.Application.Models;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Queries
{
    /// <summary>
    /// The query to get a collection of recipes by ingredient name.
    /// </summary>
    public class GetRecipesByIngredientNameQuery : IRequest<PaginatedResult<Domain.Entity.Recipe.Recipe>>
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
