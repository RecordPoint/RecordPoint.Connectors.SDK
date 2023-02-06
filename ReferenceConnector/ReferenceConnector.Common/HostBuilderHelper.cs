using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Configuration.AzureKeyVault;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases.AzureSql;
using RecordPoint.Connectors.SDK.Databases.Cosmos;
using RecordPoint.Connectors.SDK.Databases.LocalDb;
using RecordPoint.Connectors.SDK.Databases.PostgreSql;
using RecordPoint.Connectors.SDK.Databases.Sqlite;
using RecordPoint.Connectors.SDK.Health;
using RecordPoint.Connectors.SDK.Logging.Serilog;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.AppInsights;
using RecordPoint.Connectors.SDK.Status;
using RecordPoint.Connectors.SDK.Time;
using RecordPoint.Connectors.SDK.Toggles.Null;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus;
using RecordPoint.Connectors.SDK.WorkQueue.RabbitMq;
using System.Reflection;
namespace ReferenceConnector.Common
{
    public static class HostBuilderHelper
    {
        public static (IHostBuilder HostBuilder, IConfigurationRoot Configuration) CreateConnectorHostBuilder(string[] args)
        {
            //Setup Configuration Builder
            var configurationBuilder = ConnectorConfigurationBuilder
                .CreateConfigurationBuilder(args, Assembly.GetExecutingAssembly());

            if (args.Any(a => a.ToLower() == "/usekeyvault"))
            {
                configurationBuilder
                     .UseAzureKeyVaultConfigurationProvider();
            }

            //Setup Configuration
            var configuration = configurationBuilder.Build();

            //Create Host Builder
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .UseConfiguration(configuration)
                .UseSystemContext("RecordPoint", "Reference Connector", "RefConn")
                .UseSystemTime();

            //Setup R365 Configuration (AppSettings)
            hostBuilder.UseR365AppSettingsConfiguration();

            //Setup Connector Database
            if (args.Any(a => a.ToLower() == "/usecosmosdb"))
            {
                hostBuilder
                    .UseCosmosDbConnectorDatabase();
            }
            else if (args.Any(a => a.ToLower() == "/useazuresqldb"))
            {
                hostBuilder
                    .UseAzureSqlConnectorDatabase();
            }
            else if (args.Any(a => a.ToLower() == "/usepostgresql"))
            {
                hostBuilder
                    .UsePostgreSqlConnectorDatabase();
            }
            else if (args.Any(a => a.ToLower() == "/usesqllite"))
            {
                hostBuilder
                    .UseSqliteConnectorDatabase();
            }
            else
            {
                hostBuilder
                    .UseLocalDbConnectorDatabase();
            }

            //Setup Database Connector Configuration
            hostBuilder
                .UseDatabaseConnectorConfigurationManager()
                .UseDatabaseChannelManager();

            //Setup Telemetry
            hostBuilder
                .UseConsoleLogging()
                .UseAppInsightsTelemetryTracking();

            //Setup Serilog
            if (args.Any(a => a.ToLower() == "/useserilog"))
            {
                hostBuilder
                    .UseSerilogApplicationInsightsLogger();
            }

            //Setup Feature Toggles
            hostBuilder.UseNullToggleProvider();

            // Use Inmemory Semaphorelock
            hostBuilder.UseInMemorySemaphoreLock<SemaphoreLockScopedKeyAction>();

            //Setup Work Queue
            hostBuilder
                .UseWorkManager()
                .UseWorkStateManager<DatabaseManagedWorkStatusManager>();

            //Check for the Work Queue and deadletter service, default will be Azure Service Bus
            if (args.Any(a => a.ToLower() == "/userabbitmq"))
            {
                hostBuilder
                    .UseRabbitMqWorkQueue()
                    .UseRabbitMqDeadLetterQueueService();
            }
            else
            {
                hostBuilder
                    .UseASBWorkQueue()
                    .UseASBDeadLetterQueueService();
            }

            //Setup Reference Connector
            hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddSingleton<IRandomHelper, RandomHelper>()
                    .Configure<ReferenceConnectorOptions>(configuration.GetSection(ReferenceConnectorOptions.SectionName));
            });

            return (hostBuilder, configuration);
        }
    }
}