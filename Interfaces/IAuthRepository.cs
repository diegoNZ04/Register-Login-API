using RegisterLoginAPI.Models;

namespace RegisterLoginAPI.Interfaces;

public interface IAuthRepository
{
    Task<UserModel> Register(UserModel user, string password);
    Task<UserModel> Login(string username, string password);
    Task<bool> UserExists(string username);
}