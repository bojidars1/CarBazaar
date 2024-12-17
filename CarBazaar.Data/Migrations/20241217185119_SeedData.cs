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
                    { "admin-user", 0, "bfff2856-5811-41ba-9ecc-3f4ba14ca357", new DateTime(2024, 12, 17, 18, 51, 19, 222, DateTimeKind.Utc).AddTicks(1112), "admin@carbazaar.com", false, false, null, "ADMIN@CARBAZAAR.COM", "ADMIN@CARBAZAAR.COM", "AQAAAAIAAYagAAAAEIed9N1FT6eBwCH55Qe7KNWmHzNTYRtr37H9Q72bgc0zzpjuTlLPkSBvW1G7bGkkmg==", null, false, "7b063d85-93b9-4f8b-ab9d-71a1ab190d12", false, "admin@carbazaar.com" },
                    { "user-1", 0, "4bb2aade-a56e-438e-92e9-b2a454c896ad", new DateTime(2024, 12, 17, 18, 51, 19, 284, DateTimeKind.Utc).AddTicks(8455), "bojidar.stoi@gmail.com", false, false, null, "BOJIDAR.STOI@GMAIL.COM", "BOJIDAR.STOI@GMAIL.COM", "AQAAAAIAAYagAAAAEAqc8PsQpDJ9HXimjs2ACwE4rB+vkbHrGNTKWGbqFpjsZvaFdfg7ot8zPL4XGp1HPg==", null, false, "7c2ee22b-88a2-4b2c-b3cb-e669ba0d834f", false, "bojidar.stoi@gmail.com" },
                    { "user-2", 0, "3cf6a4ed-c609-4fdd-ae79-121f6af54261", new DateTime(2024, 12, 17, 18, 51, 19, 341, DateTimeKind.Utc).AddTicks(6566), "john.wick@abv.bg", false, false, null, "JOHN.WICK@ABV.BG", "JOHN.WICK@ABV.BG", "AQAAAAIAAYagAAAAEFjL2jsya2nBV47aXfNHRigFxm/39Vv/awazyoe0idaBIpC9H5NPcaY9WffheMEcMQ==", null, false, "e30fe9e7-12b0-47e5-b824-7b7eb9f480a3", false, "john.wick@abv.bg" }
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
                values: new object[] { new Guid("ef3835be-ca5e-4840-918c-13fb1c8e2039"), new Guid("22222222-2222-2222-2222-222222222222"), "Is the car still available?", "user-2", "user-1", new DateTime(2024, 12, 17, 18, 51, 19, 399, DateTimeKind.Utc).AddTicks(9364) });

            migrationBuilder.InsertData(
                table: "FavoriteCarListings",
                columns: new[] { "CarListingId", "UserId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "user-1" });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "CarListingId", "CreatedAt", "IsRead", "Message", "SenderId", "UserId" },
                values: new object[] { new Guid("143b16bd-d4fe-4b35-bc4e-339718692f8a"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 12, 17, 18, 51, 19, 400, DateTimeKind.Utc).AddTicks(2280), false, "You have a new message from bojidar.stoi@gmail.com.", "user-1", "user-2" });

            migrationBuilder.InsertData(
                table: "UserCarListings",
                columns: new[] { "CarListingId", "UserId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "user-1" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "user-2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user");

            migrationBuilder.DeleteData(
                table: "ChatMessages",
                keyColumn: "Id",
                keyValue: new Guid("ef3835be-ca5e-4840-918c-13fb1c8e2039"));

            migrationBuilder.DeleteData(
                table: "FavoriteCarListings",
                keyColumns: new[] { "CarListingId", "UserId" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "user-1" });

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: new Guid("143b16bd-d4fe-4b35-bc4e-339718692f8a"));

            migrationBuilder.DeleteData(
                table: "UserCarListings",
                keyColumns: new[] { "CarListingId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "user-1" });

            migrationBuilder.DeleteData(
                table: "UserCarListings",
                keyColumns: new[] { "CarListingId", "UserId" },
                keyValues: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "user-2" });

            migrationBuilder.DeleteData(
                table: "CarListings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "CarListings",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-2");
        }
    }
}
