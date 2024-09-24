using System.Text.Json.Serialization;

namespace NetReactMovie.Server.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; } 
        public string? Source { get; set; }

        public string? Value { get; set; }
    }
}
