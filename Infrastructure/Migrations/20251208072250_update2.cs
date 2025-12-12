using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Curriculums_CurriculumId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CurriculumId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CurriculumId",
                table: "Courses");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCurriculum");

            migrationBuilder.AddColumn<int>(
                name: "CurriculumId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CurriculumId",
                table: "Courses",
                column: "CurriculumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Curriculums_CurriculumId",
                table: "Courses",
                column: "CurriculumId",
                principalTable: "Curriculums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
