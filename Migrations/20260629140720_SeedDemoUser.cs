using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class SeedDemoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "EmployeeNo", "IdNo", "Name", "Password", "Status", "UpdateTime", "Updater", "UserGroup", "UserLogin" },
                values: new object[] { "demo", "9999", null, "Demo", "$2b$11$LQ/G.1mJraiSxXjlC8fmLOId22mezggL3EzWCquvb7AwOYb2ay3x2", "1", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "管理者", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: "demo");
        }
    }
}
