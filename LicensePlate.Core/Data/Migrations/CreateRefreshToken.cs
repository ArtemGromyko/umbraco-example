using LicensePlate.Core.Models;
using Umbraco.Cms.Infrastructure.Migrations;

namespace LicensePlate.Core.Data.Migrations;

public class CreateRefreshToken : MigrationBase
{
    public CreateRefreshToken(IMigrationContext context) : base(context)
    { }

    protected override void Migrate()
    {
        if (!TableExists("ORefreshToken"))
        {
            Create.Table<RefreshToken>().Do();
        }
    }
}
