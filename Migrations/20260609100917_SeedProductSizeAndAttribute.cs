using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductSizeAndAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "HOT", "熱" },
                    { "ICE", "冰" },
                    { "WRM", "溫" }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "L", "大" },
                    { "M", "中" },
                    { "S", "小" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attribute",
                keyColumn: "Code",
                keyValue: "HOT");

            migrationBuilder.DeleteData(
                table: "Attribute",
                keyColumn: "Code",
                keyValue: "ICE");

            migrationBuilder.DeleteData(
                table: "Attribute",
                keyColumn: "Code",
                keyValue: "WRM");

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Code",
                keyValue: "L");

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Code",
                keyValue: "M");

            migrationBuilder.DeleteData(
                table: "Size",
                keyColumn: "Code",
                keyValue: "S");
        }
    }
}
