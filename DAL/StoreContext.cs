using Common.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemGenre>()
                .HasKey(ig => new { ig.AbstractItemId, ig.GenreId });
            modelBuilder.Entity<ItemGenre>()
                .HasOne(ig => ig.Genre)
                .WithMany(g => g.ItemGenres)
                .HasForeignKey(ig => ig.GenreId);
            modelBuilder.Entity<ItemGenre>()
                .HasOne(ig => ig.AbstractItem)
                .WithMany(ai => ai.ItemGenres)
                .HasForeignKey(ig => ig.AbstractItemId);

            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<AbstractItem> Items { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ItemGenre> ItemGenres { get; set; }


        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Publisher>()
                .HasData(new Publisher { Id = 1, ContactEmail = "publish@example.com", Name = "publisher" });
            modelBuilder.Entity<Author>()
                .HasData(new Author { Id = 1, FirstName = "john", LastName = "doe", PseuduName = "don joe" });
            modelBuilder.Entity<Genre>()
                .HasData(new Genre { Id = 1, Name = "BrainPower" });            
            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    Id = 1,
                    Isbn = "978-3-16-148410-0",
                    Description = "this is the description",
                    Title = "A Book",
                    Edition = 1,
                    Price = 50,
                    AuthorId = 1,
                    PublisherId = 1,
                    PublishDate = DateTime.Now
                });
            modelBuilder.Entity<Journal>()
                .HasData(new Journal
                {
                    Id = 2,
                    Issn = "12345631",
                    Price = 40,
                    PublishDate = DateTime.Now,
                    PublisherId = 1,
                    Title = "journal"
                });
            modelBuilder.Entity<ItemGenre>().HasData(
                new ItemGenre { AbstractItemId = 1, GenreId = 1 },
                new ItemGenre { AbstractItemId = 2, GenreId = 1 }
                );
        }
    }
}
