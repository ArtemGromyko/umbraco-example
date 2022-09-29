using LicensePlate.Core.Infrastructure.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace LicensePlate.Core.Infrastructure.DependencyInjections;

public static class FiltersDependencyInjection
{
    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddScoped<UmbracoAuthorizationAttribute>();
        return services;
    }
}
