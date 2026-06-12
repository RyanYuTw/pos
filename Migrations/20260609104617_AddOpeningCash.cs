using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class AddOpeningCash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "OpeningCash",
                table: "Station_log",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "OpeningCash",
                table: "Hand_over",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningCash",
                table: "Station_log");

            migrationBuilder.DropColumn(
                name: "OpeningCash",
                table: "Hand_over");
        }
    }
}
