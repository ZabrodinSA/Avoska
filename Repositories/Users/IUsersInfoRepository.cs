using Avoska.Models.Users;

namespace Avoska.Repositories.Users;

public interface IUsersInfoRepository
{
    public Task<IEnumerable<UserInfoModel>> GetAll();

    public Task<UserInfoModel?> Add(string phoneNumber);

    public Task<UserInfoModel?> Put(PutUserInfoModelDto putUserInfoModelDto);
    public Task<UserInfoModel?> Patch(PatchUserInfoModelDto patchCategoryModelDto);

    public Task<UserInfoModel?> GetByPhone(string phoneNumber);
    
    public Task<UserInfoModel?> DeleteByPhone(string phoneNumber);
}
