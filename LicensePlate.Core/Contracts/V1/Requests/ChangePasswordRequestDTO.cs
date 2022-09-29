namespace LicensePlate.Core.Contracts.V1.Requests;

public class ChangePasswordRequestDTO
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}