namespace NetReactMovie.Server.Models.Entities
{
    public class MovieQuery
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = new Movie();

        public DateTime SearchTime { get; set; }
    }
}
