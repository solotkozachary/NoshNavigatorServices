using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Queries;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient;
using zs.nn.NoshNavigatorServices.Application.Models;
using System.Linq;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Handlers
{
    /// <summary>
    /// The handler responsible for retrieving a paginated list of ingredients by name.
    /// </summary>
    public class GetIngredientsByNameHandler : IRequestHandler<GetIngredientsByNameQuery, PaginatedResult<Domain.Entity.Recipe.Ingredient>>
    {
        private readonly ILogger<GetIngredientsByNameHandler> _logger;
        private readonly IIngredientPersistenceQueries _queries;

        public GetIngredientsByNameHandler(
            ILogger<GetIngredientsByNameHandler> logger,
            IIngredientPersistenceQueries queries
            )
        {
            _logger = logger;
            _queries = queries;
        }

        public async Task<PaginatedResult<Domain.Entity.Recipe.Ingredient>> Handle(GetIngredientsByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetIngredientsByNameHandler - IngredientName:{IngredientName}, PageSize:{PageSize}, CurrentPage:{CurrentPage}",
                             request.Name, request.PageSize, request.CurrentPage);

            var results = await _queries.GetPaginatedIngredientsByName(request.Name, request.PageSize, request.CurrentPage, cancellationToken);

            var paginatedResult = new PaginatedResult<Domain.Entity.Recipe.Ingredient>
            {
                Results = results.ToList(),
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage
            };

            _logger.LogTrace("Exit GetIngredientsByNameHandler - IngredientName:{IngredientName}, PageSize:{PageSize}, CurrentPage:{CurrentPage}",
                             request.Name, request.PageSize, request.CurrentPage);

            return paginatedResult;
        }
    }
}
