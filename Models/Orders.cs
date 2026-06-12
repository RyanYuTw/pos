using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("Orders")]
public class Orders
{
    [Key, MaxLength(20)]
    public string No { get; set; } = default!;

    [Required, MaxLength(20)]
    public string CustomerId { get; set; } = default!;

    [Required, MaxLength(3)]
    public string StationNo { get; set; } = default!;

    [Required, MaxLength(12)]
    public string HandOverNo { get; set; } = default!;

    [MaxLength(12)]
    public string? InvoiceNo { get; set; }

    public DateTime OrderDate { get; set; }

    [Required, MaxLength(3)]
    public string TableNo { get; set; } = default!;

    public float SubTotal { get; set; }

    public float Allowance { get; set; }

    public float ServiceCharge { get; set; }

    public float GrandTotal { get; set; }

    public float? Cash { get; set; }

    public float? CreditCard { get; set; }

    public float? Point { get; set; }

    public float? Coupons { get; set; }

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    [Required, MaxLength(1)]
    public string Status { get; set; } = "0";

    public ICollection<OrdersDetail> Details { get; set; } = new List<OrdersDetail>();
}

[Table("Orders_detail")]
public class OrdersDetail
{
    [Key, MaxLength(20)]
    public string No { get; set; } = default!;

    public int SerialNo { get; set; }

    [Required, MaxLength(5)]
    public string ProductNo { get; set; } = default!;

    public float Amount { get; set; }

    public float? Discount { get; set; }

    public float? DiscountPrice { get; set; }

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    [Required, MaxLength(1)]
    public string Status { get; set; } = "0";
}
