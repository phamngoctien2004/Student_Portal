using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "year",
                table: "Semesters",
                newName: "Year");

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Semesters",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "CourseSections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "CourseSections");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Semesters",
                newName: "year");
        }
    }
}
