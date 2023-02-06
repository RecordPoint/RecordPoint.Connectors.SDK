using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.R365;
using ReferenceConnector.Common;

namespace ReferenceConnector.AuditEventSubmission
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
                .UseR365Integration();

            return builder.HostBuilder;
        }
    }
}