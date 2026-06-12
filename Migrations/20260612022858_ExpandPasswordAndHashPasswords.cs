using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class ExpandPasswordAndHashPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(72)",
                maxLength: 72,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: "admin",
                column: "Password",
                value: "$2b$11$iiXbduXveVZcjXrVEfEX2.8uWRJkROdFjx8C351b17bnTFYtehGzu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(72)",
                oldMaxLength: 72);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: "admin",
                column: "Password",
                value: "admin123");
        }
    }
}
