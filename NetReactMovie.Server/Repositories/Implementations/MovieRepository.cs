using NetReactMovie.Server.Data.Context;
using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Repositories.Interfaces;

namespace NetReactMovie.Server.Repositories.Impementations
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        private bool _disposed = false;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Movie> GetMovieQueryByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                throw new KeyNotFoundException($"Movie with id {id} was not found.");
            }

            return movie;
        }

        public async Task AddMovieQueryAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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