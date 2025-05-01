using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentInformationSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Grade",
                table: "Grade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Announcement",
                table: "Announcement");

            migrationBuilder.RenameTable(
                name: "Grade",
                newName: "Grades");

            migrationBuilder.RenameTable(
                name: "Announcement",
                newName: "Announcements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grades",
                table: "Grades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Announcements",
                table: "Announcements",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "Id", "Content", "Date", "Title" },
                values: new object[,]
                {
                    { 1, "Classes start on September 1st. Good luck!", new DateTime(2025, 4, 26, 21, 10, 11, 325, DateTimeKind.Local).AddTicks(3834), "Welcome to the New Semester!" },
                    { 2, "Midterm exams will be held between November 10-15.", new DateTime(2025, 4, 26, 21, 10, 11, 325, DateTimeKind.Local).AddTicks(3847), "Midterm Exams" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "Code", "GradeValue", "LessonId", "StudentId", "StudentNumber" },
                values: new object[,]
                {
                    { 1, "CS101", "A", 1, 1, "S1234567" },
                    { 2, "MATH102", "B+", 2, 2, "S8765432" }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "Code", "Credit", "Name", "Semester" },
                values: new object[,]
                {
                    { 1, "CS101", 4, "Introduction to Computer Science", "1" },
                    { 2, "MATH102", 5, "Calculus I", "1" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "BirthDate", "Email", "Gender", "IdentityNumber", "Name", "PhoneNumber", "StudentNumber", "Surname" },
                values: new object[,]
                {
                    { 1, "123 Main Street", new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.johnson@example.com", "Female", "10987654321", "Alice", "+905551234567", "S1234567", "Johnson" },
                    { 2, "456 Side Avenue", new DateTime(1999, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.williams@example.com", "Male", "19876543210", "Bob", "+905552345678", "S8765432", "Williams" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Email", "IdentityNumber", "Name", "Office", "Surname" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "11223344556", "John", "B12", "Doe" },
                    { 2, "jane.smith@example.com", "22334455667", "Jane", "C34", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IdentityNumber", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "12345678901", "password123", "Admin", "12345678901" },
                    { 2, "10987654321", "password456", "Student", "10987654321" },
                    { 3, "11223344556", "password789", "Teacher", "11223344556" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Grades",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Announcements",
                table: "Announcements");

            migrationBuilder.DeleteData(
                table: "Announcements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Announcements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Grades",
                newName: "Grade");

            migrationBuilder.RenameTable(
                name: "Announcements",
                newName: "Announcement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grade",
                table: "Grade",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Announcement",
                table: "Announcement",
                column: "Id");
        }
    }
}
