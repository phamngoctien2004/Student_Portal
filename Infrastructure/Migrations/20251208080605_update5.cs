using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSections_SemesterId",
                table: "CourseSections",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSections_Semesters_SemesterId",
                table: "CourseSections",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSections_Semesters_SemesterId",
                table: "CourseSections");

            migrationBuilder.DropIndex(
                name: "IX_CourseSections_SemesterId",
                table: "CourseSections");

            migrationBuilder.DropColumn(
                name: "year",
                table: "Semesters");
        }
    }
}
