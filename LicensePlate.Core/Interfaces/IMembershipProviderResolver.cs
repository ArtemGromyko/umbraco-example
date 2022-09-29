namespace LicensePlate.Core.Interfaces;

public interface IMembershipProviderResolver
{
    IMembershipProvider Resolve(string name);
}