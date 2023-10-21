using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class School3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_students_tbl_notes_NoteID",
                table: "tbl_students");

            migrationBuilder.AlterColumn<int>(
                name: "NoteID",
                table: "tbl_students",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_students_tbl_notes_NoteID",
                table: "tbl_students",
                column: "NoteID",
                principalTable: "tbl_notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_students_tbl_notes_NoteID",
                table: "tbl_students");

            migrationBuilder.AlterColumn<int>(
                name: "NoteID",
                table: "tbl_students",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_students_tbl_notes_NoteID",
                table: "tbl_students",
                column: "NoteID",
                principalTable: "tbl_notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
