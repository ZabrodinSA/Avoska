using Avoska.Models.Goods;

namespace Avoska.Repositories.Goods;

public class GoodsInfoRepository : IGoodsInfoRepository
{
    private static readonly Dictionary<Guid, GoodInfoModel> GoodInfosMap = [];

    public IEnumerable<GoodInfoModel> GetAll()
    {
        return GoodInfosMap.Values;
    }

    public GoodInfoModel? GetById(Guid id)
    {
        return GoodInfosMap.GetValueOrDefault(id);
    }

    public GoodInfoModel? GetByName(string name)
    {
        return GoodInfosMap.Values.FirstOrDefault(o => o.Name == name);
    }

    public IEnumerable<GoodInfoModel> GetByCategoryName(string categoryName)
    {
        return GoodInfosMap.Values.Where(o => o.CategoryName == categoryName);
    }

    public GoodInfoModel Add(AddGoodInfoModelDto addGoodInfoModelDto)
    {
        var goodInfo = new GoodInfoModel(addGoodInfoModelDto);

        GoodInfosMap.Add(goodInfo.Id, goodInfo);

        return goodInfo;    
    }

    public GoodInfoModel? Put(PutGoodInfoModelDto putGoodInfo)
    {
        if (!GoodInfosMap.TryGetValue(putGoodInfo.Id, out var goodInfo))
            return null;

        goodInfo.Update(putGoodInfo);
        
        return goodInfo;    
    }

    public GoodInfoModel? Patch(PatchGoodInfoModelDto patchGoodInfo)
    {
        if (!GoodInfosMap.TryGetValue(patchGoodInfo.Id, out var goodInfo))
            return null;

        goodInfo.Update(patchGoodInfo);
        
        return goodInfo;
    }

    public GoodInfoModel? DeleteById(Guid id)
    {
        if (!GoodInfosMap.Remove(id, out var userInfo))
            return null;

        return userInfo;
    }
}