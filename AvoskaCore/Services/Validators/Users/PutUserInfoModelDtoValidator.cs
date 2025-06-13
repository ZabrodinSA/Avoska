using Avoska.Models.Users;
using FluentValidation;

namespace Avoska.Services.Validators.Users;

public class PutUserInfoModelDtoValidator : AbstractValidator<PutUserInfoModelDto>
{
    public PutUserInfoModelDtoValidator()
    {
        RuleFor(userInfo => userInfo.Email)
            .NotEmpty()
            .WithMessage("Email should be not empty")
            .EmailAddress()
            .WithMessage("Email not correct");
    }
}