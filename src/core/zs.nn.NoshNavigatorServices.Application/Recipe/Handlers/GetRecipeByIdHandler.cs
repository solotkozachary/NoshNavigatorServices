using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using zs.nn.NoshNavigatorServices.Application.Enumerations;
using zs.nn.NoshNavigatorServices.Application.Exceptions;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Recipe;
using zs.nn.NoshNavigatorServices.Application.Recipe.Queries;

namespace zs.nn.NoshNavigatorServices.Application.Recipe.Handlers
{
    /// <summary>
    /// The handler responsible for retrieving a recipe by Id.
    /// </summary>
    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdQuery, Domain.Entity.Recipe.Recipe>
    {
        private readonly ILogger<GetRecipeByIdHandler> _logger;
        private readonly IRecipePersistenceQueries _queries;

        public GetRecipeByIdHandler(
            ILogger<GetRecipeByIdHandler> logger,
            IRecipePersistenceQueries queries
            )
        {
            _logger = logger;
            _queries = queries;
        }

        public async Task<Domain.Entity.Recipe.Recipe> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetRecipeByIdHandler - RecipeId:{RecipeId}", request.Id);

            var entity = await _queries.GetById(request.Id, cancellationToken);

            if (request.MustExist && entity == null)
            {
                var errMsg = "No Recipe exists for provided id - RecipeId:{RecipeId}";

                _logger.LogError(errMsg, request.Id);

                _logger.LogTrace("Exit GetRecipeByIdHandler - RecipeId:{RecipeId}", request.Id);

                throw new NoshNavigatorException(ApplicationConcern.Entity, ApplicationRules.MustExist, errMsg.Replace("{RecipeId}", request.Id.ToString()));
            }

            _logger.LogTrace("Exit GetRecipeByIdHandler - RecipeId:{RecipeId}", request.Id);

            return entity;
        }
    }
}
