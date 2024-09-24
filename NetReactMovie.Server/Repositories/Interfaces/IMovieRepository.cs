using NetReactMovie.Server.Models.Entities;


namespace NetReactMovie.Server.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> GetMovieQueryByIdAsync(int id);
        Task AddMovieQueryAsync(Movie movie);
        Task SaveChangesAsync();
    }
}











