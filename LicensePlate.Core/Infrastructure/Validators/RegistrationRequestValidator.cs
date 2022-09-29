using FluentValidation;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Infrastructure.Extensions;

namespace LicensePlate.Core.Infrastructure.Validators;

public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name).NotEmptyWtihMassage();
        RuleFor(x => x.Email).NotEmptyWtihMassage()
            .EmailAddress();
        RuleFor(x => x.UserName).NotEmptyWtihMassage();
        RuleFor(x => x.Password).NotEmptyWtihMassage()
            .MinimumLength(10);
        RuleFor(x => x.ConfirmPassword).NotEmptyWtihMassage()
            .Matches(x => x.Password);
        RuleFor(x => x.DeviceId).NotEmptyWtihMassage();
    }
}
