using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.R365;
using ReferenceConnector.Common;

namespace ReferenceConnector.ChannelDiscovery
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

            //Setup Reference Connector Content Services
            builder.HostBuilder
                .UseR365Integration()
                .UseChannelDiscoveryOperation<ChannelDiscoveryAction>();

            return builder.HostBuilder;
        }
    }
}