using System.ComponentModel.DataAnnotations;

namespace Avoska.Models.Users;

public class VerifyCodeModelDto(string phoneNumber, int code)
{
    [Required]
    public string PhoneNumber { get; } = phoneNumber;
    
    [Required]
    public int Code { get; } = code;
}