using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models;

[Table("Store")]
public class Store
{
    [Key, MaxLength(8)]
    public string Id { get; set; } = default!;

    [MaxLength(4)]
    public string? BranchNo { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = default!;

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(20)]
    public string? Telephone { get; set; }

    [MaxLength(10)]
    public string? Chairman { get; set; }

    [MaxLength(10)]
    public string? BusinessRegistrationCode { get; set; }

    [MaxLength(50)]
    public string? Email { get; set; }

    [MaxLength(10)]
    public string? Fax { get; set; }

    [MaxLength(50)]
    public string? Bank { get; set; }

    [MaxLength(50)]
    public string? BankingDepartment { get; set; }

    [MaxLength(50)]
    public string? BankAccount { get; set; }

    [MaxLength(200)]
    public string? Url { get; set; }

    [Required, MaxLength(50)]
    public string Version { get; set; } = "1.0";
}
