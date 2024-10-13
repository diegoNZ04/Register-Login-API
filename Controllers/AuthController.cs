using RegisterLoginAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using RegisterLoginAPI.Services;

namespace RegisterLoginAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _authService.Register(request);
        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _authService.Login(request);

        if (!result.Success)
            return Unauthorized(result.Message);

        return Ok(new { token = result.Token });
    }
}