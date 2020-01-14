using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdatedJournal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssueNum",
                table: "Items",
                nullable: true);

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
                columns: new[] { "PublishDate", "IssueNum" },
                values: new object[] { new DateTime(2020, 1, 14, 20, 9, 13, 795, DateTimeKind.Local).AddTicks(9997), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueNum",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2020, 1, 13, 13, 34, 38, 357, DateTimeKind.Local).AddTicks(447));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishDate",
                value: new DateTime(2020, 1, 13, 13, 34, 38, 360, DateTimeKind.Local).AddTicks(560));
        }
    }
}
