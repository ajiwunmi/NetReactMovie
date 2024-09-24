using Microsoft.AspNetCore.Mvc;
using NetReactMovie.Server.Services.Interfaces;

namespace NetReactMovie.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IOmdbClient _clientService;
        private readonly IMovieService _movieService;

        public MovieController(IOmdbClient clientService, IMovieService movieService)
        {
            _clientService = clientService;
            _movieService = movieService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies(string query)
        {
            var results = await _clientService.SearchMoviesAsync(query);

            if (results == null)
            {
                return NotFound("No movies found.");
            }

            return Ok(results);
        }
    }
}
