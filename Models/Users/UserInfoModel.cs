using System.ComponentModel.DataAnnotations;

namespace Avoska.Models.Users;

public class UserInfoModel(string phoneNumber)
{
    [Required] 
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string PhoneNumber { get; } = phoneNumber;

    public string? Name { get; set; }

    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
    public string? Email { get; set; }

    public void Update(PutUserInfoModelDto putUserInfoModelDto)
    {
        Name = putUserInfoModelDto.Name;
        Email = putUserInfoModelDto.Email;
    }
    
    public void Update(PatchUserInfoModelDto patchCategoryModelDto)
    {
        Name = patchCategoryModelDto.Name;
        Email = patchCategoryModelDto.Email;
    }
}