using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Avoska.Models.Users;

public sealed class UserInfoModel : IdentityUser
{
    public const string  ADMIN_ROLE = "admin";
    public const string  USER_ROLE = "user";
    public const string  DEFAULT_USER_NAME = "Empty";

    public UserInfoModel(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        Id = phoneNumber;
        UserName = DEFAULT_USER_NAME;
    }

    [Required]
    public override string PhoneNumber { get; set; }

    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
    public override string? Email { get; set; }

    public override string Id { get; set; }

    public void Update(PutUserInfoModelDto putUserInfoModelDto)
    {
        UserName = putUserInfoModelDto.Name;
        Email = putUserInfoModelDto.Email;
    }
    
    public void Update(PatchUserInfoModelDto patchCategoryModelDto)
    {
        UserName = patchCategoryModelDto.Name;
        Email = patchCategoryModelDto.Email;
    }
}