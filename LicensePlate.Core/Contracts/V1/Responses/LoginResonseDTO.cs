namespace LicensePlate.Core.Contracts.V1.Responses;

public class LoginResponseDTO
{
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }
}