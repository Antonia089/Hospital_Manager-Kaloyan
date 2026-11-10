using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Hospital_Manager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "BedsCount", "CreatedOn", "Email", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Kazanlak", 200, new DateTime(2026, 10, 30, 14, 22, 40, 42, DateTimeKind.Local).AddTicks(7753), "hospital1@gmail.com", false, "MBAL Kazanlak", "1234567890" },
                    { 2, "Stara Zagora", 350, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hospital2@gmail.com", false, "UMBAL Stara Zagora", "1261502980" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
