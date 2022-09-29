using System.Security.Claims;
using LicensePlate.Core.Models;

namespace LicensePlate.Core.Interfaces;

public interface IUserService
{
    ValueTask<bool> ValidateUser(string username);
    ValueTask<bool> ValidateUser(string username, string password);
    Task<IEnumerable<Claim>> GetUserClaimsAsync(string username);
    Task<string> GetUserNameById(string userId);
    ValueTask<bool> CreateUserAsync(UserModel newUser, string password);
    ValueTask<bool> IsExistedEmail(string email);
    ValueTask<bool> IsExistedUserName(string username);
    ValueTask<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword);
}