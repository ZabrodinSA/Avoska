using Microsoft.AspNetCore.Identity;


namespace Avoska.Models.Users;

public class AuthUserModel : IdentityUser
{
    public const string  ADMIN_ROLE = "admin";
    public const string  USER_ROLE = "user";
}