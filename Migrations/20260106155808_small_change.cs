using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionAPI.Migrations
{
    /// <inheritdoc />
    public partial class small_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prizes_categories_CategoryId",
                table: "prizes");

            migrationBuilder.DropColumn(
                name: "IsPremium",
                table: "prizes");

            migrationBuilder.AlterColumn<int>(
            name: "CategoryId",
            table: "prizes",
            type: "int",
            nullable: true, 
            oldClrType: typeof(int),
            oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_prizes_categories_CategoryId",
                table: "prizes",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prizes_categories_CategoryId",
                table: "prizes");

            migrationBuilder.AddColumn<bool>(
                name: "IsPremium",
                table: "prizes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_prizes_categories_CategoryId",
                table: "prizes",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
