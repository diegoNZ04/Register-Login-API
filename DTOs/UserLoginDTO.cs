using System.ComponentModel.DataAnnotations;

namespace RegisterLoginAPI.DTOs;

public class UserLoginDTO
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = default!;
    [Required]
    [StringLength(255)]
    public string Email { get; set; } = default!;
    [Required]
    [StringLength(50)]
    public string Password { get; set; } = default!;
}