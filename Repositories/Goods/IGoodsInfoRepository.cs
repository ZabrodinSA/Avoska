using Avoska.Models.Goods;

namespace Avoska.Repositories.Goods;

public interface IGoodsInfoRepository
{
    public IEnumerable<GoodInfoModel> GetAll();

    public GoodInfoModel? GetById(Guid id);

    public GoodInfoModel? GetByName(string name);

    public IEnumerable<GoodInfoModel> GetByCategoryName(string categoryName);

    public GoodInfoModel Add(AddGoodInfoModelDto addGoodInfo);

    public GoodInfoModel? Put(PutGoodInfoModelDto addGoodInfo);
    
    public GoodInfoModel? Patch(PatchGoodInfoModelDto addGoodInfo);

    public GoodInfoModel? DeleteById(Guid name);
}