using Avoska.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace Avoska.Repositories.Users;

public class ManagementUsersInfoRepository(
    UserManager<UserInfoModel> userManager,
    ILogger<ManagementUsersInfoRepository> logger)
    : IUsersInfoRepository
{
    
    public Task<IEnumerable<UserInfoModel>> GetAll()
    {
        return Task.FromResult<IEnumerable<UserInfoModel>>(userManager.Users);
    }

    public async Task<UserInfoModel?> Add(string phoneNumber)
    {
        var user = new UserInfoModel(phoneNumber);
        var result = await userManager.CreateAsync(user);
        if (result.Succeeded) return user;

        logger.Log(LogLevel.Error, result.Errors.ToString());
        return null;
    }

    public async Task<UserInfoModel?> Put(PutUserInfoModelDto putUserInfoModelDto)
    {
        var userInfo = await GetByPhone(putUserInfoModelDto.PhoneNumber);
        if (userInfo == null) return userInfo;
        
        userInfo.Update(putUserInfoModelDto);
        var identityResult = await userManager.UpdateAsync(userInfo);
        if (identityResult.Succeeded) return userInfo;

        logger.Log(LogLevel.Error, identityResult.Errors.ToString());
        return null;
    }

    public async Task<UserInfoModel?> Patch(PatchUserInfoModelDto patchCategoryModelDto)
    {
        var userInfo = await GetByPhone(patchCategoryModelDto.PhoneNumber);
        if (userInfo == null) return userInfo;
        
        userInfo.Update(patchCategoryModelDto);
        var identityResult = await userManager.UpdateAsync(userInfo);
        if (identityResult.Succeeded) return userInfo;

        logger.Log(LogLevel.Error, identityResult.Errors.ToString());
        return null;
    }

    public async Task<UserInfoModel?> GetByPhone(string phoneNumber)
    {
        var userInfo = await userManager.FindByIdAsync(phoneNumber);
        return userInfo;
    }

    public async Task<UserInfoModel?> DeleteByPhone(string phoneNumber)
    {
        var user = await GetByPhone(phoneNumber);
        if (user == null) return user;
        
        var deleteResult = await userManager.DeleteAsync(user);
        if (deleteResult.Succeeded) return user;
        
        logger.Log(LogLevel.Error, deleteResult.Errors.ToString());
        return null;
    }
}