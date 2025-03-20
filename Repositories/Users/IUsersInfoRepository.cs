using Avoska.Models.Users;

namespace Avoska.Repositories.Users;

public interface IUsersInfoRepository
{
    public IEnumerable<UserInfoModel> GetAll();

    public UserInfoModel Add(string phoneNumber);

    public UserInfoModel? Put(PutUserInfoModelDto putUserInfoModelDto);
    public UserInfoModel? Patch(PatchUserInfoModelDto patchCategoryModelDto);

    public UserInfoModel? GetById(Guid id);
    
    public UserInfoModel? GetByPhone(string phoneNumber);
    
    public UserInfoModel? DeleteById(Guid id);
    
    public UserInfoModel? DeleteByPhone(string phoneNumber);
}
