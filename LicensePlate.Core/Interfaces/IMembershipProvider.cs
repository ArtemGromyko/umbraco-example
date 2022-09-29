using LicensePlate.Core.Models;

namespace LicensePlate.Core.Interfaces;

public interface IMembershipProvider
{
    ValueTask<bool> ValidateUserAsync(string username);
    ValueTask<bool> ValidateUserAsync(string username, string password);
    Task<UserModel> GetUserAsync(string username);
    Task<UserModel> GetUserByIdAsync(string userId);
    ValueTask<bool> CreateUserAsync(UserModel newUser, string password);
    ValueTask<bool> IsExistedEmail(string email);
    ValueTask<bool> IsExistedUserName(string username);
    ValueTask<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword);
}