﻿using System;
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
                    PlayTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Play", x => x.PlayId);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PlayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seat_Play_PlayId",
                        column: x => x.PlayId,
                        principalTable: "Play",
                        principalColumn: "PlayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_PlayId",
                table: "Seat",
                column: "PlayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Play");
        }
    }
}