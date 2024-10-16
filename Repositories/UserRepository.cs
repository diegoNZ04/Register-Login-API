using Microsoft.EntityFrameworkCore;
using RegisterLoginAPI.Interfaces;
using RegisterLoginAPI.Models;

namespace RegisterLoginAPI.Repositories;

public class UserRepository : IUserRepository
{
    private UserContext context;
    public UserRepository(UserContext context)
    {
        this.context = context;
    }
    public IEnumerable<UserModel> GetAllUsers()
    {
        return context.Users.ToList();
    }

    public UserModel? GetUserById(long id)
    {
        return context.Users.Find(id);
        // context.Users.Where(x => x.Id == id).FirstOrDefault();
    }
    public void InsertUser(UserModel user)
    {
        context.Users.Add(user);
        context.SaveChanges();
    }


    public void DeleteUser(long id)
    {
        UserModel user = context.Users.Find(id);
        context.Users.Remove(user);
        context.SaveChanges();

    }

    public void UpdateUser(UserModel user)
    {
        context.Entry(user).State = EntityState.Modified;
        context.SaveChanges();
    }

}
