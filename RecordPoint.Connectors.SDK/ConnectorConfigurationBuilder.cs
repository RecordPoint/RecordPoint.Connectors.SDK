using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Context;
using System;
using System.Reflection;

namespace RecordPoint.Connectors.SDK
{
    /// <summary>
    /// Connector Configuration support
    /// </summary>
    public static class ConnectorConfigurationBuilder
    {

        /// <summary>
        /// Creates a Connector configuration builder.
        /// </summary>
        /// <param name="hostBuilder">The host builder to be configured</param>
        /// <param name="args">Application args</param>
        /// <param name="connectorAssembly">Assembly of the application being configured</param>
        /// <returns>Configuration builder</returns>
        /// <remarks>
        /// Create a configuration that loads settings from the following sources:
        /// + The appsettings.json file.
        /// + The appsetings.{Environment}.json files.
        /// + Environment values
        /// + Command line arguments
        /// </remarks>
        public static IHostBuilder UseAppSettings(this IHostBuilder hostBuilder, string[] args, Assembly connectorAssembly)
        {
            return hostBuilder.ConfigureAppConfiguration((context, builder) =>
            {
                var environment = context.HostingEnvironment.EnvironmentName;
                builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                if (EnvironmentExtensions.IsDevelopmentEnvironment(environment))
                {
                    builder.AddUserSecrets(connectorAssembly, true);
                }

                builder.AddCommandLine(args);
            });
        }

        /// <summary>
        /// Creates a Connector configuration builder.
        /// </summary>
        /// <param name="args">Application args</param>
        /// <returns>Configuration builder</returns>
        /// <remarks>
        /// Create a configuration that loads settings from the following sources:
        /// + The appsettings.json file.
        /// + The appsetings.{Environment}.json files.
        /// + Environment values
        /// + Command line arguments
        /// </remarks>
        public static IConfigurationBuilder CreateConfigurationBuilder(string[] args, Assembly connectorAssembly)
        {
            var environment = EnvironmentExtensions.GetASPNetCoreEnvironmentVariable();
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (EnvironmentExtensions.IsDevelopmentEnvironment(environment))
            {
                builder.AddUserSecrets(connectorAssembly, true);
            }

            builder.AddCommandLine(args);

            return builder;
        }

        /// <summary>
        /// Adds the built Connector configuration to the Host Builder
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IHostBuilder UseConfiguration(this IHostBuilder hostBuilder, IConfiguration configuration)
        {
            return hostBuilder.ConfigureAppConfiguration(builder => builder.AddConfiguration(configuration));
        }

    }
}
