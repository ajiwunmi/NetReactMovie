using NetReactMovie.Server.Models.Responses;

namespace NetReactMovie.Server.Data.DTO
{
    public class MovieQueryDto
    {

        public int Id { get; set; }


        public string? Query { get; set; }

        public DateTime SearchTime { get; set; }

        public string? TotalResults { get; set; }

        public string? Response { get; set; }
        public List<MovieDto>? Movies { get; set; }
    }
}
