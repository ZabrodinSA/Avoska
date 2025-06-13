using Microsoft.EntityFrameworkCore;

namespace Avoska.Models.Goods;

public class GoodsInfoContext : DbContext
{
    public DbSet<GoodInfoModel> Goods { get; set; } = null!;

    public GoodsInfoContext(DbContextOptions<GoodsInfoContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}