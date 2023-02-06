using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Work
{
    /// <summary>
    /// SUT for Managed Work Status Manager tests
    /// </summary>
    public class ManagedWorkStatusManagerSut : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
            => base.CreateSutBuilder()
                .UseWorkManager()
                .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
                .UseMockConnectorDatabase()
                .ConfigureServices(svcs =>
                    svcs.AddMockWorkQueue()
                );

        public override async Task StopSUTAsync()
        {
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();
            await databaseProvider.RemoveAsync(CancellationToken.None);
            await base.StopSUTAsync();
        }
    }

    /// <summary>
    /// Managed Work Status Manager Tests
    /// </summary>
    public class ManagedWorkStatusManagerTests : CommonTestBase<ManagedWorkStatusManagerSut>
    {

        private static ManagedWorkStatusModel CreateTestWorkStatus()
        {
            var testWork = new ManagedWorkStatusModel()
            {
                Configuration = "TestConfig",
                ConfigurationType = "Test1",
                ConnectorId = "123",
                WorkId = "123",
                WorkType = "TestWork",
                Status = ManagedWorkStatuses.Complete,
                Id = Guid.NewGuid().ToString()
            };
            return testWork;
        }

        [Fact]
        public async Task GetMissingWork_ReturnsNull()
        {
            await StartSutAsync();

            var workManager = Services.GetRequiredService<IManagedWorkStatusManager>();
            var work = await workManager.GetWorkStatusAsync("BadWorkId", CancellationToken.None);
            Assert.Null(work);
        }

        [Fact]
        public async Task AddWorkStatus_Happy()
        {
            await StartSutAsync();

            var workManager = Services.GetRequiredService<IManagedWorkStatusManager>();

            var originalWork = CreateTestWorkStatus();
            await workManager.AddWorkStatusAsync(originalWork, CancellationToken.None);
            var savedWork = await workManager.GetWorkStatusAsync(originalWork.Id, CancellationToken.None);
            Assert.Equal(originalWork.Serialize(), savedWork.Serialize());
        }

        [Fact]
        public async Task SetWorkComplete_Happy()
        {
            await StartSutAsync();

            var workManager = Services.GetRequiredService<IManagedWorkStatusManager>();
            var cancellationToken = CancellationToken.None;

            var originalWork = CreateTestWorkStatus();
            await workManager.AddWorkStatusAsync(originalWork, cancellationToken);
            await workManager.SetWorkCompleteAsync(originalWork.Id, cancellationToken);
            var updatedWork = await workManager.GetWorkStatusAsync(originalWork.Id, cancellationToken);
            Assert.Equal(ManagedWorkStatuses.Complete, updatedWork.Status);
        }

        [Fact]
        public async Task SetWorkFailed_Happy()
        {
            await StartSutAsync();

            var workManager = Services.GetRequiredService<IManagedWorkStatusManager>();
            var cancellationToken = CancellationToken.None;

            var originalWork = CreateTestWorkStatus();
            await workManager.AddWorkStatusAsync(originalWork, cancellationToken);
            await workManager.SetWorkFailedAsync(originalWork.Id, cancellationToken);
            var updatedWork = await workManager.GetWorkStatusAsync(originalWork.Id, cancellationToken);
            Assert.Equal(ManagedWorkStatuses.Failed, updatedWork.Status);
        }

        [Fact]
        public async Task SetWorkContinue_Happy()
        {
            await StartSutAsync();

            var workManager = Services.GetRequiredService<IManagedWorkStatusManager>();
            var cancellationToken = CancellationToken.None;

            var originalWork = CreateTestWorkStatus();
            await workManager.AddWorkStatusAsync(originalWork, cancellationToken);
            var nextWorkId = "567";
            await workManager.SetWorkContinueAsync(originalWork.Id, nextWorkId, string.Empty, cancellationToken);
            var updatedWork = await workManager.GetWorkStatusAsync(originalWork.Id, cancellationToken);
            Assert.Equal(nextWorkId, updatedWork.WorkId);
        }

    }
}
