using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Avoska.Models.Users;

public class AuthUserDbContext : IdentityDbContext<AuthUserModel>
{
    public AuthUserDbContext(DbContextOptions<AuthUserDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}