using System.ComponentModel.DataAnnotations;

namespace Avoska.Models.Users;

public class PatchUserInfoModelDto(string phoneNumber)
{
    [Required]
    public string PhoneNumber { get; set; } = phoneNumber;

    public string? Name { get; set; }

    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
    public string? Email { get; set; }
}