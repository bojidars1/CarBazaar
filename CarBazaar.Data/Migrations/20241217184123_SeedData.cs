using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin-user", 0, "d022a220-a363-4a57-9b35-0fe69d6c9718", new DateTime(2024, 12, 17, 18, 41, 23, 2, DateTimeKind.Utc).AddTicks(3827), "admin@carbazaar.com", false, false, null, "ADMIN@CARBAZAAR.COM", "ADMIN@CARBAZAAR.COM", "AQAAAAIAAYagAAAAECP1ss1UZyLC398VvaKVGNXFwwl5I3Yc3VXDFvvtEPAoM7rGId2dmxyjgh0//UVnTA==", null, false, "a9cd28ae-f6cb-4643-b70f-bade32f47216", false, "admin@carbazaar.com" },
                    { "user-1", 0, "94fe044d-e007-4fa6-9c5b-f81ac9b42988", new DateTime(2024, 12, 17, 18, 41, 23, 69, DateTimeKind.Utc).AddTicks(3522), "bojidar.stoi@gmail.com", false, false, null, "BOJIDAR.STOI@GMAIL.COM", "BOJIDAR.STOI@GMAIL.COM", "AQAAAAIAAYagAAAAEJ8CVAkKdcmAwm6eB4w9MjyonnlBCDXdRQDG067tSjbzH7andD0zp3WRa4pBqmJ8dA==", null, false, "8c6b97d2-cb92-4961-8565-ce38688e6d57", false, "bojidar.stoi@gmail.com" },
                    { "user-2", 0, "fcaa037a-933d-41be-84ba-ebaedb7b4b31", new DateTime(2024, 12, 17, 18, 41, 23, 131, DateTimeKind.Utc).AddTicks(2242), "john.wick@abv.bg", false, false, null, "JOHN.WICK@ABV.BG", "JOHN.WICK@ABV.BG", "AQAAAAIAAYagAAAAEJcTK+CQeGa09wVevG+uWoc/+ZJjcaEMfRa7mrlyx1R29BYmpviQ1FMY+4X/J6lEFg==", null, false, "b5d2b218-15e2-42ff-854d-875b59ea3ad1", false, "john.wick@abv.bg" }
                });

            migrationBuilder.InsertData(
                table: "CarListings",
                columns: new[] { "Id", "Brand", "Color", "ExtraInfo", "Gearbox", "Horsepower", "ImageURL", "IsDeleted", "Km", "Name", "Price", "ProductionYear", "PublicationDate", "SellerId", "State", "Type" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Toyota", "White", "Well maintained, low mileage.", "Automatic", 130, "https://scene7.toyota.eu/is/image/toyotaeurope/cors0005a_web_2023:Medium-Landscape?ts=1708962012070&resMode=sharp2&op_usm=1.75,0.3,2,0", false, 80000L, "Toyota Corolla", 15000m, 2018, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user-1", "Used", "Sedan" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "BMW", "Black", "Luxury package included.", "Automatic", 300, "https://media.ed.edmunds-media.com/bmw/x5/2025/oem/2025_bmw_x5_4dr-suv_xdrive40i_fq_oem_1_1280.jpg", false, 5000L, "BMW X5", 50000m, 2022, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user-2", "New", "SUV" }
                });

            migrationBuilder.InsertData(
                table: "ChatMessages",
                columns: new[] { "Id", "CarListingId", "Message", "ReceiverId", "SenderId", "Timestamp" },
                values: new object[] { new Guid("9ceb3623-7822-4af8-a7fa-eed8da9f5c9c"), new Guid("22222222-2222-2222-2222-222222222222"), "Is the car still available?", "user-2", "user-1", new DateTime(2024, 12, 17, 18, 41, 23, 193, DateTimeKind.Utc).AddTicks(2536) });

            migrationBuilder.InsertData(
                table: "FavoriteCarListings",
                columns: new[] { "CarListingId", "UserId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "user-1" });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "CarListingId", "CreatedAt", "IsRead", "Message", "SenderId", "UserId" },
                values: new object[] { new Guid("fba8f601-1cfe-4d7e-b60f-e0189942c549"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 12, 17, 18, 41, 23, 193, DateTimeKind.Utc).AddTicks(5964), false, "You have a new message from bojidar.stoi@gmail.com.", "user-1", "user-2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user");

            migrationBuilder.DeleteData(
                table: "CarListings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "Id",
                keyValue: new Guid("9ceb3623-7822-4af8-a7fa-eed8da9f5c9c"));

            migrationBuilder.DeleteData(
                table: "FavoriteCarListings",
                keyColumns: new[] { "CarListingId", "UserId" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "user-1" });

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("fba8f601-1cfe-4d7e-b60f-e0189942c549"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-1");

            migrationBuilder.DeleteData(
                table: "CarListings",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-2");
        }
    }
}
