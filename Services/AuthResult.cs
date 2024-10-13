namespace RegisterLoginAPI.Services;

public class AuthResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = default!;
    public string Token { get; set; } = default!;
}