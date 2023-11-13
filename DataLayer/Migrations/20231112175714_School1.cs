using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class School1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_lessons",
                columns: table => new
                {
                    LessonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_lessons", x => x.LessonID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_notes",
                columns: table => new
                {
                    NoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(nullable: false),
                    LessonID = table.Column<int>(nullable: true),
                    FirstExam = table.Column<int>(nullable: false),
                    SecondExam = table.Column<int>(nullable: false),
                    Project = table.Column<int>(nullable: false),
                    AverageNote = table.Column<int>(nullable: false),
                    DidItPass = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_notes", x => x.NoteID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_students",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSurname = table.Column<string>(nullable: true),
                    TCKimlikNo = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_teachers",
                columns: table => new
                {
                    TeacherID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonID = table.Column<int>(nullable: true),
                    NameSurname = table.Column<string>(nullable: true),
                    TCKimlikNo = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_teachers", x => x.TeacherID);
                    table.ForeignKey(
                        name: "FK_tbl_teachers_tbl_lessons_LessonID",
                        column: x => x.LessonID,
                        principalTable: "tbl_lessons",
                        principalColumn: "LessonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_teachers_LessonID",
                table: "tbl_teachers",
                column: "LessonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_notes");

            migrationBuilder.DropTable(
                name: "tbl_students");

            migrationBuilder.DropTable(
                name: "tbl_teachers");

            migrationBuilder.DropTable(
                name: "tbl_lessons");
        }
    }
}
