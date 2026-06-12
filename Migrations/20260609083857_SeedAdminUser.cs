using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "EmployeeNo", "IdNo", "Name", "Password", "Status", "UpdateTime", "Updater", "UserGroup", "UserLogin" },
                values: new object[] { "admin", "0001", null, "管理者", "admin123", "0", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "管理者", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: "admin");
        }
    }
}
