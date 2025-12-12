using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCurriculum");

            migrationBuilder.DropTable(
                name: "Curriculums");

            migrationBuilder.CreateTable(
                name: "CourseMajor",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    MajorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMajor", x => new { x.CoursesId, x.MajorsId });
                    table.ForeignKey(
                        name: "FK_CourseMajor_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseMajor_Majors_MajorsId",
                        column: x => x.MajorsId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseMajor_MajorsId",
                table: "CourseMajor",
                column: "MajorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseMajor");

            migrationBuilder.CreateTable(
                name: "Curriculums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curriculums_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseCurriculum",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    CurriCulumDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCurriculum", x => new { x.CoursesId, x.CurriCulumDetailsId });
                    table.ForeignKey(
                        name: "FK_CourseCurriculum_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseCurriculum_Curriculums_CurriCulumDetailsId",
                        column: x => x.CurriCulumDetailsId,
                        principalTable: "Curriculums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCurriculum_CurriCulumDetailsId",
                table: "CourseCurriculum",
                column: "CurriCulumDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_MajorId",
                table: "Curriculums",
                column: "MajorId");
        }
    }
}
