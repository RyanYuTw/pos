using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("User")]
public class User
{
    [Key]
    [MaxLength(20)]
    public string UserId { get; set; } = default!;

    [Required, MaxLength(50)]
    public string Password { get; set; } = default!;

    [Required, MaxLength(4)]
    public string EmployeeNo { get; set; } = default!;

    [Required, MaxLength(50)]
    public string Name { get; set; } = default!;

    [MaxLength(50)]
    public string? IdNo { get; set; }

    [MaxLength(50)]
    public string? UserGroup { get; set; }

    public bool? UserLogin { get; set; }

    [Required, MaxLength(20)]
    public string Updater { get; set; } = default!;

    public DateTime UpdateTime { get; set; }

    [Required, MaxLength(1)]
    public string Status { get; set; } = "0";
}
