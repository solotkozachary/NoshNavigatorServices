using System.Collections.Generic;

namespace zs.nn.NoshNavigatorServices.Application.Models
{
    /// <summary>
    /// Defines a paginated result.
    /// </summary>
    /// <typeparam name="T">The type of collection returned.</typeparam>
    public class PaginatedResult<T>
    {
        /// <summary>
        /// The total count of records returned.
        /// </summary>
        public int TotalCount { get { return Results.Count; } }

        /// <summary>
        /// The number of records to return in the page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The current page to fetch.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The collection of resulting records.
        /// </summary>
        public List<T> Results { get; set; } = new List<T>();
    }
}
