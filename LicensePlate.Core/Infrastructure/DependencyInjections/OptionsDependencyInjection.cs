using LicensePlate.Core.Infrastructure.Options;
using LicensePlate.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;

namespace LicensePlate.Core.Infrastructure.DependencyInjections;

public static class OptionsDependencyInjection
{
    public static IServiceCollection AddAuthentificationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        AddOAuthOptions(services, configuration);
        AddGlobalSettingOptions(services, configuration);
        return services;
    }

    private static void AddOAuthOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OAuthOptions>(
            configuration.GetSection(OAuthOptions.Authentication)
        );

        services.AddScoped<IOAuthOptions>(provider =>
        {
            return GetOAuthOptions(provider);
        });

        services.AddScoped<IJwtTokenOptions>(provider =>
        {
            return GetOAuthOptions(provider);
        });

        services.AddScoped<IMembershipProviderOptions>(provider =>
        {
            return GetOAuthOptions(provider);
        });
    }

    private static void AddGlobalSettingOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GlobalSettingsOptions>(
            configuration.GetSection(GlobalSettingsOptions.GlobalSettings)
        );

        services.AddScoped<GlobalSettings>(provider =>
        {
            IOptions<GlobalSettingsOptions> options = provider.GetService<IOptions<GlobalSettingsOptions>>();
            return options.Value;
        });
    }

    private static OAuthOptions GetOAuthOptions(IServiceProvider provider)
    {
        IOptions<OAuthOptions> options = provider.GetService<IOptions<OAuthOptions>>();
        return options.Value;
    }

}