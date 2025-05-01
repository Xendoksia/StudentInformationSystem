using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentInformationSystem.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Announcements",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2025, 4, 26, 21, 43, 17, 415, DateTimeKind.Local).AddTicks(6966));

            migrationBuilder.UpdateData(
                table: "Announcements",
                keyColumn: "Id",
                keyValue: 10,
                column: "Date",
                value: new DateTime(2025, 4, 26, 21, 43, 17, 415, DateTimeKind.Local).AddTicks(6978));

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "BirthDate", "Email", "Gender", "IdentityNumber", "Name", "PhoneNumber", "StudentNumber", "Surname" },
                values: new object[] { 2, "123 Main Street", new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.johnson@example.com", "Female", "10987654321", "Alice", "+905551234567", "S1234567", "Johnson" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Email", "IdentityNumber", "Name", "Office", "Surname" },
                values: new object[] { 3, "john.doe@example.com", "11223344556", "John", "B12", "Doe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Announcements",
                keyColumn: "Id",
                keyValue: 9,
                column: "Date",
                value: new DateTime(2025, 4, 26, 21, 26, 13, 748, DateTimeKind.Local).AddTicks(6511));

            migrationBuilder.UpdateData(
                table: "Announcements",
                keyColumn: "Id",
                keyValue: 10,
                column: "Date",
                value: new DateTime(2025, 4, 26, 21, 26, 13, 748, DateTimeKind.Local).AddTicks(6527));

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "BirthDate", "Email", "Gender", "IdentityNumber", "Name", "PhoneNumber", "StudentNumber", "Surname" },
                values: new object[] { 4, "123 Main Street", new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.johnson@example.com", "Female", "10987654321", "Alice", "+905551234567", "S1234567", "Johnson" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Email", "IdentityNumber", "Name", "Office", "Surname" },
                values: new object[] { 5, "john.doe@example.com", "11223344556", "John", "B12", "Doe" });
        }
    }
}
