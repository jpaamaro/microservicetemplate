using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroserviceTemplate.Infrastructure.Migrations
{
    public partial class AddFact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FactId",
                table: "Incidents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Incidents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "IncidentFact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumberOfPeopleInvolved = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentFact", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_FactId",
                table: "Incidents",
                column: "FactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_IncidentFact_FactId",
                table: "Incidents",
                column: "FactId",
                principalTable: "IncidentFact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_IncidentFact_FactId",
                table: "Incidents");

            migrationBuilder.DropTable(
                name: "IncidentFact");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_FactId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "FactId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Incidents");
        }
    }
}
