#nullable enable
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Databases.Sqlite;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite.Test
{
    public class SqliteConnectorDatabaseBuilderExtensionsTests
    {
        private static IServiceCollection CaptureServices(out IHostBuilder returnedBuilder)
        {
            IServiceCollection? captured = null;

            var hostBuilder = Host.CreateDefaultBuilder();
            returnedBuilder = hostBuilder.UseSqliteConnectorDatabase();

            // Registered after the extension so all of the extension's registrations are visible.
            returnedBuilder.ConfigureServices(services => captured = services);
            returnedBuilder.Build();

            Assert.NotNull(captured);
            return captured!;
        }

        [Fact]
        public void UseSqliteConnectorDatabase_ReturnsSameBuilderInstance()
        {
            var hostBuilder = Host.CreateDefaultBuilder();

            var result = hostBuilder.UseSqliteConnectorDatabase();

            Assert.Same(hostBuilder, result);
        }

        [Fact]
        public void UseSqliteConnectorDatabase_RegistersDatabaseProviderAsSingleton()
        {
            var services = CaptureServices(out _);

            var descriptor = services.Single(d => d.ServiceType == typeof(IConnectorDatabaseProvider));

            Assert.Equal(typeof(SqliteConnectorDatabaseProvider), descriptor.ImplementationType);
            Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
        }

        [Fact]
        public void UseSqliteConnectorDatabase_RegistersDatabaseClientAsSingleton()
        {
            var services = CaptureServices(out _);

            var descriptor = services.Single(d => d.ServiceType == typeof(IConnectorDatabaseClient));

            Assert.Equal(typeof(ConnectorDatabaseClient), descriptor.ImplementationType);
            Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
        }

        [Fact]
        public void UseSqliteConnectorDatabase_RegistersDatabaseHostedService()
        {
            var services = CaptureServices(out _);

            Assert.Contains(services, d =>
                d.ServiceType == typeof(IHostedService) &&
                d.ImplementationType == typeof(DatabaseService<ConnectorDbContext, IConnectorDatabaseProvider>));
        }
    }
}
