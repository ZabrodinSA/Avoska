using System.ComponentModel.DataAnnotations;

namespace Avoska.Models.Catalogs;

public class CategoryModel
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
    public string? Name { get; set; }
    
    public string? Image { get; set; }

    public void Update(PutCategoryModelDto categoryModel)
    {
        Name = categoryModel.Name;
        Image = categoryModel.Image;
    }
    
    public void Update(PatchCategoryModelDto categoryModel)
    {
        Name = categoryModel.Name;
        Image = categoryModel.Image;
    }

    public void Update(AddCategoryModelDto addCategoryModelDto)
    {
        Name = addCategoryModelDto.Name;
        Image = addCategoryModelDto.Image;
    }
}