using Avoska.Models.Goods;

namespace Avoska.Repositories.Goods;

public interface IGoodsInfoRepository
{
    public Task<IEnumerable<GoodInfoModel>> GetAll();

    public Task<GoodInfoModel?> GetById(Guid id);

    public Task<GoodInfoModel?> GetByName(string name);

    public Task<IEnumerable<GoodInfoModel>> GetByCategoryName(string categoryName);

    public Task<GoodInfoModel?> Add(AddGoodInfoModelDto addGoodInfo);

    public Task<GoodInfoModel?> Put(PutGoodInfoModelDto putGoodInfo);
    
    public Task<GoodInfoModel?> Patch(PatchGoodInfoModelDto patchGoodInfo);

    public Task<GoodInfoModel?> DeleteById(Guid name);
}