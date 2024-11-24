using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Car Listing Identifier"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Car Listing Name"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Car Type"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Car Brand"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Car Price"),
                    Gearbox = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Car Gearbox"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Car State"),
                    Km = table.Column<long>(type: "bigint", nullable: false, comment: "Car KM"),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Car Production Date"),
                    Horsepower = table.Column<int>(type: "int", nullable: false, comment: "Car Horsepower"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Car Color"),
                    ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Extra Car Info"),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Listing Publication Date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarListings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarListings");
        }
    }
}
