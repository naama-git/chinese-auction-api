using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChineseAuctionAPI.Migrations
{
    /// <inheritdoc />
    public partial class _04012026_change_models_prize_package : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfClassicTickets",
                table: "packages");

            migrationBuilder.RenameColumn(
                name: "NumberOfPremiumTickets",
                table: "packages",
                newName: "NumOfTickets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfTickets",
                table: "packages",
                newName: "NumberOfPremiumTickets");

            migrationBuilder.AddColumn<int>(
                name: "NumOfClassicTickets",
                table: "packages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
