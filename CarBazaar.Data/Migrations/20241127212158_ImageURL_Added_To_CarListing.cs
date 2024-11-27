using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageURL_Added_To_CarListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "CarListings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Image URL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "CarListings");
        }
    }
}
