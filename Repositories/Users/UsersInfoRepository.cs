using Avoska.Models.Users;

namespace Avoska.Repositories.Users;

public class UsersInfoRepository : IUsersInfoRepository
{
    private static readonly Dictionary<Guid, UserInfoModel> UserInfosMap = [];
    
    public IEnumerable<UserInfoModel> GetAll()
    {
        return UserInfosMap.Values;
    }

    public UserInfoModel Add(string phoneNumber)
    {
        var userInfo = new UserInfoModel(phoneNumber);

        UserInfosMap.Add(userInfo.Id, userInfo);

        return userInfo;
    }

    public UserInfoModel? Put(PutUserInfoModelDto putUserInfoModelDto)
    {
        if (!UserInfosMap.TryGetValue(putUserInfoModelDto.Id, out var userInfo))
            return null;

        userInfo.Update(putUserInfoModelDto);
        
        return userInfo;    
    }

    public UserInfoModel? Patch(PatchUserInfoModelDto patchCategoryModelDto)
    {
        if (!UserInfosMap.TryGetValue(patchCategoryModelDto.Id, out var userInfo))
            return null;

        userInfo.Update(patchCategoryModelDto);
        
        return userInfo;    
    }

    public UserInfoModel? GetById(Guid id)
    {
        return UserInfosMap.GetValueOrDefault(id);
    }

    public UserInfoModel? GetByPhone(string phoneNumber)
    {
        return UserInfosMap.Values.SingleOrDefault(o => o.PhoneNumber == phoneNumber);
    }

    public UserInfoModel? DeleteById(Guid id)
    {
        if (!UserInfosMap.Remove(id, out var userInfo))
            return null;

        return userInfo;
    }

    public UserInfoModel? DeleteByPhone(string phoneNumber)
    {
        UserInfoModel? userInfo = GetByPhone(phoneNumber);

        if (userInfo == null)
            return null;

        UserInfosMap.Remove(userInfo.Id);

        return userInfo;
    }
}