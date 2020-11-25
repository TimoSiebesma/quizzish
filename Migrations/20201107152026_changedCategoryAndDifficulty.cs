using Microsoft.EntityFrameworkCore.Migrations;

namespace Quizzish.Migrations
{
    public partial class changedCategoryAndDifficulty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Challenges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Difficulty",
                table: "Challenges",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Challenges");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Difficulty",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
