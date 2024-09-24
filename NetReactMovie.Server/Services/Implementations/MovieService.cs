using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Repositories.Impementations;
using NetReactMovie.Server.Services.Interfaces;

namespace NetReactMovie.Server.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly MovieRepository _movieRepository;

        public MovieService(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;

        }
        public async Task<Movie> GetMovieDetailsAsync(int id)
        {
            return await _movieRepository.GetMovieQueryByIdAsync(id);
        }
    }
}
