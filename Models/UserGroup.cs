using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("User_group")]
public class UserGroup
{
    [Key, MaxLength(1)]
    public string Id { get; set; } = default!;

    [Required, MaxLength(50)]
    public string Name { get; set; } = default!;

    [MaxLength(100)]
    public string? Description { get; set; }
}
