using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Io.TedTalk.Data.Migrations
{
    public partial class add_ted_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 400, nullable: true),
                    Author = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Views = table.Column<long>(type: "INTEGER", nullable: false),
                    Likes = table.Column<long>(type: "INTEGER", nullable: false),
                    Link = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ted", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ted_Title_Author",
                table: "Ted",
                columns: new[] { "Title", "Author" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ted");
        }
    }
}
