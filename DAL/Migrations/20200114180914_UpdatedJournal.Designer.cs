﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20200114180914_UpdatedJournal")]
    partial class UpdatedJournal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Common.Model.AbstractItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AbstractItem");
                });

            modelBuilder.Entity("Common.Model.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PseuduName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "john",
                            LastName = "doe",
                            PseuduName = "don joe"
                        });
                });

            modelBuilder.Entity("Common.Model.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "BrainPower"
                        });
                });

            modelBuilder.Entity("Common.Model.ItemGenre", b =>
                {
                    b.Property<int>("AbstractItemId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("AbstractItemId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("ItemGenres");

                    b.HasData(
                        new
                        {
                            AbstractItemId = 1,
                            GenreId = 1
                        },
                        new
                        {
                            AbstractItemId = 2,
                            GenreId = 1
                        });
                });

            modelBuilder.Entity("Common.Model.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Publishers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactEmail = "publish@example.com",
                            Name = "publisher"
                        });
                });

            modelBuilder.Entity("Common.Model.Book", b =>
                {
                    b.HasBaseType("Common.Model.AbstractItem");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Edition")
                        .HasColumnType("int");

                    b.Property<string>("Isbn")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AuthorId");

                    b.HasDiscriminator().HasValue("Book");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Price = 50m,
                            PublishDate = new DateTime(2020, 1, 14, 20, 9, 13, 793, DateTimeKind.Local).AddTicks(558),
                            PublisherId = 1,
                            Title = "A Book",
                            AuthorId = 1,
                            Description = "this is the description",
                            Edition = 1,
                            Isbn = "978-3-16-148410-0"
                        });
                });

            modelBuilder.Entity("Common.Model.Journal", b =>
                {
                    b.HasBaseType("Common.Model.AbstractItem");

                    b.Property<string>("Issn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IssueNum")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Journal");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Price = 40m,
                            PublishDate = new DateTime(2020, 1, 14, 20, 9, 13, 795, DateTimeKind.Local).AddTicks(9997),
                            PublisherId = 1,
                            Title = "journal",
                            Issn = "12345631",
                            IssueNum = 1
                        });
                });

            modelBuilder.Entity("Common.Model.AbstractItem", b =>
                {
                    b.HasOne("Common.Model.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.Model.ItemGenre", b =>
                {
                    b.HasOne("Common.Model.AbstractItem", "AbstractItem")
                        .WithMany("ItemGenres")
                        .HasForeignKey("AbstractItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Common.Model.Genre", "Genre")
                        .WithMany("ItemGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Common.Model.Book", b =>
                {
                    b.HasOne("Common.Model.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
