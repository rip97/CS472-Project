using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Los_Portales.Data.Migrations
{
    public partial class Play : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Play",
                columns: table => new
                {
                    PlayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Play", x => x.PlayId);
                    table.ForeignKey(
                        name: "FK_Play_Play_PlayId1",
                        column: x => x.PlayId1,
                        principalTable: "Play",
                        principalColumn: "PlayId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Play_PlayId1",
                table: "Play",
                column: "PlayId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Play");
        }
    }
}
