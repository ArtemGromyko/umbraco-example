using MediatR;

namespace LicensePlate.Core.Commands.Requests;

public class RevokeRequest : IRequest
{
    public string RefreshToken { get; set; }
    public string DeviceId { get; set; }
}
