using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionAPI.Migrations
{
    /// <inheritdoc />
    public partial class _23_12_2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prizes_donators_DonorId",
                table: "prizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_donators",
                table: "donators");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "DonatorId",
                table: "prizes");

            migrationBuilder.DropColumn(
                name: "PackagesId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "PrizesId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "PrizesId",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "PrizesId",
                table: "donators");

            migrationBuilder.RenameTable(
                name: "donators",
                newName: "donors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_donors",
                table: "donors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_prizes_donors_DonorId",
                table: "prizes",
                column: "DonorId",
                principalTable: "donors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prizes_donors_DonorId",
                table: "prizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_donors",
                table: "donors");

            migrationBuilder.RenameTable(
                name: "donors",
                newName: "donators");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DonatorId",
                table: "prizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PackagesId",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrizesId",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrizesId",
                table: "carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrizesId",
                table: "donators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_donators",
                table: "donators",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_prizes_donators_DonorId",
                table: "prizes",
                column: "DonorId",
                principalTable: "donators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
