using System.ComponentModel.DataAnnotations;

namespace Avoska.Models.Goods;

public class AddGoodInfoModelDto
{
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
    public string Name { get; set; } = "";

    public string? CategoryName { get; set;}

    public string? Description { get; set;}
    
    public string? Image { get; set;}
    
    public string? Composition { get; set;}
    
    public int? WeightPerGram { get; set;}
    
    public NutritionalValue? NutritionalValue { get; set;}
    
    public string? ManufactureCountry { get; set;}
    
    public DateTime? ExpirationDate { get; set;}
}
