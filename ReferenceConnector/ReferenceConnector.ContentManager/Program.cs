using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.ContentManager;
using ReferenceConnector.Common;

namespace ReferenceConnector.ContentManager
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

            //Use Content Manager Service
            builder.HostBuilder.UseContentManagerService();

            return builder.HostBuilder;
        }
    }
}