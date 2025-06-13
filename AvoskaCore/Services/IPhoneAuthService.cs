using Avoska.Models.Users;

namespace Avoska.Services;

public interface IPhoneAuthService
{
    void SendAuthCode(string phoneNumber);

    Task<bool> VerifyCodeAsync(VerifyCodeModelDto verifyCode);
}