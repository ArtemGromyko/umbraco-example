using FluentValidation;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Infrastructure.Extensions;

namespace LicensePlate.Core.Infrastructure.Validators;

public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
{
    public RefreshRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.RefreshToken).NotEmptyWtihMassage();
        RuleFor(x => x.DeviceId).NotEmptyWtihMassage();
    }
}
