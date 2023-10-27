using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class School4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "NoteID",
                table: "tbl_students",
                newName: "NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_students_NoteID",
                table: "tbl_students",
                newName: "IX_tbl_students_NoteId");

            migrationBuilder.AlterColumn<int>(
                name: "LessonID",
                table: "tbl_teachers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LessonID",
                table: "tbl_notes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "tbl_notes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_notes_StudentID",
                table: "tbl_notes",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_notes_tbl_lessons_LessonID",
                table: "tbl_notes",
                column: "LessonID",
                principalTable: "tbl_lessons",
                principalColumn: "LessonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_notes_tbl_students_StudentID",
                table: "tbl_notes",
                column: "StudentID",
                principalTable: "tbl_students",
                principalColumn: "StudentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_students_tbl_notes_NoteId",
                table: "tbl_students",
                column: "NoteId",
                principalTable: "tbl_notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_teachers_tbl_lessons_LessonID",
                table: "tbl_teachers",
                column: "LessonID",
                principalTable: "tbl_lessons",
                principalColumn: "LessonID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_notes_tbl_lessons_LessonID",
                table: "tbl_notes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_notes_tbl_students_StudentID",
                table: "tbl_notes");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_students_tbl_notes_NoteId",
                table: "tbl_students");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_teachers_tbl_lessons_LessonID",
                table: "tbl_teachers");

            migrationBuilder.DropIndex(
                name: "IX_tbl_notes_StudentID",
                table: "tbl_notes");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "tbl_notes");

            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "tbl_students",
                newName: "NoteID");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_students_NoteId",
                table: "tbl_students",
                newName: "IX_tbl_students_NoteID");

            migrationBuilder.AlterColumn<int>(
                name: "LessonID",
                table: "tbl_teachers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LessonID",
                table: "tbl_notes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_teachers_tbl_lessons_LessonID",
                table: "tbl_teachers",
                column: "LessonID",
                principalTable: "tbl_lessons",
                principalColumn: "LessonID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
