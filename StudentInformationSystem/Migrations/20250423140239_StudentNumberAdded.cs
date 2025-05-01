using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentInformationSystem.Migrations
{
    /// <inheritdoc />
    public partial class StudentNumberAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentNumber",
                table: "Students",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Students");
        }
    }
}
