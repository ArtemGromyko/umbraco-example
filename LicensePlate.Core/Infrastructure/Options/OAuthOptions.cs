using LicensePlate.Core.Interfaces;

namespace LicensePlate.Core.Infrastructure.Options;

public class OAuthOptions : IOAuthOptions
{
    public const string Authentication = "OAuth";

    public string SymmetricKey { get; set; }

    public string AllowedOrigin { get; set; }

    public int AccessTokenLifeTime { get; set; }

    public int RefreshTokenLifeTime { get; set; }

    public bool AllowInsecureHttp { get; set; }
    
    public string MembershipProviderName { get; set; }
}