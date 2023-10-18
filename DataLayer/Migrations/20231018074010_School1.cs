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
                    LessonName = table.Column<string>(nullable: false)
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
                    LessonID = table.Column<int>(nullable: false),
                    FirstExam = table.Column<int>(nullable: true),
                    SecondExam = table.Column<int>(nullable: true),
                    Project = table.Column<int>(nullable: true),
                    AverageNote = table.Column<int>(nullable: true),
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
                    NoteID = table.Column<int>(nullable: false),
                    NameSurname = table.Column<string>(nullable: false),
                    TCKimlikNo = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
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
                    LessonID = table.Column<int>(nullable: false),
                    NameSurname = table.Column<string>(nullable: false),
                    TCKimlikNo = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_teachers", x => x.TeacherID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_lessons");

            migrationBuilder.DropTable(
                name: "tbl_notes");

            migrationBuilder.DropTable(
                name: "tbl_students");

            migrationBuilder.DropTable(
                name: "tbl_teachers");
        }
    }
}
