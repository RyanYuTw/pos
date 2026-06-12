using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class HashPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: "admin",
                column: "Password",
                value: "admin123");
        }
    }
}
