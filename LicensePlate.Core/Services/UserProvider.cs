using LicensePlate.Core.Abstraction;
using LicensePlate.Core.Models;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Security;

namespace LicensePlate.Core.Services;

public class UserProvider : MembershipProviderAbstract<BackOfficeIdentityUser>
{
    private readonly GlobalSettings _globalSettings;

    public UserProvider(IBackOfficeUserManager userManager,
                        GlobalSettings globalSettings) : base(userManager)
    {
        _globalSettings = globalSettings;
    }

    protected override UserModel Map(BackOfficeIdentityUser backOffeceUser, IEnumerable<string> roles)
    {
        return new()
        {
            Id = backOffeceUser.Key,
            Name = backOffeceUser.Name,
            Email = backOffeceUser.Email,
            UserName = backOffeceUser.UserName,
            IsApproved = backOffeceUser.IsApproved,
            IsLockedOut = backOffeceUser.IsLockedOut,
            Roles = roles
        };
    }

    protected override BackOfficeIdentityUser Map(UserModel user)
    {
        return BackOfficeIdentityUser.CreateNew(globalSettings: _globalSettings,
                                                username: user.UserName,
                                                email: user.Email,
                                                culture: "en",
                                                name: user.UserName);
    }
}