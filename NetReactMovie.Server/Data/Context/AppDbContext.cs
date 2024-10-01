using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetReactMovie.Server.Models.Entities;

namespace NetReactMovie.Server.Data.Context
{
    public class AppDbContext :  IdentityDbContext<User> //DbContext,
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
                
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieQuery> MovieQueries { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users {  get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.MovieQueries)
                .WithMany(mq => mq.Movies)
                .UsingEntity(j => j.ToTable("MovieQueryMovies"));
        }
    }
    
}
