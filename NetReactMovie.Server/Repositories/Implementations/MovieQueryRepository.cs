using Microsoft.EntityFrameworkCore;
using NetReactMovie.Server.Data.Context;
using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Repositories.Implementations;
using NetReactMovie.Server.Repositories.Interfaces;

namespace NetReactMovie.Server.Repositories.Implementations
{
    public class MovieQueryRepository : Repository<MovieQuery>, IMovieQueryRepository
    {
        public MovieQueryRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<MovieQuery>> GetLatestQueriesAsync(int count)
        {
            return await _context.MovieQueries
                                 .OrderByDescending(mq => mq.SearchTime)
                                 .Take(count)
                                 .ToListAsync();
        }
    }

}
