using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class School2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tbl_teachers_LessonID",
                table: "tbl_teachers",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_students_NoteID",
                table: "tbl_students",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_notes_LessonID",
                table: "tbl_notes",
                column: "LessonID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_notes_tbl_lessons_LessonID",
                table: "tbl_notes",
                column: "LessonID",
                principalTable: "tbl_lessons",
                principalColumn: "LessonID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_students_tbl_notes_NoteID",
                table: "tbl_students",
                column: "NoteID",
                principalTable: "tbl_notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_teachers_tbl_lessons_LessonID",
                table: "tbl_teachers",
                column: "LessonID",
                principalTable: "tbl_lessons",
                principalColumn: "LessonID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_notes_tbl_lessons_LessonID",
                table: "tbl_notes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_students_tbl_notes_NoteID",
                table: "tbl_students");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_teachers_tbl_lessons_LessonID",
                table: "tbl_teachers");

            migrationBuilder.DropIndex(
                name: "IX_tbl_teachers_LessonID",
                table: "tbl_teachers");

            migrationBuilder.DropIndex(
                name: "IX_tbl_students_NoteID",
                table: "tbl_students");

            migrationBuilder.DropIndex(
                name: "IX_tbl_notes_LessonID",
                table: "tbl_notes");
        }
    }
}
