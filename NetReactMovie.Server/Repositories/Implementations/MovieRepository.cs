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


        public async Task UpdateMovieAsync(int movieId, Movie updatedMovie)
        {
            // Find the movie by ID
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie != null)
            {


                // Update movie properties with the values from updatedMovie
                movie.Title = updatedMovie.Title;
                movie.Year = updatedMovie.Year;
                movie.Rated = updatedMovie.Rated;
                movie.Released = updatedMovie.Released;
                movie.Runtime = updatedMovie.Runtime;
                movie.Genre = updatedMovie.Genre;
                movie.Director = updatedMovie.Director;
                movie.Writer = updatedMovie.Writer;
                movie.Actors = updatedMovie.Actors;
                movie.Plot = updatedMovie.Plot;
                movie.Language = updatedMovie.Language;
                movie.Country = updatedMovie.Country;
                movie.Awards = updatedMovie.Awards;
                movie.Poster = updatedMovie.Poster;
                movie.Ratings = updatedMovie.Ratings;  // Assuming Ratings are handled correctly with relations
                movie.Metascore = updatedMovie.Metascore;
                movie.ImdbRating = updatedMovie.ImdbRating;
                movie.ImdbVotes = updatedMovie.ImdbVotes;
                movie.ImdbID = updatedMovie.ImdbID;
                movie.Type = updatedMovie.Type;
                movie.DVD = updatedMovie.DVD;
                movie.BoxOffice = updatedMovie.BoxOffice;
                movie.Production = updatedMovie.Production;
                movie.Website = updatedMovie.Website;
                movie.Response = updatedMovie.Response;



                // Save changes to the database
                await _context.SaveChangesAsync();
            }
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