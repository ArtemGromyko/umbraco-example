using LicensePlate.Core.Commands.Responses;
using MediatR;

namespace LicensePlate.Core.Commands.Requests;

public class RegistrationRequest : IRequest<RegistrationResponse>
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string DeviceId { get; set; }
}
