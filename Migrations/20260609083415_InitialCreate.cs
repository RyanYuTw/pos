using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Authority",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UserGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authority", x => new { x.UserId, x.SystemId });
                });

            migrationBuilder.CreateTable(
                name: "Business_hours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeInterval = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Week = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EndTime = table.Column<float>(type: "real", nullable: true),
                    MealTimes = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business_hours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupons_list",
                columns: table => new
                {
                    OrderNo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    CouponsNo = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons_list", x => new { x.OrderNo, x.CouponsNo });
                });

            migrationBuilder.CreateTable(
                name: "Factory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Chairman = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BusinessRegistrationCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Bank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankingDepartment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hand_over",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    HandoverDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrandTotal = table.Column<float>(type: "real", nullable: false),
                    ServiceCharge = table.Column<float>(type: "real", nullable: false),
                    Allowance = table.Column<float>(type: "real", nullable: false),
                    Cash = table.Column<float>(type: "real", nullable: true),
                    CreditCard = table.Column<float>(type: "real", nullable: true),
                    Point = table.Column<float>(type: "real", nullable: true),
                    Coupons = table.Column<float>(type: "real", nullable: true),
                    HandOverUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hand_over", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "Maintain_log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DbName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintain_log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mix",
                columns: table => new
                {
                    MixNo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mix", x => new { x.MixNo, x.No });
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StationNo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    HandOverNo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TableNo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    SubTotal = table.Column<float>(type: "real", nullable: false),
                    Allowance = table.Column<float>(type: "real", nullable: false),
                    ServiceCharge = table.Column<float>(type: "real", nullable: false),
                    GrandTotal = table.Column<float>(type: "real", nullable: false),
                    Cash = table.Column<float>(type: "real", nullable: true),
                    CreditCard = table.Column<float>(type: "real", nullable: true),
                    Point = table.Column<float>(type: "real", nullable: true),
                    Coupons = table.Column<float>(type: "real", nullable: true),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Change = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceDisplay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    UnitValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Size = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    DiscountPrice = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Shelfing = table.Column<bool>(type: "bit", nullable: false),
                    Promotion = table.Column<bool>(type: "bit", nullable: false),
                    Mix = table.Column<bool>(type: "bit", nullable: false),
                    Stock = table.Column<bool>(type: "bit", nullable: false),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "Product_Attribute",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Attribute", x => new { x.No, x.Code });
                });

            migrationBuilder.CreateTable(
                name: "Product_kind",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_kind", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "Promotion_duration",
                columns: table => new
                {
                    No = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Week = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion_duration", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "Promotion_product",
                columns: table => new
                {
                    No = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionNo = table.Column<int>(type: "int", nullable: false),
                    ProductNo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: true),
                    MemberOnly = table.Column<bool>(type: "bit", nullable: true),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion_product", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    BookDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TableNo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Attendance = table.Column<float>(type: "real", nullable: true),
                    Member = table.Column<bool>(type: "bit", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Station_cash",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationNo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    HandOverNo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    CashType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station_cash", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Station_host",
                columns: table => new
                {
                    StationNo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station_host", x => x.StationNo);
                });

            migrationBuilder.CreateTable(
                name: "Station_log",
                columns: table => new
                {
                    StationNo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HandOverNo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station_log", x => x.StationNo);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    BranchNo = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Chairman = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BusinessRegistrationCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Bank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankingDepartment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table_setting",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SeatNum = table.Column<float>(type: "real", nullable: true),
                    Hall = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Waiter = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_setting", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployeeNo = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserLogin = table.Column<bool>(type: "bit", nullable: true),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Orders_detail",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    SerialNo = table.Column<int>(type: "int", nullable: false),
                    ProductNo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: true),
                    DiscountPrice = table.Column<float>(type: "real", nullable: true),
                    Updater = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders_detail", x => new { x.No, x.SerialNo });
                    table.ForeignKey(
                        name: "FK_Orders_detail_Orders_No",
                        column: x => x.No,
                        principalTable: "Orders",
                        principalColumn: "No",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Payment",
                columns: new[] { "Code", "Change", "InvoiceDisplay", "Name" },
                values: new object[,]
                {
                    { "CARD", false, true, "信用卡" },
                    { "CASH", true, true, "現金" },
                    { "POINT", false, false, "點數折抵" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Authority");

            migrationBuilder.DropTable(
                name: "Business_hours");

            migrationBuilder.DropTable(
                name: "Coupons_list");

            migrationBuilder.DropTable(
                name: "Factory");

            migrationBuilder.DropTable(
                name: "Hand_over");

            migrationBuilder.DropTable(
                name: "Maintain_log");

            migrationBuilder.DropTable(
                name: "Mix");

            migrationBuilder.DropTable(
                name: "Orders_detail");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Product_Attribute");

            migrationBuilder.DropTable(
                name: "Product_kind");

            migrationBuilder.DropTable(
                name: "Promotion_duration");

            migrationBuilder.DropTable(
                name: "Promotion_product");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Station_cash");

            migrationBuilder.DropTable(
                name: "Station_host");

            migrationBuilder.DropTable(
                name: "Station_log");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Table_setting");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
