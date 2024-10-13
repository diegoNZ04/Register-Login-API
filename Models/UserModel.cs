using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegisterLoginAPI.Models;

// User Model
public class UserModel
{
    // Primary Key
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = default!;
    [StringLength(255)]
    [Required]
    public string Email { get; set; } = default!;
    [StringLength(50)]
    [Required]
    public string? Password { get; set; } = default!;
    [StringLength(255)]
    [Required]
    public string Address { get; set; } = default!;
    [StringLength(255)]
    public string? Complement { get; set; } = default!;
    [Required]
    [StringLength(50)]
    public string City { get; set; } = default!;
    [Required]
    [StringLength(50)]
    public string State { get; set; } = default!;

}