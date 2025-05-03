using Avoska.Models.Goods;
using Microsoft.EntityFrameworkCore;

namespace Avoska.Repositories.Goods;

public class DbGoodsInfoRepository(
    GoodsInfoContext db,
    ILogger<DbGoodsInfoRepository> logger)
    : IGoodsInfoRepository
{
    public Task<IEnumerable<GoodInfoModel>> GetAll()
    {
        return Task.FromResult<IEnumerable<GoodInfoModel>>(db.Goods);
    }

    public async Task<GoodInfoModel?> GetById(Guid id)
    {
        return await db.Goods.FirstOrDefaultAsync(good => good.Id == id);
    }

    public async Task<GoodInfoModel?> GetByName(string name)
    {
        return await db.Goods.FirstOrDefaultAsync(good => good.Name == name);
    }

    public async Task<IEnumerable<GoodInfoModel>> GetByCategoryName(string categoryName)
    {
        return await db.Goods.Where(good => good.CategoryName == categoryName).ToListAsync();
    }

    public async Task<GoodInfoModel?> Add(AddGoodInfoModelDto addGoodInfo)
    {
        var goodInfoModel = await db.Goods.FirstOrDefaultAsync(good => good.Name == addGoodInfo.Name);
        if (goodInfoModel != null)
        {
            logger.Log(LogLevel.Error, "Попытка добавить продукт с занятым именем");
            return null;
        }
        
        goodInfoModel = new GoodInfoModel();
        goodInfoModel.Update(addGoodInfo);
        
        await db.Goods.AddAsync(goodInfoModel);
        return await UpdateDb() ? goodInfoModel : null;
    }

    public async Task<GoodInfoModel?> Put(PutGoodInfoModelDto putGoodInfo)
    {
        var goodInfo = await db.Goods.FirstOrDefaultAsync(good => good.Id == putGoodInfo.Id);
        if (goodInfo == null) return null;
        
        goodInfo.Update(putGoodInfo);
        
        return await SaveGood(goodInfo) ? goodInfo : null;
    }

    public async Task<GoodInfoModel?> Patch(PatchGoodInfoModelDto patchGoodInfo)
    {
        var goodInfo = await db.Goods.FirstOrDefaultAsync(good => good.Id == patchGoodInfo.Id);
        if (goodInfo == null) return null;
        
        goodInfo.Update(patchGoodInfo);
        
        return await SaveGood(goodInfo) ? goodInfo : null;
    }

    public async Task<GoodInfoModel?> DeleteById(Guid id)
    {
        var goodInfo = await db.Goods.FirstOrDefaultAsync(good => good.Id == id);
        if (goodInfo == null) return null;
        
        db.Goods.Remove(goodInfo);
        
        return await UpdateDb() ? goodInfo : null;
    }

    private async Task<bool> SaveGood(GoodInfoModel goodInfo)
    {
        try
        {
            db.Goods.Update(goodInfo);
            await db.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            logger.Log(LogLevel.Error, ex.ToString());
            return false;
        }
    }
    
    private async Task<bool> UpdateDb()
    {
        try
        {
            await db.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            logger.Log(LogLevel.Error, ex.ToString());
            return false;
        }
    }
}