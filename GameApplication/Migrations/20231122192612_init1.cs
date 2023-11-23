using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameApplication.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Developer = table.Column<string>(type: "text", nullable: false),
                    Editor = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: false),
                    PositiveReviews = table.Column<int>(type: "integer", nullable: false),
                    PositiveReviewsList = table.Column<List<string>>(type: "text[]", nullable: false),
                    NegativeReviews = table.Column<int>(type: "integer", nullable: false),
                    NegativeReviewsList = table.Column<List<string>>(type: "text[]", nullable: false),
                    TimeItWasEdited = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
