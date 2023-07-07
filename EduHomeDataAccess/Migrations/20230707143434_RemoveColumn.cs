using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHomeDataAccess.Migrations
{
    public partial class RemoveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutCours",
                table: "CoursesDetailss");

            migrationBuilder.DropColumn(
                name: "AboutCoursDescription",
                table: "CoursesDetailss");

            migrationBuilder.DropColumn(
                name: "Certification",
                table: "CoursesDetailss");

            migrationBuilder.DropColumn(
                name: "CertificationDescription",
                table: "CoursesDetailss");

            migrationBuilder.DropColumn(
                name: "ToApply",
                table: "CoursesDetailss");

            migrationBuilder.DropColumn(
                name: "ToApplyDescription",
                table: "CoursesDetailss");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutCours",
                table: "CoursesDetailss",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AboutCoursDescription",
                table: "CoursesDetailss",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Certification",
                table: "CoursesDetailss",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CertificationDescription",
                table: "CoursesDetailss",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToApply",
                table: "CoursesDetailss",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToApplyDescription",
                table: "CoursesDetailss",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");
        }
    }
}
