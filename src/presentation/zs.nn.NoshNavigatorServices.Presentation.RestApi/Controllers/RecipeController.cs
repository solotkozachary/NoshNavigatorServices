using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using zs.nn.NoshNavigatorServices.Application.Recipe.Commands;
using zs.nn.NoshNavigatorServices.Presentation.RestApi.Models.Requests;
using Swashbuckle.AspNetCore.Annotations;
using zs.nn.NoshNavigatorServices.Application.Exceptions;
using System;
using zs.nn.NoshNavigatorServices.Application.Recipe.Queries;
using zs.nn.NoshNavigatorServices.Application.Models;

namespace zs.nn.NoshNavigatorServices.Presentation.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public RecipeController(
            ILogger<RecipeController> logger,
            IMapper mapper,
            IMediator mediator
            )
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new recipe",
            Description = "Accepts a recipe creation request and persists it to the database.",
            Tags = new[] { "Recipes" }
        )]
        [SwaggerResponse(201, "Recipe created successfully and the URI to the new recipe is returned.", typeof(string))]
        [SwaggerResponse(400, "Bad request", typeof(NoshNavigatorException))]
        [SwaggerResponse(500, "Internal server error", typeof(NoshNavigatorException))]
        public async Task<IActionResult> Create(
            [FromBody, SwaggerParameter("Details for the new recipe.", Required = true)] CreateRecipeRequest request,
            [SwaggerParameter("A token that can be used to request cancellation of the asynchronous operation.", Required = false)] CancellationToken cancellation)
        {
            _logger.LogTrace("Enter CreateRecipe");

            var command = _mapper.Map<CreateRecipeCommand>(request);

            var result = await _mediator.Send(command, cancellation);

            var locationUri = new Uri(Url.Action("Get", new { id = result }));

            _logger.LogTrace("Exit CreateRecipe - RecipeId:{RecipeId}", result);

            return Created(locationUri, result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a recipe by id",
            Description = "Retrieves a recipe from persistence for the supplied identifier.",
            Tags = new[] { "Recipes" }
        )]
        [SwaggerResponse(200, "Recipe entity located and returned.", typeof(Domain.Entity.Recipe.Recipe))]
        [SwaggerResponse(404, "Entity not found", typeof(NoshNavigatorException))]
        [SwaggerResponse(400, "Bad request", typeof(NoshNavigatorException))]
        [SwaggerResponse(500, "Internal server error", typeof(NoshNavigatorException))]
        public async Task<IActionResult> Get(
            [FromRoute, SwaggerParameter("The unique identifier of the recipe.", Required = true)] Guid id,
            [SwaggerParameter("A token that can be used to request cancellation of the asynchronous operation.", Required = false)]  CancellationToken cancellation)
        {
            _logger.LogTrace("Enter GetRecipe - RecipeId:{RecipeId}", id);

            var entity = await _mediator.Send(new GetRecipeByIdQuery { Id = id, MustExist = true }, cancellation);

            _logger.LogTrace("Exit GetRecipe - RecipeId:{RecipeId}", id);

            return Ok(entity);
        }

        [HttpGet("byIngredientName/{name}")]
        [SwaggerOperation(
            Summary = "Get a collection of recipes by ingredient name",
            Description = "Retrieves a collection od recipes from persistence for the supplied ingredient name.",
            Tags = new[] { "Recipes" }
        )]
        [SwaggerResponse(200, "Recipe entities located and returned.", typeof(PaginatedResult<Domain.Entity.Recipe.Recipe>))]
        [SwaggerResponse(404, "No entities found", typeof(NoshNavigatorException))]
        [SwaggerResponse(400, "Bad request", typeof(NoshNavigatorException))]
        [SwaggerResponse(500, "Internal server error", typeof(NoshNavigatorException))]
        public async Task<IActionResult> GetByIngredientName(
            [FromRoute, SwaggerParameter("The ingredient name to search by.", Required = true)] string name,
            [FromQuery, SwaggerParameter("The number of records to return in the page.", Required = true)] int pageSize,
            [FromQuery, SwaggerParameter("The current page number to fetch.", Required = true)] int currentPage,
            [SwaggerParameter("A token that can be used to request cancellation of the asynchronous operation.", Required = false)] CancellationToken cancellation)
        {
            _logger.LogTrace("Enter GetRecipeByIngredientName - IngredientName:{IngredientName}, PageSize:{PageSize}, CurrentPage:{CurrentPage}",
                             name, pageSize, currentPage);

            var result = await _mediator.Send(new GetRecipesByIngredientNameQuery { Name = name, PageSize = pageSize, CurrentPage = currentPage }, cancellation);

            _logger.LogTrace("Exit GetRecipeByIngredientName - IngredientName:{IngredientName}, PageSize:{PageSize}, CurrentPage:{CurrentPage}",
                             name, pageSize, currentPage);

            return Ok(result);
        }
    }
}
