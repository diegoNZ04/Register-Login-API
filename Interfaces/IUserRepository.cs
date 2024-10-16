using RegisterLoginAPI.Models;

namespace RegisterLoginAPI.Interfaces;

public interface IUserRepository
{
    IEnumerable<UserModel> GetAllUsers();
    UserModel? GetUserById(long id);
    void InsertUser(UserModel user);
    void UpdateUser(UserModel user);
    void DeleteUser(long id);
}