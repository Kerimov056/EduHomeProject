using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHomeDataAccess.Migrations
{
    public partial class Evets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hecne");

            migrationBuilder.CreateTable(
                name: "Eventss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(46)", maxLength: 46, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speakerss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakerss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventsDetailss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1300)", maxLength: 1300, nullable: false),
                    EventsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsDetailss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventsDetailss_Eventss_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Eventss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventsDetails",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    SpeakersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsDetails", x => new { x.EventsId, x.SpeakersId });
                    table.ForeignKey(
                        name: "FK_EventsDetails_Eventss_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Eventss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsDetails_Speakerss_SpeakersId",
                        column: x => x.SpeakersId,
                        principalTable: "Speakerss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventsDetails_SpeakersId",
                table: "EventsDetails",
                column: "SpeakersId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsDetailss_EventsId",
                table: "EventsDetailss",
                column: "EventsId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsDetails");

            migrationBuilder.DropTable(
                name: "EventsDetailss");

            migrationBuilder.DropTable(
                name: "Speakerss");

            migrationBuilder.DropTable(
                name: "Eventss");

            migrationBuilder.CreateTable(
                name: "Hecne",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hecne", x => x.id);
                });
        }
    }
}
