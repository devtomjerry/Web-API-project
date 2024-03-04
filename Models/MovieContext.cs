using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Models
    {
        public class MovieContext : DbContext
        {
        public MovieContext(DbContextOptions<MovieContext> options)
    : base(options)
        {
        }


        public DbSet<Movie> Movie { get; set; }

    }
}
