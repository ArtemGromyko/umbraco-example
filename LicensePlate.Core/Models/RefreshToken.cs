using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace LicensePlate.Core.Models;

[PrimaryKey("Id")]
[TableName(nameof(RefreshToken))]
public class RefreshToken
{
    [PrimaryKeyColumn]
    public int Id { get; set; }

    [NullSetting(NullSetting = NullSettings.NotNull)]
    public string Token { get; set; }

    [NullSetting(NullSetting = NullSettings.NotNull)]
    [SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
    public string JwtId { get; set; }

    [NullSetting(NullSetting = NullSettings.NotNull)]
    public DateTime CreationDate { get; set; }

    [NullSetting(NullSetting = NullSettings.NotNull)]
    public DateTime ExpireDate { get; set; }

    [NullSetting(NullSetting = NullSettings.NotNull)]
    public string UserId { get; set; }

    [NullSetting(NullSetting = NullSettings.NotNull)]
    public string DeviceId { get; set; }
}
