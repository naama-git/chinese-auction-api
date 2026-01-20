using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionAPI.Migrations
{
    /// <inheritdoc />
    public partial class categories_prizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prizes_categories_CategoryId",
                table: "prizes");

            migrationBuilder.DropIndex(
                name: "IX_prizes_CategoryId",
                table: "prizes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "prizes");

            migrationBuilder.CreateTable(
                name: "CategoryPrize",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PrizesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPrize", x => new { x.CategoriesId, x.PrizesId });
                    table.ForeignKey(
                        name: "FK_CategoryPrize_categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPrize_prizes_PrizesId",
                        column: x => x.PrizesId,
                        principalTable: "prizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPrize_PrizesId",
                table: "CategoryPrize",
                column: "PrizesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPrize");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "prizes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_prizes_CategoryId",
                table: "prizes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_prizes_categories_CategoryId",
                table: "prizes",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
