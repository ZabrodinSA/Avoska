using Avoska.Models.Users;
using FluentValidation;

namespace Avoska.Services.Validators.Users;

public class VerifyCodeModelDtoValidator : AbstractValidator<VerifyCodeModelDto>
{
    private const int PHONE_NUMBER_MINIMUM_LENGTH = 10;
    
    public VerifyCodeModelDtoValidator()
    {
        RuleFor(verifyCode => verifyCode.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number should be not empty")
            .MinimumLength(PHONE_NUMBER_MINIMUM_LENGTH)
            .WithMessage($"The phone number must contain at least {PHONE_NUMBER_MINIMUM_LENGTH} characters");

        RuleFor(verifyCode => verifyCode.Code)
            .NotEmpty()
            .WithMessage("Verify code should be not empty");
    }
}