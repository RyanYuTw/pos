using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("Promotion_duration")]
public class PromotionDuration
{
    [Key]
    public int No { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    /// <summary>星期 (0=不限, 1=週一 ... 7=週日)</summary>
    [MaxLength(1)]
    public string? Week { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
}

[Table("Promotion_product")]
public class PromotionProduct
{
    [Key]
    public int No { get; set; }

    public int PromotionNo { get; set; }

    [Required, MaxLength(5)]
    public string ProductNo { get; set; } = default!;

    public float? Discount { get; set; }

    public float? Price { get; set; }

    public bool? MemberOnly { get; set; }

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    [Required, MaxLength(1)]
    public string Status { get; set; } = "0";
}
