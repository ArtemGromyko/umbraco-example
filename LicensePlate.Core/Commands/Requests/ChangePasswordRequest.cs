using MediatR;

namespace LicensePlate.Core.Commands.Requests;

public class ChangePasswordRequest : IRequest
{
    public string UserName { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}