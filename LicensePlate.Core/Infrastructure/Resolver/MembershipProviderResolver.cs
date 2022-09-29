using System.Reflection;
using LicensePlate.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LicensePlate.Core.Infrastructure.Resolver
{
    public class MembershipProviderResolver : IMembershipProviderResolver
    {
        private readonly IServiceProvider _provider;

        public MembershipProviderResolver(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IMembershipProvider Resolve(string name)
        {
            Type type = Assembly.GetAssembly(typeof(MembershipProviderResolver)).GetType($"LicensePlate.Core.Services.{name}Provider", false, true);
            var instance = _provider.GetRequiredService(type) as IMembershipProvider;
            
            return instance;
        }
    }
}