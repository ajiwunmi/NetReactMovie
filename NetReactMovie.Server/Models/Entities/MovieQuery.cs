using System.Text.Json.Serialization;
using NetReactMovie.Server.Models.Entities;

namespace NetReactMovie.Server.Models.Entities
{
    public class MovieQuery
    {
        public int Id { get; set; }

        //public int MovieId { get; set; }
        public string?  Query { get; set; }

        public DateTime SearchTime { get; set; }

        public string? TotalResults { get; set; }

        public string? Response { get; set; }

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
