using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_UserCarListing_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCarListings",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User Id"),
                    CarListingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Car Listing Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCarListings", x => new { x.UserId, x.CarListingId });
                    table.ForeignKey(
                        name: "FK_UserCarListings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCarListings_CarListings_CarListingId",
                        column: x => x.CarListingId,
                        principalTable: "CarListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCarListings_CarListingId",
                table: "UserCarListings",
                column: "CarListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCarListings");
        }
    }
}
