using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class CarListing_ProductionDate_To_ProductionYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionDate",
                table: "CarListings");

            migrationBuilder.AddColumn<int>(
                name: "ProductionYear",
                table: "CarListings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Car Production Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionYear",
                table: "CarListings");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionDate",
                table: "CarListings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Car Production Date");
        }
    }
}
