using Umbraco.Cms.Infrastructure.Migrations;

namespace LicensePlate.Core.Data.Migrations;

/// <summary>
/// This is the plan that runs the AuthU Migrations
/// </summary>
internal class AuthUMigrationPlan : MigrationPlan
{
    /// <summary>
    /// Add the demo id if preferred
    /// </summary>
    /// <param name="CreateDemoClient"></param>
    public AuthUMigrationPlan() : base("AuthU")
    {
        From(InitialState)
            .To<CreateRefreshToken>("RefreshToken");
    }

    /// <summary>
    /// Using the helper method to get the AuthU Key 'Umbraco.Core.Upgrader.State+AuthU' Value
    /// </summary>
    public override string InitialState => string.Empty;
}
