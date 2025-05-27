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
    /// SUT for Managed Work factory tests
    /// </summary>
    public class ManagedWorkFactorySut : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
            => base
                .CreateSutBuilder()
                .UseWorkManager()
                .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
                .UseMockConnectorDatabase()
                .ConfigureServices(svcs =>
                    svcs
                        .AddMockWorkQueue()
                );

        public override async Task StopSUTAsync()
        {
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();
            await databaseProvider.RemoveAsync(CancellationToken.None);
            await base.StopSUTAsync();
        }
    }

    /// <summary>
    /// Managed Work factory tests
    /// </summary>
    public class ManagedWorkFactoryTests : CommonTestBase<ManagedWorkFactorySut>
    {

        private const string TEST_JOB_ID = "234";

        private static ManagedWorkStatusModel CreateTestManagedWorkStatusModel()
        {
            var testManagedWorkStatusModel = new ManagedWorkStatusModel()
            {
                Configuration = "TestConfiguration",
                ConfigurationType = "TestConfigurationType",
                ConnectorId = "TestConnectorId",
                WorkId = TEST_JOB_ID,
                WorkType = "TestWorkType",
                State = "TestState",
                StateType = "TestStateType",
                Id = "345"
            };
            return testManagedWorkStatusModel;
        }

        [Fact]
        public async Task CreateWork_HappyPath()
        {
            await StartSutAsync();

            var workFactory = Services.GetRequiredService<IManagedWorkFactory>();
            var work = workFactory.CreateWork("TestWorkId", "TestWorkType", "TestConnectorId", "TestConfigurationType", "TestConfiguration");

            Assert.NotNull(work.WorkStatus.WorkId);
            Assert.Equal("TestWorkId", work.WorkStatus.Id);
            Assert.Equal("TestWorkType", work.WorkStatus.WorkType);
            Assert.Equal("TestConnectorId", work.WorkStatus.ConnectorId);
            Assert.Equal("TestConfigurationType", work.WorkStatus.ConfigurationType);
            Assert.Equal("TestConfiguration", work.WorkStatus.Configuration);
        }

        [Fact]
        public async Task Works_AreScoped()
        {
            await StartSutAsync();

            using var scope1 = Services.CreateScope();
            using var scope2 = Services.CreateScope();

            var workFactory1 = scope1.ServiceProvider.GetRequiredService<IManagedWorkFactory>();
            var workFactory2 = scope2.ServiceProvider.GetRequiredService<IManagedWorkFactory>();
            var work1 = workFactory1.CreateWork("TestWorkId1", "TestWorkType", "TestConnectorId", "TestConfigurationType", "TestConfiguration");
            var work2 = workFactory2.CreateWork("TestWorkId2", "TestWorkType", "TestConnectorId", "TestConfigurationType", "TestConfiguration");
            Assert.NotSame(work1, work2);
            Assert.NotSame(work1.WorkStatus.WorkId, work2.WorkStatus.WorkId);
        }


        [Fact]
        public async Task LoadWork_HappyPath()
        {
            await StartSutAsync();

            var workFactory = Services.GetRequiredService<IManagedWorkFactory>();
            var workMessage = CreateTestManagedWorkStatusModel();
            var work = workFactory.LoadWork(workMessage);
            Assert.Equal(workMessage.Configuration, work.WorkStatus.Configuration);
            Assert.Equal(workMessage.ConfigurationType, work.WorkStatus.ConfigurationType);
            Assert.Equal(workMessage.ConnectorId, work.WorkStatus.ConnectorId);
            Assert.Equal(workMessage.WorkId, work.WorkStatus.WorkId);
            Assert.Equal(workMessage.WorkType, work.WorkStatus.WorkType);
            Assert.Equal(workMessage.State, work.WorkStatus.State);
            Assert.Equal(workMessage.StateType, work.WorkStatus.StateType);
            Assert.Equal(workMessage.WorkId, work.WorkStatus.WorkId);
        }

    }

}
