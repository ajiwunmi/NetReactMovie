using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NetReactMovie.Server.Data.Context;
using NetReactMovie.Server.Data.DTO;
using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Models.Responses;
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

        //public async Task<IEnumerable<MovieQuery>> GetLatestQueriesWithMoviesAsync(int count)
        //{
        //    return await _context.MovieQueries
        //                         .Include(mq => mq.Movies)  
        //                         .OrderByDescending(mq => mq.SearchTime)
        //                         .Take(count)
        //                         .ToListAsync();
        //}

        public async Task<IEnumerable<MovieQueryDto>> GetLatestQueriesWithMoviesAsync(int count)
        {
            return await _context.MovieQueries
                                 .Include(mq => mq.Movies)
                                 .OrderByDescending(mq => mq.SearchTime)
                                 .Take(count)
                                 .Select(mq => new MovieQueryDto
                                 {
                                     Id = mq.Id,
                                     SearchTime = mq.SearchTime,
                                     Query = mq.Query,
                                     TotalResults = mq.TotalResults,
                                     Response = mq.Response,
                                     Movies = mq.Movies.Select(m => new MovieDto
                                     {
                                         Id = m.Id,
                                         Title = m.Title,
                                         ImdbID = m.ImdbID,
                                         Year = m.Year,
                                         Type = m.Type,
                                         Poster =m.Poster,

                                     }).ToList()
                                 })
                                 .ToListAsync();
        }

       


    }

}

