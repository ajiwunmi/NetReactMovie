using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using NetReactMovie.Server.Data.DTO;
using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Models.Responses;
using NetReactMovie.Server.Repositories.Impementations;
using NetReactMovie.Server.Repositories.Interfaces;
using NetReactMovie.Server.Services.Interfaces;

namespace NetReactMovie.Server.Services.Implementations
{

    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieQueryRepository _movieQueryRepository;
        private readonly IOmdbClient _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public MovieService(
            IMovieRepository movieRepository, 
            IMovieQueryRepository movieQueryRepository, 
            IOmdbClient httpClientFactory, 
            IConfiguration configuration,
            IMemoryCache cache
            )
        {
            _movieRepository = movieRepository;
            _movieQueryRepository = movieQueryRepository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<QueriedMovieResponse> SearchMoviesByTitleAsync(string title)
        {
            var cacheKey = $"MovieSearch_{title}";
            var queryString = $"s={title}";

            // First, let's check if we've already searched for this movie
            if (_cache.TryGetValue(cacheKey, out QueriedMovieResponse cachedResponse))
            {

                if (cachedResponse != null)
                {
                    return cachedResponse;
                }
            }

            var queriedMovieResponse = await _httpClientFactory.GetOmdbDataAsync<QueriedMovieResponse>(queryString);
            if (queriedMovieResponse != null)
            {

                // Cache the response for faster future searches
                _cache.Set(cacheKey, queriedMovieResponse, TimeSpan.FromMinutes(10));

                // Log this search into the database
                await SaveSearchQuery(title, queriedMovieResponse);

                return queriedMovieResponse;
            }
            else
            {
                throw new Exception("Movie search failed. Please try again.");
            }
           
        }

        public async Task<Movie> GetMovieDetailsByIdAsync(int imdbID)
        {
            var cacheKey = $"MovieDetailsByID_{imdbID}";
            //var queryString = $"i={imdbID}";

            // First check the cache for movie details
            if (_cache.TryGetValue(cacheKey, out Movie cachedDetails))
            {
                return cachedDetails;
            }


            var movieDetail = await _movieRepository.GetByIdAsync(imdbID);

            if (movieDetail != null)
            {
                // Cache the movie details for quick future access
                _cache.Set(cacheKey, movieDetail, TimeSpan.FromMinutes(10));

                return movieDetail;
            }
            else
            {
                throw new Exception("Failed to fetch movie details.");
            }
        }

        public async Task<MovieDetailResponse> GetMovieDetailsByImdbIdAsync(string imdbID)
        {
            var cacheKey = $"MovieDetails_{imdbID}";
            var queryString = $"i={imdbID}";

            // First check the cache for movie details
            if (_cache.TryGetValue(cacheKey, out MovieDetailResponse cachedDetails))
            {
                return cachedDetails;
            }
            
                
            var movieDetailResponse = await _httpClientFactory.GetOmdbDataAsync<MovieDetailResponse>(queryString);
           
            if (movieDetailResponse != null)
            {
                // Cache the movie details for quick future access
                _cache.Set(cacheKey, movieDetailResponse, TimeSpan.FromMinutes(10));

                return movieDetailResponse;
            }
            else
            {
                throw new Exception("Failed to fetch movie details.");
            }
        }

        public async Task<IEnumerable<MovieQueryDto>> GetLatestSearchQueriesAsync()
        {
            var cacheKey = "LatestMovieQueries";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<MovieQueryDto> cachedQueries))
            {
                return cachedQueries;
            }

            // Fetch the latest queries from the database
            //var latestQueries = await _movieQueryRepository.GetLatestQueriesAsync(5);
            var latestQueries = await _movieQueryRepository.GetLatestQueriesWithMoviesAsync(5);

            // Cache the results for a brief time to optimize performance
            _cache.Set(cacheKey, latestQueries, TimeSpan.FromMinutes(5));

            return latestQueries;
        }


        private async Task SaveSearchQuery(string query, QueriedMovieResponse response)
        {
            var movieQuery = new MovieQuery
            {
                Query = query,
                SearchTime = DateTime.UtcNow,
                TotalResults = response.TotalResults,
                Response = response.Response
            };

            foreach (var movie in response.Search)
            {

                var existingMovie =  await _movieRepository.GetByImdbIDAsync(movie.ImdbID);

                if (existingMovie.ImdbID == null && !string.IsNullOrWhiteSpace(movie.ImdbID))
                {
                    var newMovie = new Movie
                    {
                        Title = movie.Title,
                        Year = movie.Year,
                        ImdbID = movie.ImdbID,
                        Type = movie.Type,
                        Poster = movie.Poster
                    };
                    await _movieRepository.AddAsync(newMovie);
                    movieQuery.Movies.Add(newMovie);
                }

                //movieQuery.Movies.Add(existingMovie);
            }

            await _movieQueryRepository.AddAsync(movieQuery);

            // Remove oldest entries if more than 5
            var allQueries = await _movieQueryRepository.GetAllAsync();
            if (allQueries.Count() > 5)
            {
                var queriesToRemove = allQueries.OrderBy(q => q.SearchTime).Take(allQueries.Count() - 5);
                foreach (var oldQuery in queriesToRemove)
                {
                    await _movieQueryRepository.DeleteAsync(oldQuery.Id);
                }
            }
        }
    }

}