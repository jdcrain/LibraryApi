using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace LibraryApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseNpgsql("Host=localhost;database=libraryapi");
        }

        // Tell Entity Framework about the Author model and that it should be store in the DB
        //  a migration was created to store the Author data in a table
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}