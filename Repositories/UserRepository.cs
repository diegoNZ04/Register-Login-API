using Microsoft.AspNetCore.Mvc;
using RegisterLoginAPI.Models;

namespace RegisterLoginAPI.Repositories;

public class UserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }

    public UserModel CreatedUser(UserModel user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public UserModel GetUserByUsername(string username)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);

        return user;
    }

    public void UpdateUser(UserModel user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void DeleteUser(long id)
    {
        var user = _context.Users.SingleOrDefault(u => u.Id == id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public List<UserModel> GetAllUsers()
    {
        return _context.Users.ToList();
    }

}
