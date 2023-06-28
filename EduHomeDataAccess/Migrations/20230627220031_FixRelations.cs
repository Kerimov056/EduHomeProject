using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHomeDataAccess.Migrations;

public partial class FixRelations : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Event_Spkears_EvSpeakers_EvSpeakersId",
            table: "Event_Spkears");

        migrationBuilder.DropForeignKey(
            name: "FK_Event_Spkears_Events_EventId",
            table: "Event_Spkears");

        migrationBuilder.DropTable(
            name: "Event_Spkears");

        migrationBuilder.CreateTable(
            name: "EvCompanySpeakers",
            columns: table => new
            {
                EvCompanyId = table.Column<int>(type: "int", nullable: false),
                EvSpeakersId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EvCompanySpeakers", x => new { x.EvCompanyId, x.EvSpeakersId });
                table.ForeignKey(
                    name: "FK_EvCompanySpeakers_EvCompanys_EvCompanyId",
                    column: x => x.EvCompanyId,
                    principalTable: "EvCompanys",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_EvCompanySpeakers_EvSpeakerss_EvSpeakersId",
                    column: x => x.EvSpeakersId,
                    principalTable: "EvSpeakerss",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_EvCompanySpeakers_EvSpeakersId",
            table: "EvCompanySpeakers",
            column: "EvSpeakersId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EvCompanySpeakers");

        migrationBuilder.CreateTable(
            name: "Event_Spkears",
            columns: table => new
            {
                EventId = table.Column<int>(type: "int", nullable: false),
                EvSpeakersId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Event_Spkears", x => new { x.EventId, x.EvSpeakersId });
                table.ForeignKey(
                    name: "FK_Event_Spkears_EvSpeakers_EvSpeakersId",
                    column: x => x.EvSpeakersId,
                    principalTable: "EvSpeakers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Event_Spkears_Events_EventId",
                    column: x => x.EventId,
                    principalTable: "Events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Event_Spkears_EvSpeakersId",
            table: "Event_Spkears",
            column: "EvSpeakersId");
    }
}
