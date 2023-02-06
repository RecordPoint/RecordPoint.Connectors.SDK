using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.WebHost;
using ReferenceConnector.Common;

namespace ReferenceConnector.Notifications
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateConnectorHostBuilder(args)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateConnectorHostBuilder(string[] args)
        {
            var builder = HostBuilderHelper.CreateConnectorHostBuilder(args);

            //Use Push Notifications
            return builder.HostBuilder
                .UseWebHost(builder.Configuration)
                .UseWebhookNotifications();
        }
    }
}