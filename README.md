# POS 系統

ASP.NET Core MVC 餐飲 POS 系統，含前台點餐、結帳、訂位，以及後台商品、報表、使用者管理

## 技術規格

| 項目 | 版本 |
|------|------|
| .NET | 10.0 |
| ORM | Entity Framework Core 9 |
| 資料庫 | SQL Server（Docker） |
| 認證 | Cookie Authentication |

## 功能模組

**前台 (FrontDesk)**
- 開收銀站、交班
- 點餐（建立、修改訂單）
- 結帳、退貨
- 訂位管理

**後台 (BackOffice)**
- 商品管理（含尺寸、屬性）
- 門市設定
- 使用者與群組管理
- 報表：每日銷售、交班紀錄、退貨明細、商品銷售排行

## 本機開發環境設定

### 前置需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Docker（執行 SQL Server 容器）

### 1. 設定資料庫連線

複製範本並填入實際密碼：

```bash
cp appsettings.Development.json.example appsettings.Development.json
```

編輯 `appsettings.Development.json`，將 `YOUR_PASSWORD` 替換為實際密碼。

### 2. 設定環境變數

```bash
export MSSQL_SA_PASSWORD="你的 SA 密碼"
```

建議加入 `~/.zshrc` 或 `~/.zprofile` 避免每次重設。

### 3. 建立資料庫並執行 Migration

```bash
dotnet ef database update
```

### 4. 啟動應用程式

```bash
./rebuild-and-run.sh
```

應用程式將在 `http://localhost:8003` 啟動並自動開啟瀏覽器。

## 專案結構

```
Pos/
├── Controllers/
│   ├── FrontDesk/      # 前台：點餐、結帳、訂位、收銀站
│   └── BackOffice/     # 後台：商品、報表、門市、使用者
├── Models/             # EF Core 資料模型
├── Views/              # Razor 頁面
├── ViewModels/         # 表單與顯示用 ViewModel
├── Data/               # DbContext
├── Migrations/         # EF Core Migration 記錄
└── wwwroot/            # 靜態資源（CSS、JS）
```

## 敏感資料說明

| 檔案 | 用途 | git |
|------|------|-----|
| `appsettings.json` | 基本設定（無密碼） | ✅ 追蹤 |
| `appsettings.Development.json` | 本機連線字串 | ❌ 排除 |
| `appsettings.Development.json.example` | 設定範本 | ✅ 追蹤 |

環境變數 `MSSQL_SA_PASSWORD` 供 `rebuild-and-run.sh` 使用，請勿寫入任何 git 追蹤的檔案。
