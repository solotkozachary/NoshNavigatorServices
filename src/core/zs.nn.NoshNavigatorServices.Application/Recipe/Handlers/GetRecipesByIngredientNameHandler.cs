using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Queries;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Recipe;
using zs.nn.NoshNavigatorServices.Application.Models;
using zs.nn.NoshNavigatorServices.Application.Recipe.Queries;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Handlers
{
    /// <summary>
    /// The handler responsible for retrieving recipes by ingredient name.
    /// </summary>
    public class GetRecipesByIngredientNameHandler : IRequestHandler<GetRecipesByIngredientNameQuery, PaginatedResult<Domain.Entity.Recipe.Recipe>>
    {
        private readonly ILogger<GetRecipesByIngredientNameHandler> _logger;
        private readonly IMediator _mediator;

        public GetRecipesByIngredientNameHandler(
            ILogger<GetRecipesByIngredientNameHandler> logger,
            IMediator mediator
            )
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<PaginatedResult<Domain.Entity.Recipe.Recipe>> Handle(GetRecipesByIngredientNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetRecipesByIngredientNameHandler - IngredientName:{IngredientName}, PageSize:{PageSize}, CurrentPage:{CurrentPage}",
                             request.Name, request.PageSize, request.CurrentPage);

            var results = new List<Domain.Entity.Recipe.Recipe>();

            var ingredientList = await _mediator.Send(new GetIngredientsByNameQuery 
                { Name = request.Name, PageSize = request.PageSize, CurrentPage = request.CurrentPage }, cancellationToken);

            var taskList = new List<Task<Domain.Entity.Recipe.Recipe>>();

            foreach (var ingredient in ingredientList.Results)
            {
                taskList.Add(_mediator.Send(new GetRecipeByIdQuery { Id = ingredient.RecipeId, MustExist = true }, cancellationToken));
            }

            await Task.WhenAll(taskList);

            foreach(var task in taskList)
            {
                results.Add(await task);
            }

            var paginatedResult = new PaginatedResult<Domain.Entity.Recipe.Recipe>
            {
                Results = results,
                PageSize = request.PageSize,
                CurrentPage = request.CurrentPage
            };

            _logger.LogTrace("Exit GetRecipesByIngredientNameHandler - IngredientName:{IngredientName}, PageSize:{PageSize}, CurrentPage:{CurrentPage}",
                             request.Name, request.PageSize, request.CurrentPage);

            return paginatedResult;
        }
    }
}
