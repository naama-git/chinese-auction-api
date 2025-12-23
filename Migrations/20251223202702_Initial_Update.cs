using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prizes_donators_DonatorId",
                table: "prizes");

            migrationBuilder.DropIndex(
                name: "IX_prizes_DonatorId",
                table: "prizes");

            migrationBuilder.AddColumn<int>(
                name: "DonorId",
                table: "prizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_prizes_DonorId",
                table: "prizes",
                column: "DonorId");

            migrationBuilder.AddForeignKey(
                name: "FK_prizes_donators_DonorId",
                table: "prizes",
                column: "DonorId",
                principalTable: "donators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prizes_donators_DonorId",
                table: "prizes");

            migrationBuilder.DropIndex(
                name: "IX_prizes_DonorId",
                table: "prizes");

            migrationBuilder.DropColumn(
                name: "DonorId",
                table: "prizes");

            migrationBuilder.CreateIndex(
                name: "IX_prizes_DonatorId",
                table: "prizes",
                column: "DonatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_prizes_donators_DonatorId",
                table: "prizes",
                column: "DonatorId",
                principalTable: "donators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
