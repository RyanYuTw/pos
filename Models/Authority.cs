using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("Authority")]
public class Authority
{
    [MaxLength(20)]
    public string UserId { get; set; } = default!;

    [MaxLength(50)]
    public string? UserGroup { get; set; }

    [MaxLength(10)]
    public string? SystemId { get; set; }

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    [Required, MaxLength(1)]
    public string Status { get; set; } = "0";
}
