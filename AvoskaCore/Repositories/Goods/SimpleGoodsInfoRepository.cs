using Avoska.Models.Goods;

namespace Avoska.Repositories.Goods;

public class SimpleGoodsInfoRepository : IGoodsInfoRepository
{
    private static readonly Dictionary<Guid, GoodInfoModel> GoodInfosMap = [];

    public Task<IEnumerable<GoodInfoModel>> GetAll()
    {
        return Task.FromResult<IEnumerable<GoodInfoModel>>(GoodInfosMap.Values);
    }

    public Task<GoodInfoModel?> GetById(Guid id)
    {
        return Task.FromResult(GoodInfosMap.GetValueOrDefault(id));
    }

    public Task<GoodInfoModel?> GetByName(string name)
    {
        return Task.FromResult(GoodInfosMap.Values.FirstOrDefault(o => o.Name == name));
    }

    public Task<IEnumerable<GoodInfoModel>> GetByCategoryName(string categoryName)
    {
        return Task.FromResult(GoodInfosMap.Values.Where(o => o.CategoryName == categoryName));
    }

    public Task<GoodInfoModel?> Add(AddGoodInfoModelDto addGoodInfoModelDto)
    {
        var goodInfo = new GoodInfoModel();
        goodInfo.Update(addGoodInfoModelDto);

        GoodInfosMap.Add(goodInfo.Id, goodInfo);

        return Task.FromResult(goodInfo)!;    
    }

    public Task<GoodInfoModel?> Put(PutGoodInfoModelDto putGoodInfo)
    {
        GoodInfosMap.TryGetValue(putGoodInfo.Id, out var goodInfo);
        
        goodInfo?.Update(putGoodInfo);
        
        return Task.FromResult(goodInfo);    
    }

    public Task<GoodInfoModel?> Patch(PatchGoodInfoModelDto patchGoodInfo)
    {
        GoodInfosMap.TryGetValue(patchGoodInfo.Id, out var goodInfo);

        goodInfo?.Update(patchGoodInfo);
        
        return Task.FromResult(goodInfo);
    }

    public Task<GoodInfoModel?> DeleteById(Guid id)
    {
        GoodInfosMap.Remove(id, out var userInfo);

        return Task.FromResult(userInfo);
    }
}