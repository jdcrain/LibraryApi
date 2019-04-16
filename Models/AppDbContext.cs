using System;
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
            var connectionString = "Host=localhost;database=libraryapi;";
            var databaseUrlEnv = Environment.GetEnvironmentVariable("DATABASE_URL");

            if (databaseUrlEnv != null)
            {
                Uri.TryCreate(databaseUrlEnv, UriKind.Absolute, out var uri);
                var host = uri.Host;
                var port = uri.Port;
                var username = uri.UserInfo.Split(':')[0];
                var password = uri.UserInfo.Split(':')[1];
                var database = uri.LocalPath.Substring(1);
                connectionString = $"host={host};port={port};username={username};password={password};database={database};sslmode=Prefer;Trust Server Certificate=true";
            }

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Author>()
                .HasMany(e => e.Books)
                .WithOne(b => b.Author)
                .OnDelete(DeleteBehavior.Restrict);
        }

        // Tell Entity Framework about the Author model and that it should be store in the DB
        //  a migration was created to store the Author data in a table
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
    }
}