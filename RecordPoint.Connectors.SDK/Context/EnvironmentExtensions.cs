using System;

namespace RecordPoint.Connectors.SDK.Context
{
    /// <summary>
    /// The environment extensions.
    /// </summary>
    public static class EnvironmentExtensions
    {
        /// <summary>
        /// The ASPNETCORE ENVIRONMENT.
        /// </summary>
        private const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
        /// <summary>
        /// The DEVELOPMENT.
        /// </summary>
        private const string DEVELOPMENT = "Development";

        /// <summary>
        /// Get ASP net core environment variable.
        /// </summary>
        /// <returns>A string</returns>
        public static string GetASPNetCoreEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable(ASPNETCORE_ENVIRONMENT) ?? string.Empty;
        }

        /// <summary>
        /// Checks if is development environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <returns>A bool</returns>
        public static bool IsDevelopmentEnvironment(string environment)
        {
            return string.Compare(environment, DEVELOPMENT, true) == 0;
        }

        /// <summary>
        /// Checks if is development environment.
        /// </summary>
        /// <returns>A bool</returns>
        public static bool IsDevelopmentEnvironment()
        {
            return IsDevelopmentEnvironment(GetASPNetCoreEnvironmentVariable());
        }
    }
}
