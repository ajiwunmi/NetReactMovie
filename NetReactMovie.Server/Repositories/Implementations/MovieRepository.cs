using System;
using Microsoft.EntityFrameworkCore;
using NetReactMovie.Server.Data.Context;
using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Models.Responses;
using NetReactMovie.Server.Repositories.Implementations;
using NetReactMovie.Server.Repositories.Interfaces;
namespace NetReactMovie.Server.Repositories.Impementations
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
      
        private bool _disposed = false;

        public MovieRepository(AppDbContext context) : base(context) { }

        public async Task<Movie> GetByImdbIDAsync(string? imdbID)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(m => m.ImdbID == imdbID);
            if (result == null)
            {
                //throw new KeyNotFoundException($"Movie with ID {imdbID} was not found.");

                return new Movie();  
            }
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}