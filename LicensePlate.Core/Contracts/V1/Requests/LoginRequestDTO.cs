namespace LicensePlate.Core.Contracts.V1.Requests;

public class LoginRequestDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string DeviceId { get; set; }
}