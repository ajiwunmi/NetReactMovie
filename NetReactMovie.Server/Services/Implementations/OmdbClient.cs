using System.Text.Json;
using NetReactMovie.Server.Models.Responses;
using NetReactMovie.Server.Services.Interfaces;

namespace NetReactMovie.Server.Services.Implementations
{
    public class OmdbClient : IOmdbClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<MovieService> _logger;

        public OmdbClient(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<MovieService> logger)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<QueriedMovieResponse?> SearchMoviesAsync(string title)
        {

            var apiKey = _configuration["OMDB:ApiKey"];
            var endpointUrl = $"?apikey={apiKey}&s={title}";
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                using (var client = _clientFactory.CreateClient("HTTPClient"))
                {

                    var response = await client.GetAsync(endpointUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError($"Failed to fetch data from OMDB API. Status code: {response.StatusCode}");
                        return null;
                    }
                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        if (contentStream == null || contentStream.Length == 0)
                        {
                            // Handle the case where the content stream is null or empty
                            return null;
                        }                                                                         
                        return await JsonSerializer.DeserializeAsync<QueriedMovieResponse>(contentStream, jsonOptions);

                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occurred getting all countries: {ex}");
                throw;
            }
        }

    }
}


    