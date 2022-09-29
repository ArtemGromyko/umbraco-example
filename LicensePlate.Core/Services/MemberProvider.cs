using LicensePlate.Core.Abstraction;
using LicensePlate.Core.Models;
using Umbraco.Cms.Core.Security;

namespace LicensePlate.Core.Services;

public class MemberProvider : MembershipProviderAbstract<MemberIdentityUser>
{
    public MemberProvider(IMemberManager memberManager) : base(memberManager)
    {
    }

    protected override UserModel Map(MemberIdentityUser member, IEnumerable<string> roles) => new()
    {
        Id = member.Key,
        Name = member.Name,
        UserName = member.UserName,
        Email = member.Email,
        IsApproved = member.IsApproved,
        IsLockedOut = member.IsLockedOut,
        Roles = roles
    };

    protected override MemberIdentityUser Map(UserModel user) => new()
    {
        Name = user.Name,
        UserName = user.UserName,
        Email = user.Email,
        IsApproved = user.IsApproved
    };
}