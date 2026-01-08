using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionAPI.Migrations
{
    /// <inheritdoc />
    public partial class change_modelprize_model_package : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_packages_orders_OrderId",
                table: "packages");

            migrationBuilder.DropForeignKey(
                name: "FK_prizes_orders_OrderId",
                table: "prizes");

            migrationBuilder.DropIndex(
                name: "IX_prizes_OrderId",
                table: "prizes");

            migrationBuilder.DropIndex(
                name: "IX_packages_OrderId",
                table: "packages");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "prizes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "packages");

            migrationBuilder.AddColumn<int>(
                name: "PrizeId",
                table: "carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderPackage",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    PackagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPackage", x => new { x.OrdersId, x.PackagesId });
                    table.ForeignKey(
                        name: "FK_OrderPackage_orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPackage_packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPrize",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    PrizesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPrize", x => new { x.OrdersId, x.PrizesId });
                    table.ForeignKey(
                        name: "FK_OrderPrize_orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPrize_prizes_PrizesId",
                        column: x => x.PrizesId,
                        principalTable: "prizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carts_PrizeId",
                table: "carts",
                column: "PrizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPackage_PackagesId",
                table: "OrderPackage",
                column: "PackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPrize_PrizesId",
                table: "OrderPrize",
                column: "PrizesId");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_prizes_PrizeId",
                table: "carts",
                column: "PrizeId",
                principalTable: "prizes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_prizes_PrizeId",
                table: "carts");

            migrationBuilder.DropTable(
                name: "OrderPackage");

            migrationBuilder.DropTable(
                name: "OrderPrize");

            migrationBuilder.DropIndex(
                name: "IX_carts_PrizeId",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "PrizeId",
                table: "carts");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "prizes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "packages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_prizes_OrderId",
                table: "prizes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_packages_OrderId",
                table: "packages",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_packages_orders_OrderId",
                table: "packages",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_prizes_orders_OrderId",
                table: "prizes",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id");
        }
    }
}
