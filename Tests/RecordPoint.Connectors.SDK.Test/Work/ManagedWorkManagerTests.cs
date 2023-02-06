using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Work;

public class ManagedWorkManagerSut : CommonSutBase
{
    public ConnectorOptions ConnectorOptions { get; set; } = new();
    protected override IHostBuilder CreateSutBuilder()
        => base.CreateSutBuilder()
            .UseWorkManager()
            .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
            .UseMockConnectorDatabase()
            .ConfigureServices(svcs =>
                svcs.AddMockWorkQueue()
                    .AddSingleton(Options.Create(ConnectorOptions))
            );
}

public class ManagedWorkManagerTests : CommonTestBase<ManagedWorkManagerSut>
{
    [Fact]
    public async Task FaultyAsync_TestFaultedOutcomes()
    {
        await StartSutAsync();
        var workManager = Services.GetRequiredService<IManagedWorkManager>();

        var work = await workManager.FaultyAsync("BadWorkId", CancellationToken.None, 5);
        Assert.Equal(WorkResultType.DeadLetter,work.ResultType);

        work = await workManager.FaultyAsync("BadWorkId", CancellationToken.None, 2);
        Assert.Equal(WorkResultType.Complete, work.ResultType);

    }
    [Fact]
    public async Task FaultyAsyncUnlimitedRetries_TestFaultedOutcomes()
    {
        await StartSutAsync();

        var workManager = Services.GetRequiredService<IManagedWorkManager>();
        workManager.WorkStatus.RetryOnFailure = true;
        workManager.WorkStatus.MaxRetries = -1;

        var work = await workManager.FaultyAsync("BadWorkId", CancellationToken.None, 5);
        Assert.Equal(WorkResultType.Complete, work.ResultType);

        work = await workManager.FaultyAsync("BadWorkId", CancellationToken.None, 0);
        Assert.Equal(WorkResultType.Complete, work.ResultType);

        work = await workManager.FaultyAsync("BadWorkId", CancellationToken.None, 100);
        Assert.Equal(WorkResultType.Complete, work.ResultType);

    }

}