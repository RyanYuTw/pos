using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class SeedFakeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 先移除依賴 Orders.No 的 FK / PK，才能修改欄位長度
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_detail_Orders_No",
                table: "Orders_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders_detail",
                table: "Orders_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "Orders_detail",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "No");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders_detail",
                table: "Orders_detail",
                columns: new[] { "No", "SerialNo" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_detail_Orders_No",
                table: "Orders_detail",
                column: "No",
                principalTable: "Orders",
                principalColumn: "No",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "No", "Description", "DiscountPrice", "Image", "Kind", "Mix", "Name", "Price", "Promotion", "Shelfing", "Size", "Status", "Stock", "Unit", "UnitValue", "UpdateTime", "Updater" },
                values: new object[,]
                {
                    { "D0001", null, 80f, null, "01", false, "美式咖啡", 80f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "D0002", null, 100f, null, "01", false, "拿鐵咖啡", 100f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "D0003", null, 60f, null, "01", false, "紅茶", 60f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "D0004", null, 60f, null, "01", false, "綠茶", 60f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "D0005", null, 80f, null, "01", false, "柳橙汁", 80f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "M0001", null, 180f, null, "02", false, "牛肉麵", 180f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "M0002", null, 150f, null, "02", false, "排骨飯", 150f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "M0003", null, 120f, null, "02", false, "炒飯", 120f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "M0004", null, 160f, null, "02", false, "焗烤義大利麵", 160f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "S0001", null, 60f, null, "03", false, "薯條", 60f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "S0002", null, 80f, null, "03", false, "炸雞塊", 80f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "S0003", null, 70f, null, "03", false, "洋蔥圈", 70f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "T0001", null, 120f, null, "04", false, "提拉米蘇", 120f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" },
                    { "T0002", null, 100f, null, "04", false, "草莓蛋糕", 100f, false, true, null, "0", false, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system" }
                });

            migrationBuilder.InsertData(
                table: "Product_kind",
                columns: new[] { "No", "Name" },
                values: new object[,]
                {
                    { "01", "飲料" },
                    { "02", "主食" },
                    { "03", "小點" },
                    { "04", "甜點" }
                });

            migrationBuilder.InsertData(
                table: "Station_host",
                columns: new[] { "StationNo", "HostName" },
                values: new object[,]
                {
                    { "001", "1號收銀機" },
                    { "002", "2號收銀機" }
                });

            migrationBuilder.InsertData(
                table: "Store",
                columns: new[] { "Id", "Address", "Bank", "BankAccount", "BankingDepartment", "BranchNo", "BusinessRegistrationCode", "Chairman", "Email", "Fax", "Name", "Telephone", "Url", "Version" },
                values: new object[] { "MAIN0001", "台北市信義區幸福路100號", null, null, null, "0001", null, "王大明", "info@happyrest.tw", null, "幸福小館", "02-12345678", null, "1.0" });

            migrationBuilder.InsertData(
                table: "Table_setting",
                columns: new[] { "No", "Hall", "Name", "SeatNum", "Waiter" },
                values: new object[,]
                {
                    { "A01", "A區", "A區1號", 2f, null },
                    { "A02", "A區", "A區2號", 2f, null },
                    { "A03", "A區", "A區3號", 4f, null },
                    { "A04", "A區", "A區4號", 4f, null },
                    { "B01", "B區", "B區1號", 4f, null },
                    { "B02", "B區", "B區2號", 4f, null },
                    { "B03", "B區", "B區3號", 6f, null },
                    { "B04", "B區", "B區4號", 6f, null },
                    { "C01", "包廂", "包廂1", 8f, null },
                    { "C02", "包廂", "包廂2", 10f, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "D0001");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "D0002");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "D0003");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "D0004");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "D0005");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "M0001");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "M0002");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "M0003");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "M0004");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "S0001");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "S0002");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "S0003");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "T0001");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "No",
                keyValue: "T0002");

            migrationBuilder.DeleteData(
                table: "Product_kind",
                keyColumn: "No",
                keyValue: "01");

            migrationBuilder.DeleteData(
                table: "Product_kind",
                keyColumn: "No",
                keyValue: "02");

            migrationBuilder.DeleteData(
                table: "Product_kind",
                keyColumn: "No",
                keyValue: "03");

            migrationBuilder.DeleteData(
                table: "Product_kind",
                keyColumn: "No",
                keyValue: "04");

            migrationBuilder.DeleteData(
                table: "Station_host",
                keyColumn: "StationNo",
                keyValue: "001");

            migrationBuilder.DeleteData(
                table: "Station_host",
                keyColumn: "StationNo",
                keyValue: "002");

            migrationBuilder.DeleteData(
                table: "Store",
                keyColumn: "Id",
                keyValue: "MAIN0001");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "A01");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "A02");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "A03");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "A04");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "B01");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "B02");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "B03");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "B04");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "C01");

            migrationBuilder.DeleteData(
                table: "Table_setting",
                keyColumn: "No",
                keyValue: "C02");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_detail_Orders_No",
                table: "Orders_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders_detail",
                table: "Orders_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "Orders",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "Orders_detail",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "No");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders_detail",
                table: "Orders_detail",
                columns: new[] { "No", "SerialNo" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_detail_Orders_No",
                table: "Orders_detail",
                column: "No",
                principalTable: "Orders",
                principalColumn: "No",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
