using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCartItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prizes_carts_CartId",
                table: "prizes");

            migrationBuilder.DropIndex(
                name: "IX_prizes_CartId",
                table: "prizes");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "prizes");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    PrizeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_carts_CartId",
                        column: x => x.CartId,
                        principalTable: "carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_prizes_PrizeId",
                        column: x => x.PrizeId,
                        principalTable: "prizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_PrizeId",
                table: "CartItems",
                column: "PrizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "prizes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_prizes_CartId",
                table: "prizes",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_prizes_carts_CartId",
                table: "prizes",
                column: "CartId",
                principalTable: "carts",
                principalColumn: "Id");
        }
    }
}
