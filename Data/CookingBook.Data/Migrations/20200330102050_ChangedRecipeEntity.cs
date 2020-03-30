using Microsoft.EntityFrameworkCore.Migrations;

namespace CookingBook.Data.Migrations
{
    public partial class ChangedRecipeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductNeeded",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNeeded",
                table: "Recipes");
        }
    }
}
