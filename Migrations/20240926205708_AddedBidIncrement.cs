using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bidding_platform.Migrations
{
    /// <inheritdoc />
    public partial class AddedBidIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidIncrement",
                table: "Bids");

            migrationBuilder.AddColumn<int>(
                name: "BidIncrement",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidIncrement",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "BidIncrement",
                table: "Bids",
                type: "int",
                nullable: true);
        }
    }
}
