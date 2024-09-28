using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Repositories.Interfaces;

namespace NetReactMovie.Server.Repositories.Interfaces
{
    public interface IMovieQueryRepository : IRepository<MovieQuery>
    {
        Task<IEnumerable<MovieQuery>> GetLatestQueriesAsync(int count);
    }
}

