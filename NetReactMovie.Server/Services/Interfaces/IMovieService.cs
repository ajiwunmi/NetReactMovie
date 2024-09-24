using NetReactMovie.Server.Models.Entities;

namespace NetReactMovie.Server.Services.Interfaces

{
    public interface IMovieService
    {
        Task<Movie> GetMovieDetailsAsync(int id);

    }
}
