using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("Table_setting")]
public class TableSetting
{
    [Key, MaxLength(3)]
    public string No { get; set; } = default!;

    [MaxLength(50)]
    public string? Name { get; set; }

    public float? SeatNum { get; set; }

    [MaxLength(50)]
    public string? Hall { get; set; }

    /// <summary>員工編號</summary>
    [MaxLength(4)]
    public string? Waiter { get; set; }
}

[Table("Business_hours")]
public class BusinessHours
{
    public int Id { get; set; }

    /// <summary>時段 (0: 不限, 1: 早, 2: 中, 3: 晚)</summary>
    [Required, MaxLength(1)]
    public string TimeInterval { get; set; } = default!;

    [MaxLength(1)]
    public string? Week { get; set; }

    [MaxLength(50)]
    public string? StartTime { get; set; }

    public float? EndTime { get; set; }

    public float MealTimes { get; set; }
}
