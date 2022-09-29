using LicensePlate.Core.Data;
using LicensePlate.Core.Infrastructure.Middleware;
using LicensePlate.Core.Infrastructure.Resolver;
using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Models;
using LicensePlate.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NPoco;
using System.Data.Common;

namespace LicensePlate.Core.Infrastructure.DependencyInjections;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration conf)
    {
        services.AddScoped<MemberProvider>();
        services.AddScoped<UserProvider>();
        services.AddScoped<IMembershipProviderResolver, MembershipProviderResolver>();

        services.AddScoped<IUserService, MembershipProviderUserService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddScoped<IRefreshTokenRepository<RefreshToken>, UmbracoDbRefreshTokenRepository>();

        services.AddScoped<ExceptionHandlingMiddleware>();
        services.AddScoped<UmbracoApiAutentificationMiddlware>();

        services.AddScoped<IDatabase>(p =>
        {
            string connectionString = conf.GetConnectionString("umbracoDbDSN");
            string dbProviderString = conf.GetConnectionString("umbracoDbDSN_ProviderName");
            DbProviderFactory dbProvider = DbProviderFactories.GetFactory(dbProviderString);

            return new Database(connectionString, DatabaseType.SqlServer2012, dbProvider);
        });

        return services;
    }
}