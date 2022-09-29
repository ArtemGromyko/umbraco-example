using FluentValidation;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Infrastructure.Extensions;

namespace LicensePlate.Core.Infrastructure.Validators;

public class LogoutRequestValidator : AbstractValidator<RevokeRequest>
{
    public LogoutRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.RefreshToken).NotEmptyWtihMassage();
        RuleFor(x => x.DeviceId).NotEmptyWtihMassage();
    }
}
