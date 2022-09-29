using System.Security.Claims;
using LicensePlate.Core.Infrastructure.Exeptions;
using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Models;
using Microsoft.IdentityModel.JsonWebTokens;

namespace LicensePlate.Core.Services;

public class MembershipProviderUserService : IUserService
{
    private readonly IMembershipProvider _membershipProvider;

    public MembershipProviderUserService(IMembershipProviderResolver resolver,
                                              IMembershipProviderOptions options)
    {
        _membershipProvider = resolver.Resolve(options.MembershipProviderName);
    }

    public async ValueTask<bool> ValidateUser(string username)
    {
        try
        {
            UserModel user = await _membershipProvider.GetUserAsync(username);
            return user != null && user.IsApproved && !user.IsLockedOut;
        }
        catch
        {
            return false;
        }
    }

    public async ValueTask<bool> ValidateUser(string username, string password)
    {
        try
        {
            return await _membershipProvider.ValidateUserAsync(username, password);
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetUserNameById(string userId)
    {
        UserModel user = await _membershipProvider.GetUserByIdAsync(userId);
        return user.UserName;
    }

    public async ValueTask<bool> CreateUserAsync(UserModel newUser, string password)
    {
        return await _membershipProvider.CreateUserAsync(newUser, password);
    }

    public async ValueTask<bool> IsExistedEmail(string email)
    {
        return await _membershipProvider.IsExistedEmail(email);
    }

    public async ValueTask<bool> IsExistedUserName(string username)
    {
        return await _membershipProvider.IsExistedUserName(username);
    }

    public async Task<IEnumerable<Claim>> GetUserClaimsAsync(string username)
    {
        UserModel user;
        try
        {
            user = await _membershipProvider.GetUserAsync(username);
        }
        catch
        {
            throw new InternalExeption($"Couldn't find user with {username}");
        }

        return GenerateClaims(user);
    }

    public async ValueTask<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword)
    {
        return await _membershipProvider.ChangePasswordAsync(username, oldPassword, newPassword);
    }

    private static IEnumerable<Claim> GenerateClaims(UserModel user)
    {
        if (user is not null)
        {
            yield return new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString());
            yield return new Claim(JwtRegisteredClaimNames.Email, user.Email);
            yield return new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName);

            IEnumerable<string> roles = user.Roles;
            foreach (string role in roles)
            {
                yield return new Claim(ClaimTypes.Role, role);
            }
        }
    }
}