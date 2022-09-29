using FluentValidation;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Infrastructure.Extensions;

namespace LicensePlate.Core.Infrastructure.Validators;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.UserName).NotEmptyWtihMassage();
        RuleFor(x => x.OldPassword).NotEmptyWtihMassage();
        RuleFor(x => x.NewPassword).NotEmptyWtihMassage()
            .Must((request, newPassword) => !newPassword.Equals(request.OldPassword))
            .WithMessage("Old and new passwords is equals");
        RuleFor(x => x.ConfirmNewPassword).NotEmptyWtihMassage()
            .Matches(x => x.NewPassword);
    }
}