using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHomeDataAccess.Migrations
{
    public partial class CatogoryANDCoursesDetailsANDCoursesUpdaTABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripton",
                table: "Coursess",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(90)",
                oldMaxLength: 90);

            migrationBuilder.AddColumn<int>(
                name: "CategoriesId",
                table: "Coursess",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categoriess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categorie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoriess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoursesDetailss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutCours = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AboutCoursDescription = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    ToApply = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ToApplyDescription = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Certification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CertificationDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Starts = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false),
                    Students = table.Column<int>(type: "int", nullable: false),
                    Assesments = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CourseFee = table.Column<int>(type: "int", nullable: false),
                    CoursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesDetailss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursesDetailss_Coursess_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Coursess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coursess_CategoriesId",
                table: "Coursess",
                column: "CategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesDetailss_CoursesId",
                table: "CoursesDetailss",
                column: "CoursesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coursess_Categoriess_CategoriesId",
                table: "Coursess",
                column: "CategoriesId",
                principalTable: "Categoriess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coursess_Categoriess_CategoriesId",
                table: "Coursess");

            migrationBuilder.DropTable(
                name: "Categoriess");

            migrationBuilder.DropTable(
                name: "CoursesDetailss");

            migrationBuilder.DropIndex(
                name: "IX_Coursess_CategoriesId",
                table: "Coursess");

            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "Coursess");

            migrationBuilder.AlterColumn<string>(
                name: "Descripton",
                table: "Coursess",
                type: "nvarchar(90)",
                maxLength: 90,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(700)",
                oldMaxLength: 700);
        }
    }
}
