using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_FavoriteCarListing_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteCarListings",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User Id"),
                    CarListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Car Listing Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteCarListings", x => new { x.UserId, x.CarListingId });
                    table.ForeignKey(
                        name: "FK_FavoriteCarListings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteCarListings_CarListings_CarListingId",
                        column: x => x.CarListingId,
                        principalTable: "CarListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteCarListings_CarListingId",
                table: "FavoriteCarListings",
                column: "CarListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteCarListings");
        }
    }
}
