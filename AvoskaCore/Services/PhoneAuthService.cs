using Avoska.Models.Users;

namespace Avoska.Services;

public class PhoneAuthService : IPhoneAuthService
{
    private const int AUTH_CODE = 12345;

    public void SendAuthCode(string phoneNumber)
    {
    }

    public Task<bool> VerifyCodeAsync(VerifyCodeModelDto verifyCode)
    {
        return Task.FromResult(verifyCode.Code == AUTH_CODE);
    }
}