using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

/// <summary>收銀機主機對應資料表</summary>
[Table("Station_host")]
public class StationHost
{
    [Key, MaxLength(3)]
    public string StationNo { get; set; } = default!;

    [Required, MaxLength(50)]
    public string HostName { get; set; } = default!;
}

/// <summary>收銀機紀錄資料表（開/結帳紀錄）</summary>
[Table("Station_log")]
public class StationLog
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(3)]
    public string StationNo { get; set; } = default!;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    [Required, MaxLength(20)]
    public string UserId { get; set; } = default!;

    [Required, MaxLength(50)]
    public string HostName { get; set; } = default!;

    [Required, MaxLength(12)]
    public string HandOverNo { get; set; } = default!;

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    [Required, MaxLength(1)]
    public string Status { get; set; } = "0";

    public float OpeningCash { get; set; } = 0;

    [MaxLength(50)]
    public string? Memo { get; set; }
}

/// <summary>收銀機業外收支資料表</summary>
[Table("Station_cash")]
public class StationCash
{
    public int Id { get; set; }

    [Required, MaxLength(3)]
    public string StationNo { get; set; } = default!;

    [Required, MaxLength(12)]
    public string HandOverNo { get; set; } = default!;

    /// <summary>0: 業外收入, 1: 業外支出</summary>
    [Required, MaxLength(1)]
    public string CashType { get; set; } = default!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }
}
