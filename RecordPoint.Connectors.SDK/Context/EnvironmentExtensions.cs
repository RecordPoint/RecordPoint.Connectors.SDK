using System;

namespace RecordPoint.Connectors.SDK.Context
{
    public static class EnvironmentExtensions
    {
        private const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
        private const string DEVELOPMENT = "Development";

        public static string GetASPNetCoreEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable(ASPNETCORE_ENVIRONMENT);
        }
        public static bool IsDevelopmentEnvironment(string environment)
        {
            return string.Compare(environment, DEVELOPMENT, true) == 0;
        }

        public static bool IsDevelopmentEnvironment()
        {
            return IsDevelopmentEnvironment(GetASPNetCoreEnvironmentVariable());
        }
    }
}
