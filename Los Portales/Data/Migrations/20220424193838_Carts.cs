using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Los_Portales.Data.Migrations
{
    public partial class Carts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatId = table.Column<int>(type: "int", nullable: false),
                    PlayId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Seat_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seat",
                        principalColumn: "SeatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_SeatId",
                table: "Cart",
                column: "SeatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");
        }
    }
}
