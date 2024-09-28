namespace NetReactMovie.Server.Services.Interfaces
{
    public interface IOmdbClient
    {
        Task<T> GetOmdbDataAsync<T>(string queryString) where T : new();
    }
}



/// <summary>
/// Searches for movies by title, year, etc.
/// </summary>
/// <param name="queryString">The search query string.</param>
/// <returns>QueriedMovieResponse with the movie list.</returns>
//public async Task<QueriedMovieResponse> SearchMoviesAsync(string queryString)
//{
//    return await GetOmdbDataAsync<QueriedMovieResponse>(queryString);
//}

/// <summary>
/// Fetches movie details by IMDb ID.
/// </summary>
/// <param name="imdbId">The IMDb ID of the movie.</param>
/// <returns>QueriedMovieByImdbIDResponse with movie details.</returns>
//public async Task<QueriedMovieByImdbIDResponse> GetMovieDetailsByImdbIdAsync(string imdbId)
//{
//    var queryString = $"i={imdbId}";
//    return await GetOmdbDataAsync<QueriedMovieByImdbIDResponse>(queryString);
//}

/// <summary>
/// Searches for movies by year.
/// </summary>
/// <param name="year">The year to search for.</param>
/// <returns>QueriedMovieByYearResponse with the movie list for that year.</returns>
//public async Task<QueriedMovieByYearResponse> SearchMoviesByYearAsync(string year)
//{
//    var queryString = $"y={year}";
//    return await GetOmdbDataAsync<QueriedMovieByYearResponse>(queryString);
//}