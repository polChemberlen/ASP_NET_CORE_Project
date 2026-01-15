using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "HashPassword");

            migrationBuilder.AddColumn<string>(
                name: "AnimalType",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalType",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "HashPassword",
                table: "Users",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Services",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
