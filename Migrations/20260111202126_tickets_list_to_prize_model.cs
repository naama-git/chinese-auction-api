using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionAPI.Migrations
{
    /// <inheritdoc />
    public partial class tickets_list_to_prize_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_prizes_PrizeId",
                table: "carts");

            migrationBuilder.DropIndex(
                name: "IX_carts_PrizeId",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "PrizeId",
                table: "carts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrizeId",
                table: "carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_carts_PrizeId",
                table: "carts",
                column: "PrizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_prizes_PrizeId",
                table: "carts",
                column: "PrizeId",
                principalTable: "prizes",
                principalColumn: "Id");
        }
    }
}
