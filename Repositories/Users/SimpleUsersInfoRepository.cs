using Avoska.Models.Users;

namespace Avoska.Repositories.Users;

public class SimpleUsersInfoRepository : IUsersInfoRepository
{
    private static readonly Dictionary<string, UserInfoModel> UserInfosMap = [];
    
    public Task<IEnumerable<UserInfoModel>> GetAll()
    {
        return Task.FromResult<IEnumerable<UserInfoModel>>(UserInfosMap.Values);
    }

    public Task<UserInfoModel?> Add(string phoneNumber)
    {
        var userInfo = new UserInfoModel(phoneNumber);

        UserInfosMap.Add(phoneNumber, userInfo);

        return Task.FromResult(userInfo)!;
    }


    public Task<UserInfoModel?> Put(PutUserInfoModelDto putUserInfoModelDto)
    {
        UserInfosMap.TryGetValue(putUserInfoModelDto.PhoneNumber, out var userInfo);

        userInfo?.Update(putUserInfoModelDto);
        
        return Task.FromResult(userInfo);    
    }

    public Task<UserInfoModel?> Patch(PatchUserInfoModelDto patchCategoryModelDto)
    {
        UserInfosMap.TryGetValue(patchCategoryModelDto.PhoneNumber, out var userInfo);

        userInfo?.Update(patchCategoryModelDto);

        return Task.FromResult(userInfo);    
    }

    public Task<UserInfoModel?> GetByPhone(string phoneNumber)
    {
        return Task.FromResult(UserInfosMap.Values.SingleOrDefault(o => o.PhoneNumber == phoneNumber));
    }

    public async Task<UserInfoModel?> DeleteByPhone(string phoneNumber)
    {
        var userInfo = await GetByPhone(phoneNumber);

        if (userInfo != null)
            UserInfosMap.Remove(userInfo.PhoneNumber);

        return await Task.FromResult(userInfo);
    }
}