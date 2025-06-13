using System.ComponentModel.DataAnnotations;

namespace Avoska.Models.Goods;

public class PatchGoodInfoModelDto
{
    [Required]
    public Guid Id { get; set; }

    [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
    public string? Name { get; set;}

    public string? Description { get; set;}
    
    public string? Image { get; set;}
    
    public string? ManufactureCountry { get; set;}
}


