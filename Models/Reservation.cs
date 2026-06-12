using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("Reservation")]
public class Reservation
{
    [Key, MaxLength(12)]
    public string No { get; set; } = default!;

    public DateTime BookDate { get; set; }

    public DateTime BookTime { get; set; }

    [Required, MaxLength(50)]
    public string Contact { get; set; } = default!;

    [Required, MaxLength(10)]
    public string Phone { get; set; } = default!;

    [MaxLength(3)]
    public string? TableNo { get; set; }

    public float? Attendance { get; set; }

    public bool Member { get; set; } = false;

    [MaxLength(500)]
    public string? Memo { get; set; }

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    [Required, MaxLength(1)]
    public string Status { get; set; } = "0";
}
