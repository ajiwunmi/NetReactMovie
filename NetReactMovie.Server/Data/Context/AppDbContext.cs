using Microsoft.EntityFrameworkCore;
using NetReactMovie.Server.Models.Entities;

namespace NetReactMovie.Server.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
                
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieQuery> MovieQueries { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
