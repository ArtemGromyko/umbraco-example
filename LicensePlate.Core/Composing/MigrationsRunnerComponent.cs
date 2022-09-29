using LicensePlate.Core.Data.Migrations;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Core.Composing;
using Serilog;

namespace LicensePlate.Core.Composing;

internal class MigrationsRunnerComponent : IComponent
{
    private readonly IScopeProvider _scopeProvider;
    private readonly IMigrationPlanExecutor _migrationPlanExecutor;
    private readonly IKeyValueService _keyValueService;
    private readonly IRuntimeState _runtimeState;

    public MigrationsRunnerComponent(IScopeProvider scopeProvider,
                                     IKeyValueService keyValueService,
                                     IRuntimeState runtimeState,
                                     IMigrationPlanExecutor migrationPlanExecutor)
    {
        _scopeProvider = scopeProvider;
        _keyValueService = keyValueService;
        _runtimeState = runtimeState;
        _migrationPlanExecutor = migrationPlanExecutor;
    }

    public void Initialize()
    {
        if (_runtimeState.Level < RuntimeLevel.Run)
        {
            return;
        }

        try
        {
            var plan = new AuthUMigrationPlan();
            var upgrader = new Upgrader(plan);

            upgrader.Execute(_migrationPlanExecutor, _scopeProvider, _keyValueService);
        }
        catch (Exception e)
        {
            Log.Error("Error running Migration Planner migration: {massage}", e);
        }
    }

    public void Terminate()
    { }
}
