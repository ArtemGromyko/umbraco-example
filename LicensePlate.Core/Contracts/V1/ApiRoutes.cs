namespace LicensePlate.Core.Contracts.V1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = Root + "/" + Version;

    public static class OAuth
    {
        public const string Registration = Base + "/auth/registration";
        public const string Revoke = Base + "/auth/revoke";
        public const string Login = Base + "/auth/login";
        public const string Refresh = Base + "/auth/refresh";
        public const string Password = Base + "/auth/password";
    }
}