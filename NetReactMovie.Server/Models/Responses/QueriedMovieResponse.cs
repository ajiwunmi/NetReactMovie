using System.Text.Json.Serialization;
using System.Collections.Generic;
using NetReactMovie.Server.Models.Responses;

namespace NetReactMovie.Server.Models.Responses
{
    public class QueriedMovieResponse
    {
        [JsonPropertyName("Search")]
        public List<MovieDetailResponse> Search { get; set; } = new List<MovieDetailResponse>();        

        [JsonPropertyName("totalResults")]
        public string? TotalResults { get; set; }

        [JsonPropertyName("Response")]
        public string? Response { get; set; }
    }
}
