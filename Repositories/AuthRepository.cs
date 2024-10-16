using Microsoft.EntityFrameworkCore;
using RegisterLoginAPI.Interfaces;
using RegisterLoginAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace RegisterLoginAPI.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserContext context;

    public AuthRepository(UserContext context)
    {
        this.context = context;
    }

    private void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
    {
        var hmac = new HMACSHA256();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
    {
        var hmac = new HMACSHA256(passwordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != passwordHash[i])
            {
                return false;
            }
        }

        return true;
    }

    public async Task<UserModel> Login(string username, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null)
            return null;

        if (!VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash))
            return null;

        return user;
    }

    public async Task<UserModel> Register(UserModel user, string password)
    {
        byte[] passwordSalt, passwordHash;
        CreatePasswordHash(password, out passwordSalt, out passwordHash);
        user.PasswordSalt = passwordSalt;
        user.PasswordHash = passwordHash;
        await context.Users.AddAsync(user);
        return user;

    }

    public async Task<bool> UserExists(string username)
    {
        if (await context.Users.AnyAsync(x => x.Username == username))
            return true;

        return false;
    }
}