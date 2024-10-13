using Microsoft.EntityFrameworkCore;

namespace RegisterLoginAPI.Models;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    public DbSet<UserModel> Users { get; set; } = null!;
}