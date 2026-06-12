using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class AddUserGroupAndFixStationLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Station_log",
                table: "Station_log");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Station_log",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Station_log",
                table: "Station_log",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "User_group",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_group", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User_group",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "0", "系統管理員，擁有全部權限", "管理者" },
                    { "1", "可查看交班報表", "班長" },
                    { "2", "前台收銀操作", "收銀員" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Station_log",
                table: "Station_log");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Station_log");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Station_log",
                table: "Station_log",
                column: "StationNo");
        }
    }
}
