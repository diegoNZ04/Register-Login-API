using Microsoft.AspNetCore.Mvc;
using RegisterLoginAPI.Interfaces;
using RegisterLoginAPI.DTOs;
using RegisterLoginAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace RegisterLoginAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthRepository repo;
    private readonly IConfiguration config;

    public AuthController(IAuthRepository repo, IConfiguration config)
    {
        this.config = config;
        this.repo = repo;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
    {
        userRegisterDTO.Username = userRegisterDTO.Username.ToLower();

        if (await repo.UserExists(userRegisterDTO.Username))
            return BadRequest("User Is Already Registered");

        var CreateUser = new UserModel
        {
            Username = userRegisterDTO.Username,
            Email = userRegisterDTO.Email,
            Address = userRegisterDTO.Address
        };

        var CreatedUser = await repo.Register(CreateUser, userRegisterDTO.Password);
        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
    {
        var userRepo = await repo.Login(userLoginDTO.Username.ToLower(), userLoginDTO.Password);

        if (userRepo == null)
            return Unauthorized();

        var Claims = new[]{
            new Claim(ClaimTypes.NameIdentifier, userRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, userRepo.Username)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
        var card = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(Claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = card
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}