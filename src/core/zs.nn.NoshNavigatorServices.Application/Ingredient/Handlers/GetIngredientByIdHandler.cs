using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using zs.nn.NoshNavigatorServices.Application.Enumerations;
using zs.nn.NoshNavigatorServices.Application.Exceptions;
using zs.nn.NoshNavigatorServices.Application.Ingredient.Queries;
using zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient;

namespace zs.nn.NoshNavigatorServices.Application.Ingredient.Handlers
{
    /// <summary>
    /// The handler responsible for retrieving an ingredient by Id.
    /// </summary>
    public class GetIngredientByIdHandler : IRequestHandler<GetIngredientByIdQuery, Domain.Entity.Recipe.Ingredient>
    {
        private readonly ILogger<GetIngredientByIdHandler> _logger;
        private readonly IIngredientPersistenceQueries _queries;

        public GetIngredientByIdHandler(
            ILogger<GetIngredientByIdHandler> logger,
            IIngredientPersistenceQueries queries
            )
        {
            _logger = logger;
            _queries = queries;
        }

        public async Task<Domain.Entity.Recipe.Ingredient> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Enter GetIngredientByIdHandler - IngredientId:{IngredientId}", request.Id);

            var entity = await _queries.GetById(request.Id, cancellationToken);

            if (request.MustExist && entity == null)
            {
                var errMsg = "No Ingredient exists for provided id - IngredientId:{IngredientId}";

                _logger.LogError(errMsg, request.Id);

                _logger.LogTrace("Exit GetIngredientByIdHandler - IngredientId:{IngredientId}", request.Id);

                throw new NoshNavigatorException(ApplicationConcern.Entity, ApplicationRules.MustExist, errMsg.Replace("{IngredientId}", request.Id.ToString()));
            }

            _logger.LogTrace("Exit GetIngredientByIdHandler - IngredientId:{IngredientId}", request.Id);

            return entity;
        }
    }
}
