using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("Payment")]
public class Payment
{
    [Key, MaxLength(5)]
    public string Code { get; set; } = default!;

    [Required, MaxLength(50)]
    public string Name { get; set; } = default!;

    public bool Change { get; set; } = true;

    public bool InvoiceDisplay { get; set; } = true;
}
