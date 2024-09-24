using NetReactMovie.Server.Models.Responses;

namespace NetReactMovie.Server.Services.Interfaces
{
    public interface IOmdbClient
    {
        Task<QueriedMovieResponse> SearchMoviesAsync(string title);
    }
}

