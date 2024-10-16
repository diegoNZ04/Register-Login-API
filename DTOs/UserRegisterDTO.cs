using System.ComponentModel.DataAnnotations;

namespace RegisterLoginAPI.DTOs;

public class UserRegisterDTO
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = default!;
    [StringLength(255)]
    [Required]
    public string Email { get; set; } = default!;
    [StringLength(50)]
    [Required]
    public string Password { get; set; } = default!;
    [StringLength(255)]
    [Required]
    public string Address { get; set; } = default!;
}