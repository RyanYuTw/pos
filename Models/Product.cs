using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("Product")]
public class Product
{
    [Key, MaxLength(5)]
    public string No { get; set; } = default!;

    [Required, MaxLength(50)]
    public string Name { get; set; } = default!;

    [MaxLength(2)]
    public string? Kind { get; set; }

    [MaxLength(50)]
    public string? UnitValue { get; set; }

    [MaxLength(50)]
    public string? Unit { get; set; }

    [MaxLength(3)]
    public string? Size { get; set; }

    public float Price { get; set; }

    public float DiscountPrice { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [MaxLength(50)]
    public string? Image { get; set; }

    public bool Shelfing { get; set; } = true;

    public bool Promotion { get; set; } = false;

    public bool Mix { get; set; } = false;

    public bool Stock { get; set; } = false;

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    [Required, MaxLength(1)]
    public string Status { get; set; } = "0";
}

[Table("Product_kind")]
public class ProductKind
{
    [Key, MaxLength(2)]
    public string No { get; set; } = default!;

    [Required, MaxLength(50)]
    public string Name { get; set; } = default!;
}

[Table("Mix")]
public class Mix
{
    [MaxLength(5)]
    public string MixNo { get; set; } = default!;

    [MaxLength(5)]
    public string No { get; set; } = default!;
}

[Table("Attribute")]
public class ProductAttributeCode
{
    [Key, MaxLength(3)]
    public string Code { get; set; } = default!;

    [Required, MaxLength(50)]
    public string Name { get; set; } = default!;
}

[Table("Size")]
public class ProductSize
{
    [Key, MaxLength(3)]
    public string Code { get; set; } = default!;

    [Required, MaxLength(50)]
    public string Name { get; set; } = default!;
}

[Table("Product_Attribute")]
public class ProductAttribute
{
    [MaxLength(5)]
    public string No { get; set; } = default!;

    [MaxLength(3)]
    public string Code { get; set; } = default!;
}
