using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PseuduName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    PublisherId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Edition = table.Column<int>(nullable: true),
                    Isbn = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AuthorId = table.Column<int>(nullable: true),
                    Issn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemGenres",
                columns: table => new
                {
                    AbstractItemId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGenres", x => new { x.AbstractItemId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_ItemGenres_Items_AbstractItemId",
                        column: x => x.AbstractItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName", "PseuduName" },
                values: new object[] { 1, "john", "doe", "don joe" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "BrainPower" });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "ContactEmail", "Name" },
                values: new object[] { 1, "publish@example.com", "publisher" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discriminator", "Price", "PublishDate", "PublisherId", "Title", "AuthorId", "Description", "Edition", "Isbn" },
                values: new object[] { 1, "Book", 50m, new DateTime(2020, 1, 13, 13, 34, 38, 357, DateTimeKind.Local).AddTicks(447), 1, "A Book", 1, "this is the description", 1, "978-3-16-148410-0" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Discriminator", "Price", "PublishDate", "PublisherId", "Title", "Issn" },
                values: new object[] { 2, "Journal", 40m, new DateTime(2020, 1, 13, 13, 34, 38, 360, DateTimeKind.Local).AddTicks(560), 1, "journal", "12345631" });

            migrationBuilder.InsertData(
                table: "ItemGenres",
                columns: new[] { "AbstractItemId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ItemGenres",
                columns: new[] { "AbstractItemId", "GenreId" },
                values: new object[] { 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ItemGenres_GenreId",
                table: "ItemGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PublisherId",
                table: "Items",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_AuthorId",
                table: "Items",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemGenres");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
