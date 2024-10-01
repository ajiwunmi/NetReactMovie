using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Models.Responses;
using NetReactMovie.Server.Repositories.Interfaces;


namespace NetReactMovie.Server.Repositories.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<Movie> GetByImdbIDAsync(string? imdbID);

        Task UpdateMovieAsync(int movieId, Movie updatedMovie);
    }
}










