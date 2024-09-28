using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Models.Responses;

namespace NetReactMovie.Server.Services.Interfaces

{
    public interface IMovieService
    {

      
        /// <summary>
        /// Searches for movies by title using an external API (OMDB).
        /// </summary>
        /// <param name="title">The title of the movie to search for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the queried movie response.</returns>
        Task<QueriedMovieResponse> SearchMoviesByTitleAsync(string title);

        /// <summary>
        /// Retrieves detailed information about a movie by its IMDb ID.
        /// </summary>
        /// <param name="imdbID">The IMDb ID of the movie.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the movie details.</returns>
        Task<MovieDetailResponse> GetMovieDetailsByImdbIdAsync(string imdbID);

        /// <summary>
        /// Retrieves the latest movie search queries made by users.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of latest movie queries.</returns>
        Task<IEnumerable<MovieQuery>> GetLatestSearchQueriesAsync();

        Task<Movie>GetMovieDetailsByIdAsync(int imdID);



    }
}

