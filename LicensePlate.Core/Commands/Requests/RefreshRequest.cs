using LicensePlate.Core.Commands.Responses;
using MediatR;

namespace LicensePlate.Core.Commands.Requests;

public class RefreshRequest : IRequest<RefreshResponse>
{
    public string RefreshToken { get; set; }
    public string DeviceId { get; set; }
}