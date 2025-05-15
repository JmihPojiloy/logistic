using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logistics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderAndRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostAmount",
                table: "Routes",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Routes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostAmount",
                table: "Orders",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Orders",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostAmount",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "CostAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Orders");
        }
    }
}
