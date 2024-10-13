using RegisterLoginAPI.Models;
using RegisterLoginAPI.Helpers;
using RegisterLoginAPI.Repositories;
using RegisterLoginAPI.DTOs;
using BCrypt.Net;


namespace RegisterLoginAPI.Services;

public class AuthService
{
    private readonly UserRepository _userRepository;
    private readonly JwtHelper _jwtHelper;

    public AuthService(UserRepository userRepository, JwtHelper jwtHelper)
    {
        _userRepository = userRepository;
        _jwtHelper = jwtHelper;
    }

    public AuthResult Register(RegisterRequest request)
    {
        // Lógica de registro (e.g., validações e salvamento de dados)
        var user = new UserModel
        {
            Username = request.Username,
            Email = request.Email,
            Address = request.Address,
            Complement = request.Complement,
            City = request.City,
            State = request.State,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _userRepository.CreatedUser(user);
        return new AuthResult { Success = true };
    }

    public AuthResult Login(LoginRequest request)
    {
        var user = _userRepository.GetUserByUsername(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return new AuthResult { Success = false, Message = "Invalid credentials" };

        var token = _jwtHelper.GenerateJwtToken(user);
        return new AuthResult { Success = true, Token = token };
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public string GenerateJwtToken(UserModel user)
    {
        return _jwtHelper.GenerateJwtToken(user);
    }
}

