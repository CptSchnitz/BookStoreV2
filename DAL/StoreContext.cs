using Common.Model;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class StoreContext : DbContext
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
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AbstractItem> Items { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ItemGenre> ItemGenres { get; set; }
    }
}
