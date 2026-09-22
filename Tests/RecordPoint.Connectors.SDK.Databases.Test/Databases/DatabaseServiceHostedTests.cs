#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Databases
{
    /// <summary>
    /// SUT that registers the hosted <see cref="DatabaseService{TDbContext,TDbProvider}"/>
    /// via <see cref="DatabaseBuilderExtensions.AddDatabaseService"/>.
    /// </summary>
    public class DatabaseServiceHostedSut : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
            => base
                .CreateSutBuilder()
                .UseMockConnectorDatabase()
                .AddDatabaseService();

        public override async Task StopSUTAsync()
        {
            var databaseProvider = Services!.GetRequiredService<IConnectorDatabaseProvider>();
            await databaseProvider.RemoveAsync(CancellationToken.None);
            await base.StopSUTAsync();
        }
    }

    public class DatabaseServiceHostedTests : CommonTestBase<DatabaseServiceHostedSut>
    {
        [Fact]
        public async Task StartingHost_PreparesDatabase_AndMarksProviderReady()
        {
            await StartSutAsync();

            var databaseProvider = Services!.GetRequiredService<IConnectorDatabaseProvider>();

            // PrepareAsync (via the hosted DatabaseService) sets the mock provider to exist.
            Assert.True(databaseProvider.Exists());

            // ReadyAsync should complete without throwing once the service has run.
            await databaseProvider.ReadyAsync(CancellationToken.None);
        }

        [Fact]
        public async Task StoppingHost_RunsShutdownCleanup()
        {
            await StartSutAsync();

            // Stopping the host triggers DatabaseService.OnShutdown via ApplicationStopped.
            // The shutdown path should execute cleanly without throwing.
            Assert.Null(await Record.ExceptionAsync(() => StopSUTAsync()));
        }
    }

    /// <summary>
    /// A connector database provider whose <see cref="PrepareAsync"/> always fails, used to
    /// exercise the failure branch of <see cref="DatabaseService{TDbContext,TDbProvider}"/>.
    /// </summary>
    public sealed class FailingConnectorDatabaseProvider : IConnectorDatabaseProvider
    {
        public const string FAILURE_MESSAGE = "Simulated prepare failure";

        public Exception? ReadySetWith { get; private set; }
        public bool ReadyWasSet { get; private set; }

        public string GetExternalSystemName() => "Failing Database";
        public string GetConnectionString() => throw new NotImplementedException();
        public bool Exists() => false;

        public Task PrepareAsync(CancellationToken cancellationToken)
            => throw new InvalidOperationException(FAILURE_MESSAGE);

        public Task CleanupAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task RemoveAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task ReadyAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public void SetReady(Exception exception)
        {
            ReadyWasSet = true;
            ReadySetWith = exception;
        }

        public ConnectorDbContext CreateDbContext() => throw new NotImplementedException();
    }

    public class DatabaseServiceFailingSut : CommonSutBase
    {
        public readonly FailingConnectorDatabaseProvider Provider = new();

        protected override IHostBuilder CreateSutBuilder()
            => base
                .CreateSutBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IConnectorDatabaseProvider>(Provider);
                    services.AddSingleton<IConnectorDatabaseClient>(sp =>
                        new ConnectorDatabaseClient(sp.GetRequiredService<IConnectorDatabaseProvider>()));
                })
                .AddDatabaseService();
    }

    public class DatabaseServiceFailingTests : CommonTestBase<DatabaseServiceFailingSut>
    {
        [Fact]
        public async Task StartingHost_WhenPrepareFails_SetsProviderReadyWithException()
        {
            await StartSutAsync();

            Assert.NotNull(SUT);
            Assert.True(SUT!.Provider.ReadyWasSet);
            Assert.NotNull(SUT.Provider.ReadySetWith);
            Assert.Equal(FailingConnectorDatabaseProvider.FAILURE_MESSAGE, SUT.Provider.ReadySetWith!.Message);
        }
    }
}
