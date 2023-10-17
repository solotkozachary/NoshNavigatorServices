using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace zs.nn.NoshNavigatorServices.Application.Interfaces.Persistence.Ingredient
{
    /// <summary>
    /// A contract for a service that performs entity queries.
    /// </summary>
    public interface IIngredientPersistenceQueries
    {
        /// <summary>
        /// Retrieve an ingredient by id.
        /// </summary>
        /// <param name="id">The id to search by.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>The entity found.</returns>
        Task<Domain.Entity.Recipe.Ingredient> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve a paginated list of ingredients by name.
        /// </summary>
        /// <param name="name">The name of the ingredient.</param>
        /// <param name="pageSize">The number of records to return in the page.</param>
        /// <param name="currentPage">The current page number to fetch.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>Paginated list of ingredients found.</returns>
        Task<ICollection<Domain.Entity.Recipe.Ingredient>> GetPaginatedIngredientsByName(string name, int pageSize, int currentPage, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve a list of ingredients by recipe Id.
        /// </summary>
        /// <param name="recipeId">The id of the recipe to search by.</param>
        /// <param name="cancellationToken">Propagates process cancellation signal.</param>
        /// <returns>List of ingredients found.</returns>
        Task<ICollection<Domain.Entity.Recipe.Ingredient>> GetIngredientsByRecipeId(Guid recipeId, CancellationToken cancellationToken);
    }
}
