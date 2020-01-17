using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DAL.Migrations
{
    public partial class AddDiscountSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountAmount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    AuthorId = table.Column<int>(nullable: true),
                    GenreId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    PublisherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discounts_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discounts_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2020, 1, 15, 20, 53, 4, 331, DateTimeKind.Local).AddTicks(3236));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2020, 1, 15, 20, 53, 4, 334, DateTimeKind.Local).AddTicks(2747));

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_AuthorId",
                table: "Discounts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_GenreId",
                table: "Discounts",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_PublisherId",
                table: "Discounts",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2020, 1, 14, 20, 9, 13, 793, DateTimeKind.Local).AddTicks(558));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2020, 1, 14, 20, 9, 13, 795, DateTimeKind.Local).AddTicks(9997));
        }
    }
}
