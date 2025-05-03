using System.ComponentModel.DataAnnotations;

namespace Avoska.Models.Goods;

public class GoodInfoModel
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Compare("", ErrorMessage = "Имя не задано")]
    public string? Name { get; set;}

    public string? CategoryName { get; set;}

    public string? Description { get; set;}
    
    public string? Image { get; set;}
    
    public string? Composition { get; set;}
    
    public int? WeightPerGram { get; set;}
    
    public NutritionalValue? NutritionalValue { get; set;}
    
    public string? ManufactureCountry { get; set;}
    
    public DateTime? ExpirationDate { get; set;}

    public void Update(PutGoodInfoModelDto goodInfoModelDto)
    {
        CategoryName = goodInfoModelDto.CategoryName;
        Name = goodInfoModelDto.Name;
        Description = goodInfoModelDto.Description;
        Image = goodInfoModelDto.Image;
        Composition = goodInfoModelDto.Composition;
        WeightPerGram = goodInfoModelDto.WeightPerGram;
        NutritionalValue = goodInfoModelDto.NutritionalValue;
        ManufactureCountry = goodInfoModelDto.ManufactureCountry;
        ExpirationDate = goodInfoModelDto.ExpirationDate;
    }
    
    public void Update(PatchGoodInfoModelDto goodInfoModelDto)
    {
        Name = goodInfoModelDto.Name;
        Description = goodInfoModelDto.Description;
        Image = goodInfoModelDto.Image;
        ManufactureCountry = goodInfoModelDto.ManufactureCountry;
    }
    
    public void Update(AddGoodInfoModelDto addGoodInfoModelDto)
    {
        CategoryName = addGoodInfoModelDto.CategoryName;
        Name = addGoodInfoModelDto.Name;
        Description = addGoodInfoModelDto.Description;
        Image = addGoodInfoModelDto.Image;
        Composition = addGoodInfoModelDto.Composition;
        WeightPerGram = addGoodInfoModelDto.WeightPerGram;
        NutritionalValue = addGoodInfoModelDto.NutritionalValue;
        ManufactureCountry = addGoodInfoModelDto.ManufactureCountry;
        ExpirationDate = addGoodInfoModelDto.ExpirationDate;
    }
}


