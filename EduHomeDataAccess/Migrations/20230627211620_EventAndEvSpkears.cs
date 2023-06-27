using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHomeDataAccess.Migrations
{
    public partial class EventAndEvSpkears : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Categoriess_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categoriess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvSpeakerss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvSpeakerss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventDetailss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Decsription = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetailss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventDetailss_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvCompanySpeakerss",
                columns: table => new
                {
                    EvCompanyId = table.Column<int>(type: "int", nullable: false),
                    EvSpeakersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvCompanySpeakerss", x => new { x.EvSpeakersId, x.EvCompanyId });
                    table.ForeignKey(
                        name: "FK_EvCompanySpeakerss_EvCompanys_EvCompanyId",
                        column: x => x.EvCompanyId,
                        principalTable: "EvCompanys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvCompanySpeakerss_EvSpeakerss_EvSpeakersId",
                        column: x => x.EvSpeakersId,
                        principalTable: "EvSpeakerss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event_Spkearss",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EvSpeakersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Spkearss", x => new { x.EvSpeakersId, x.EventId });
                    table.ForeignKey(
                        name: "FK_Event_Spkearss_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Spkearss_EvSpeakerss_EvSpeakersId",
                        column: x => x.EvSpeakersId,
                        principalTable: "EvSpeakerss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvCompanySpeakerss_EvCompanyId",
                table: "EvCompanySpeakerss",
                column: "EvCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Spkearss_EventId",
                table: "Event_Spkearss",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventDetailss_EventId",
                table: "EventDetailss",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoriesId",
                table: "Events",
                column: "CategoriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvCompanySpeakerss");

            migrationBuilder.DropTable(
                name: "Event_Spkearss");

            migrationBuilder.DropTable(
                name: "EventDetailss");

            migrationBuilder.DropTable(
                name: "EvCompanys");

            migrationBuilder.DropTable(
                name: "EvSpeakerss");

            migrationBuilder.DropTable(
                name: "Events");

           

        }
    }
}
