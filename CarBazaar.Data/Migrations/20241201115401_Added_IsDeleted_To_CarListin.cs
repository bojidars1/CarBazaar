using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_IsDeleted_To_CarListin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CarListings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CarListings");
        }
    }
}
