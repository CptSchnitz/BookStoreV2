﻿using Common.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // setting up the many to many relation between genre and item
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

            modelBuilder.Entity<BaseDiscount>().HasDiscriminator(d => d.Discriminator);

            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AbstractItem> Items { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ItemGenre> ItemGenres { get; set; }
        public DbSet<BaseDiscount> Discounts { get; set; }
        public DbSet<AuthorDiscount> PubDiscounts { get; set; }
        public DbSet<GenreDiscount> GenreDiscounts { get; set; }
        public DbSet<PublisherDiscount> AuthorDiscounts { get; set; }
        public DbSet<PublishDateDiscount> DateDiscount { get; set; }

        // random data to populate the db
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Publisher>()
                .HasData(new Publisher { Id = 1, ContactEmail = "publish@example.com", Name = "Random company" });
            modelBuilder.Entity<Author>()
                .HasData(new Author { Id = 1, FirstName = "john", LastName = "doe", PseuduName = "don joe" });
            modelBuilder.Entity<Genre>()
                .HasData(new Genre { Id = 1, Name = "Drama" });
            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    Id = 1,
                    Isbn = "978-3-16-148410-0",
                    Description = "this is the description",
                    Title = "Very interesting book",
                    AmountInStock = 5,
                    Edition = 1,
                    Price = 50,
                    AuthorId = 1,
                    PublisherId = 1,
                    PublishDate = new DateTime(1990, 1, 1)
                });
            modelBuilder.Entity<Journal>()
                .HasData(new Journal
                {
                    Id = 2,
                    Issn = "12345631",
                    Price = 40,
                    AmountInStock = 2,
                    PublishDate = new DateTime(2000, 1, 1),
                    PublisherId = 1,
                    Title = "A very interesting journal",
                    IssueNum = 1
                });
            modelBuilder.Entity<ItemGenre>().HasData(
                new ItemGenre { AbstractItemId = 1, GenreId = 1 },
                new ItemGenre { AbstractItemId = 2, GenreId = 1 }
                );
        }
    }
}
