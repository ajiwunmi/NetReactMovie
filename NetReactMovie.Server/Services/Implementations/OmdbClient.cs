using System.Text.Json;
using NetReactMovie.Server.Services.Interfaces;

namespace NetReactMovie.Server.Services.Implementations
{
    public class OmdbClient : IOmdbClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<OmdbClient> _logger;

        public OmdbClient(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<OmdbClient> logger)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _logger = logger;
        }

      
        public async Task<T> GetOmdbDataAsync<T>(string queryString) where T : new()
        {
            var apiKey = _configuration["OMDB:ApiKey"];
            var endpointUrl = $"?apikey={apiKey}&{queryString}";
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                using (var client = _clientFactory.CreateClient("HTTPClient"))
                {
                    try
                    {
                        var response = await client.GetAsync(endpointUrl);

                        if (!response.IsSuccessStatusCode)
                        {
                            _logger.LogError($"Failed to fetch data from OMDB API. Status code: {response.StatusCode}");
                            throw new HttpRequestException("Failed to fetch data from OMDB API.");
                        }

                        using (var contentStream = await response.Content.ReadAsStreamAsync())
                        {
                            if (contentStream == null || contentStream.Length == 0)
                            {
                                _logger.LogError("Empty contentStream returned from OMDB API.");
                                return new T(); // Return an empty instance of the response model
                            }

                            var result = await JsonSerializer.DeserializeAsync<T>(contentStream, jsonOptions);

                            return result ?? new T(); // Return deserialized result or a new instance if null
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Empty contentStream returned from OMDB API.");
                        throw new Exception($"Oops! Something went wrong : {ex.Message}");
                    }
                    
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occurred when fetching data from OMDB API: {ex.Message}");
                throw new HttpRequestException($"An error occurred when fetching data from OMDB API: {ex.Message}");
            }
        }

    }
}










//using System.Drawing;
//using System.Text.Json;
//using NetReactMovie.Server.Models.Responses;
//using NetReactMovie.Server.Services.Interfaces;

//namespace NetReactMovie.Server.Services.Implementations
//{
//    public class OmdbClient : IOmdbClient
//    {
//        private readonly IConfiguration _configuration;
//        private readonly IHttpClientFactory _clientFactory;
//        private readonly ILogger<MovieService> _logger;

//        public OmdbClient(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<MovieService> logger)
//        {
//            _configuration = configuration;
//            _clientFactory = clientFactory;
//            _logger = logger;
//        }

//        public async Task<QueriedMovieResponse> SearchMoviesAsync(string queryString, string queryType)
//        {
//            var queryResponse = new QueriedMovieResponse();
//            var apiKey = _configuration["OMDB:ApiKey"];

//            var endpointUrl = $"?apikey={apiKey}&{queryString}";
//            var jsonOptions = new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            };
//            try
//            {
//                using (var client = _clientFactory.CreateClient("HTTPClient"))
//                {

//                    var response = await client.GetAsync(endpointUrl);

//                    if (!response.IsSuccessStatusCode)
//                    {
//                        _logger.LogError($"Failed to fetch data from OMDB API. Status code: {response.StatusCode}");
//                        throw new Exception("Failed to fetch movie data.");
//                    }
//                    using (var contentStream = await response.Content.ReadAsStreamAsync())
//                    {
//                        if (contentStream == null || contentStream.Length == 0)
//                        {
//                            _logger.LogError($"An error occurred: an empty contentStream returned");
//                            return queryResponse;
//                        }
//                        switch (queryType)
//                        {
//                            case "serach":
//                                return await JsonSerializer.DeserializeAsync<QueriedMovieResponse>(contentStream, jsonOptions) ?? queryResponse;

//                            case "detail":
//                                return await JsonSerializer.DeserializeAsync<QueriedMovieResponse>(contentStream, jsonOptions) ?? queryResponse;
//                        }



//                    }

//                }
//            }
//            catch (HttpRequestException ex)
//            {
//                _logger.LogError($"An error occurred getting all movies queried {ex}");
//                throw;
//            }
//        }

//    }
//}


