using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Avoska.Models.Users;

public class UserInfoContext : IdentityDbContext<UserInfoModel>
{
    public UserInfoContext(DbContextOptions<UserInfoContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}