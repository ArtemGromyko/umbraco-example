namespace LicensePlate.Core.Contracts.V1.Requests;

public class RegistrationRequestDTO
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string DeviceId { get; set; }
}