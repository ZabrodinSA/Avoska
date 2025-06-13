using System.ComponentModel.DataAnnotations;

namespace Avoska.Models.Catalogs;

public class AddCategoryModelDto
{
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
    public string Name { get; set; } = "";
    
    public string? Image { get; set; }
}