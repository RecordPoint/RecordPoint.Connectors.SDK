using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class ContentManagerOperationSut : ContentManagerSutBase
    {

        protected override IHostBuilder CreateSutBuilder()
        {
            SelectContentManagerCallbackActionMock(new Mock<IContentManagerCallbackAction>());

            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .UseDatabaseAggregationManager()
                .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
                .ConfigureServices((context, svcs) => {
                     var contentManagerConfiguration = context.Configuration.GetSection("ContentManager");
                     svcs
                         .Configure<ContentManagerOptions>(contentManagerConfiguration)
                         .AddSingleton<ContentManagerOperationTestWrapper>();
                 });
        }

    }
}
