using Microsoft.AspNetCore.Mvc;
using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Data.DTO;
using NetReactMovie.Server.Services.Implementations;
using NetReactMovie.Server.Services.Interfaces;

namespace NetReactMovie.Server.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpPost("search")]
        public async  Task<IActionResult> SearchMovies([FromBody] MovieSearchDto movieDto)
        {
           

            if (movieDto.Title == null)
            {
                return NotFound("Movie title field is required.");
            }
            var result = await _movieService.SearchMoviesByTitleAsync(movieDto.Title);

            if (result == null)
            {
                return NotFound("No movies found.");
            }
            return Ok(result);

        }

        [HttpGet("{imdbID:int}")]
        public async Task<ActionResult> GetMovieDetail(int imdbID)
        {
            try
            {
                var result = await _movieService.GetMovieDetailsByIdAsync(imdbID);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestSearchQueries()
        {
            var latestQueries = await _movieService.GetLatestSearchQueriesAsync();
            return Ok(latestQueries);
        }
    }
}
