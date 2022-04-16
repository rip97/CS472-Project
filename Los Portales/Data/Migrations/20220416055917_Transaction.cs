using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Los_Portales.Data.Migrations
{
    public partial class Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Seat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfTickets = table.Column<int>(type: "int", nullable: false),
                    CreditCardNumber = table.Column<long>(type: "bigint", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityCode = table.Column<int>(type: "int", nullable: false),
                    NameOnCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_TransactionId",
                table: "Seat",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Transaction_TransactionId",
                table: "Seat",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Transaction_TransactionId",
                table: "Seat");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Seat_TransactionId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Seat");
        }
    }
}
