using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentInformationSystem.Migrations
{
    /// <inheritdoc />
    public partial class GradeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_LessonId",
                table: "Grade",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_StudentId",
                table: "Grade",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Lessons_LessonId",
                table: "Grade",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Students_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Lessons_LessonId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Students_StudentId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_LessonId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_StudentId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Grade");
        }
    }
}
