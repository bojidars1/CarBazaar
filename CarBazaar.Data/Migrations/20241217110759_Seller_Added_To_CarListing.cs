using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seller_Added_To_CarListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "CarListings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                comment: "Seller Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarListings_SellerId",
                table: "CarListings",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarListings_AspNetUsers_SellerId",
                table: "CarListings",
                column: "SellerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarListings_AspNetUsers_SellerId",
                table: "CarListings");

            migrationBuilder.DropIndex(
                name: "IX_CarListings_SellerId",
                table: "CarListings");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "CarListings");
        }
    }
}
