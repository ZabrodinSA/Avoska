using System.ComponentModel.DataAnnotations;

namespace Avoska.Models.Users;

public class VerifyCodeModelDto()
{
    [Required]
    public string? PhoneNumber { get; set; }
    
    [Required]
    public int? Code { get; set; }
}