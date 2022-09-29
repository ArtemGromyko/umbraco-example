using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Models;
using Microsoft.AspNetCore.Identity;
using Umbraco.Cms.Core.Security;

namespace LicensePlate.Core.Abstraction;

public abstract class MembershipProviderAbstract<TUmbracoUser> : IMembershipProvider
    where TUmbracoUser : UmbracoIdentityUser
{
    private readonly IUmbracoUserManager<TUmbracoUser> _userManager;

    public MembershipProviderAbstract(IUmbracoUserManager<TUmbracoUser> userManager)
    {
        _userManager = userManager;
    }

    protected abstract TUmbracoUser Map(UserModel user);

    protected abstract UserModel Map(TUmbracoUser user, IEnumerable<string> roles);

    public async ValueTask<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword)
    {
        TUmbracoUser umbracoUser = await _userManager.FindByNameAsync(username);
        IdentityResult identityResult = await _userManager.ChangePasswordAsync(umbracoUser, oldPassword, newPassword);

        return identityResult.Succeeded;
    }

    public async ValueTask<bool> CreateUserAsync(UserModel newUser, string password)
    {
        TUmbracoUser umbracoUser = Map(newUser);
        IdentityResult identityResult = await _userManager.CreateAsync(umbracoUser, password);

        return identityResult.Succeeded;
    }

    public async Task<UserModel> GetUserAsync(string username)
    {
        TUmbracoUser umbracoUser = await _userManager.FindByNameAsync(username);
        IEnumerable<string> roles = umbracoUser.Roles.Select(r => r.RoleId);

        UserModel user = Map(umbracoUser, roles);
        return user;
    }

    public async Task<UserModel> GetUserByIdAsync(string userId)
    {
        TUmbracoUser umbracoUser = await _userManager.FindByIdAsync(userId);
        IEnumerable<string> roles = umbracoUser.Roles.Select(r => r.RoleId);

        UserModel user = Map(umbracoUser, roles);
        return user;
    }

    public async ValueTask<bool> IsExistedEmail(string email)
    {
        TUmbracoUser umbracoUser = await _userManager.FindByEmailAsync(email);
        return umbracoUser is not null;
    }

    public async ValueTask<bool> IsExistedUserName(string username)
    {
        TUmbracoUser umbracoUser = await _userManager.FindByNameAsync(username);
        return umbracoUser is not null;
    }

    public async ValueTask<bool> ValidateUserAsync(string username)
    {
        TUmbracoUser umbracoUser = await _userManager.FindByNameAsync(username);
        return umbracoUser is not null;
    }

    public async ValueTask<bool> ValidateUserAsync(string username, string password)
    {
        return await _userManager.ValidateCredentialsAsync(username, password);
    }
}