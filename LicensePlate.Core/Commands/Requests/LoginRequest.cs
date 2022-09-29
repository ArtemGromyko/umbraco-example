using LicensePlate.Core.Commands.Responses;
using MediatR;

namespace LicensePlate.Core.Commands.Requests;

public class LoginRequest : IRequest<LoginResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string DeviceId { get; set; }
}