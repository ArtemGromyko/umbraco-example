namespace LicensePlate.Core.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public bool IsApproved { get; set; }
    public bool IsLockedOut { get; set; }
}