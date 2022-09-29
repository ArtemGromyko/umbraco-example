using FluentValidation;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Infrastructure.Extensions;

namespace LicensePlate.Core.Infrastructure.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserName).NotEmptyWtihMassage();
        RuleFor(x => x.Password).NotEmptyWtihMassage();
    }
}