using Microsoft.EntityFrameworkCore;

namespace Avoska.Models.Catalogs;

public class CategoriesContext : DbContext
{
    public DbSet<CategoryModel> Categories { get; set; } = null!;

    public CategoriesContext(DbContextOptions<CategoriesContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}