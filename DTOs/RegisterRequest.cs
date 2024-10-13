namespace RegisterLoginAPI.DTOs;

public class RegisterRequest
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? Complement { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
}