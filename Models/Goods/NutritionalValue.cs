using Microsoft.EntityFrameworkCore;

namespace Avoska.Models.Goods;

[Owned]
public record NutritionalValue(
    string Proteins,
    string Fat,
    string Carbohydrates
);