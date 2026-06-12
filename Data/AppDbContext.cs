using Microsoft.EntityFrameworkCore;
using Pos.Models;

namespace Pos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // 使用者 & 權限
    public DbSet<User> Users { get; set; }
    public DbSet<Authority> Authorities { get; set; }

    // 商家 & 廠商
    public DbSet<Store> Stores { get; set; }
    public DbSet<Factory> Factories { get; set; }

    // 收銀機
    public DbSet<StationHost> StationHosts { get; set; }
    public DbSet<StationLog> StationLogs { get; set; }
    public DbSet<StationCash> StationCashes { get; set; }

    // 商品
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductKind> ProductKinds { get; set; }
    public DbSet<Mix> Mixes { get; set; }
    public DbSet<ProductAttributeCode> ProductAttributeCodes { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }

    // 付費方式
    public DbSet<Payment> Payments { get; set; }

    // 促銷
    public DbSet<PromotionDuration> PromotionDurations { get; set; }
    public DbSet<PromotionProduct> PromotionProducts { get; set; }

    // 桌位 & 營業時間
    public DbSet<TableSetting> TableSettings { get; set; }
    public DbSet<BusinessHours> BusinessHours { get; set; }

    // 前台交易
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrdersDetail> OrdersDetails { get; set; }
    public DbSet<HandOver> HandOvers { get; set; }
    public DbSet<CouponsList> CouponsLists { get; set; }

    // 使用者群組
    public DbSet<UserGroup> UserGroups { get; set; }

    // 系統維護
    public DbSet<MaintainLog> MaintainLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mix 複合主鍵
        modelBuilder.Entity<Mix>()
            .HasKey(m => new { m.MixNo, m.No });

        // ProductAttribute 複合主鍵
        modelBuilder.Entity<ProductAttribute>()
            .HasKey(pa => new { pa.No, pa.Code });

        // Authority 複合主鍵
        modelBuilder.Entity<Authority>()
            .HasKey(a => new { a.UserId, a.SystemId });

        // Orders → OrdersDetail 一對多
        modelBuilder.Entity<Orders>()
            .HasMany(o => o.Details)
            .WithOne()
            .HasForeignKey(d => d.No);

        // OrdersDetail 複合主鍵
        modelBuilder.Entity<OrdersDetail>()
            .HasKey(d => new { d.No, d.SerialNo });

        // CouponsList 複合主鍵
        modelBuilder.Entity<CouponsList>()
            .HasKey(c => new { c.OrderNo, c.CouponsNo });

        // 預設付費方式種子資料
        modelBuilder.Entity<Payment>().HasData(
            new Payment { Code = "CASH", Name = "現金", Change = true, InvoiceDisplay = true },
            new Payment { Code = "CARD", Name = "信用卡", Change = false, InvoiceDisplay = true },
            new Payment { Code = "POINT", Name = "點數折抵", Change = false, InvoiceDisplay = false }
        );

        // 預設使用者群組種子資料
        modelBuilder.Entity<UserGroup>().HasData(
            new UserGroup { Id = "0", Name = "管理者", Description = "系統管理員，擁有全部權限" },
            new UserGroup { Id = "1", Name = "班長", Description = "可查看交班報表" },
            new UserGroup { Id = "2", Name = "收銀員", Description = "前台收銀操作" }
        );

        // 預設管理者帳號
        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = "admin",
                Password = "$2b$11$iiXbduXveVZcjXrVEfEX2.8uWRJkROdFjx8C351b17bnTFYtehGzu",
                EmployeeNo = "0001",
                Name = "管理者",
                UserGroup = "管理者",
                UserLogin = false,
                Updater = "system",
                UpdateTime = new DateTime(2026, 1, 1),
                Status = "0"
            }
        );

        // 商家資料
        modelBuilder.Entity<Store>().HasData(
            new Store
            {
                Id = "MAIN0001",
                BranchNo = "0001",
                Name = "幸福小館",
                Address = "台北市信義區幸福路100號",
                Telephone = "02-12345678",
                Chairman = "王大明",
                Email = "info@happyrest.tw",
                Version = "1.0"
            }
        );

        // 收銀機
        modelBuilder.Entity<StationHost>().HasData(
            new StationHost { StationNo = "001", HostName = "1號收銀機" },
            new StationHost { StationNo = "002", HostName = "2號收銀機" }
        );

        // 桌位
        modelBuilder.Entity<TableSetting>().HasData(
            new TableSetting { No = "A01", Name = "A區1號", SeatNum = 2, Hall = "A區" },
            new TableSetting { No = "A02", Name = "A區2號", SeatNum = 2, Hall = "A區" },
            new TableSetting { No = "A03", Name = "A區3號", SeatNum = 4, Hall = "A區" },
            new TableSetting { No = "A04", Name = "A區4號", SeatNum = 4, Hall = "A區" },
            new TableSetting { No = "B01", Name = "B區1號", SeatNum = 4, Hall = "B區" },
            new TableSetting { No = "B02", Name = "B區2號", SeatNum = 4, Hall = "B區" },
            new TableSetting { No = "B03", Name = "B區3號", SeatNum = 6, Hall = "B區" },
            new TableSetting { No = "B04", Name = "B區4號", SeatNum = 6, Hall = "B區" },
            new TableSetting { No = "C01", Name = "包廂1", SeatNum = 8, Hall = "包廂" },
            new TableSetting { No = "C02", Name = "包廂2", SeatNum = 10, Hall = "包廂" }
        );

        // 商品種類
        modelBuilder.Entity<ProductKind>().HasData(
            new ProductKind { No = "01", Name = "飲料" },
            new ProductKind { No = "02", Name = "主食" },
            new ProductKind { No = "03", Name = "小點" },
            new ProductKind { No = "04", Name = "甜點" }
        );

        // 商品規格（杯型/份量大小）
        modelBuilder.Entity<ProductSize>().HasData(
            new ProductSize { Code = "S", Name = "小" },
            new ProductSize { Code = "M", Name = "中" },
            new ProductSize { Code = "L", Name = "大" }
        );

        // 商品屬性（溫度選項）
        modelBuilder.Entity<ProductAttributeCode>().HasData(
            new ProductAttributeCode { Code = "HOT", Name = "熱" },
            new ProductAttributeCode { Code = "ICE", Name = "冰" },
            new ProductAttributeCode { Code = "WRM", Name = "溫" }
        );

        // 商品
        var seedTime = new DateTime(2026, 1, 1);
        modelBuilder.Entity<Product>().HasData(
            new Product { No = "D0001", Name = "美式咖啡", Kind = "01", Price = 80, DiscountPrice = 80, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "D0002", Name = "拿鐵咖啡", Kind = "01", Price = 100, DiscountPrice = 100, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "D0003", Name = "紅茶", Kind = "01", Price = 60, DiscountPrice = 60, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "D0004", Name = "綠茶", Kind = "01", Price = 60, DiscountPrice = 60, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "D0005", Name = "柳橙汁", Kind = "01", Price = 80, DiscountPrice = 80, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "M0001", Name = "牛肉麵", Kind = "02", Price = 180, DiscountPrice = 180, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "M0002", Name = "排骨飯", Kind = "02", Price = 150, DiscountPrice = 150, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "M0003", Name = "炒飯", Kind = "02", Price = 120, DiscountPrice = 120, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "M0004", Name = "焗烤義大利麵", Kind = "02", Price = 160, DiscountPrice = 160, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "S0001", Name = "薯條", Kind = "03", Price = 60, DiscountPrice = 60, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "S0002", Name = "炸雞塊", Kind = "03", Price = 80, DiscountPrice = 80, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "S0003", Name = "洋蔥圈", Kind = "03", Price = 70, DiscountPrice = 70, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "T0001", Name = "提拉米蘇", Kind = "04", Price = 120, DiscountPrice = 120, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" },
            new Product { No = "T0002", Name = "草莓蛋糕", Kind = "04", Price = 100, DiscountPrice = 100, Shelfing = true, Updater = "system", UpdateTime = seedTime, Status = "0" }
        );
    }
}
