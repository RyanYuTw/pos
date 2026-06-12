using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("Hand_over")]
public class HandOver
{
    [Key, MaxLength(12)]
    public string No { get; set; } = default!;

    public DateTime HandoverDate { get; set; }

    public float GrandTotal { get; set; }

    public float ServiceCharge { get; set; }

    public float Allowance { get; set; }

    public float OpeningCash { get; set; } = 0;

    public float? Cash { get; set; }

    public float? CreditCard { get; set; }

    public float? Point { get; set; }

    public float? Coupons { get; set; }

    [Required, MaxLength(20)]
    public string HandOverUser { get; set; } = default!;

    [Required, MaxLength(20)]
    public string Receiver { get; set; } = default!;

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    [Required, MaxLength(1)]
    public string Status { get; set; } = "0";
}

[Table("Coupons_list")]
public class CouponsList
{
    [Key, MaxLength(12)]
    public string OrderNo { get; set; } = default!;

    [Required, MaxLength(64)]
    public string CouponsNo { get; set; } = default!;
}

[Table("Maintain_log")]
public class MaintainLog
{
    public int Id { get; set; }

    public DateTime LogDate { get; set; }

    /// <summary>0: 更新, 1: 備份, 2: 還原</summary>
    [Required, MaxLength(1)]
    public string Action { get; set; } = default!;

    [Required, MaxLength(50)]
    public string DbName { get; set; } = default!;

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }
}
