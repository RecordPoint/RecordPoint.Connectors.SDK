using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    public static class ContentManagerBuilderExtensions
    {
        public static IHostBuilder UseContentManagerService(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .Configure<ContentManagerOptions>(configuration.GetSection(ContentManagerOptions.SECTION_NAME))
                    .AddAddQueueableWorkOperation<ContentManagerOperation>(ContentManagerOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddHostedService<ContentManagerService>();
            });
        }

        public static IHostBuilder UseChannelDiscoveryOperation<TAction>(this IHostBuilder hostBuilder)
            where TAction : class, IChannelDiscoveryAction
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .Configure<ChannelDiscoveryOperationOptions>(configuration.GetSection(ChannelDiscoveryOperationOptions.SECTION_NAME))
                    .AddAddQueueableWorkOperation<ChannelDiscoveryOperation>(ChannelDiscoveryOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IChannelDiscoveryAction, TAction>();
            });
        }

        public static IHostBuilder UseContentSynchronisationOperation<TAction>(this IHostBuilder hostBuilder)
            where TAction : class, IContentSynchronisationAction
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .Configure<ContentSynchronisationOperationOptions>(configuration.GetSection(ContentSynchronisationOperationOptions.SECTION_NAME))
                    .AddAddQueueableWorkOperation<ContentSynchronisationOperation>(ContentSynchronisationOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IContentSynchronisationAction, TAction>();
            });
        }

        public static IHostBuilder UseContentRegistrationOperation<TAction>(this IHostBuilder hostBuilder)
            where TAction : class, IContentRegistrationAction
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                .Configure<ContentRegistrationOperationOptions>(configuration.GetSection(ContentRegistrationOperationOptions.SECTION_NAME))
                    .AddAddQueueableWorkOperation<ContentRegistrationOperation>(ContentRegistrationOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IContentRegistrationAction, TAction>();
            });
        }

        public static IHostBuilder UseRecordSubmissionOperation(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitRecordOperation>(SubmitRecordOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>();
            });
        }

        public static IHostBuilder UseRecordSubmissionOperation<TCallbackAction>(this IHostBuilder hostBuilder)
            where TCallbackAction : class, IRecordSubmissionCallbackAction
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitRecordOperation>(SubmitRecordOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IRecordSubmissionCallbackAction, TCallbackAction>();
            });
        }

        public static IHostBuilder UseBinarySubmissionOperation<TAction>(this IHostBuilder hostBuilder)
            where TAction : class, IBinaryRetrievalAction
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitBinaryOperation>(SubmitBinaryOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IBinaryRetrievalAction, TAction>();
            });
        }

        public static IHostBuilder UseBinarySubmissionOperation<TAction, TCallbackAction>(this IHostBuilder hostBuilder)
            where TAction : class, IBinaryRetrievalAction
            where TCallbackAction : class, IBinarySubmissionCallbackAction
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitBinaryOperation>(SubmitBinaryOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IBinaryRetrievalAction, TAction>()
                    .AddTransient<IBinarySubmissionCallbackAction, TCallbackAction>();
            });
        }

        public static IHostBuilder UseAggregationSubmissionOperation(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitAggregationOperation>(SubmitAggregationOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>();
            });
        }

        public static IHostBuilder UseAggregationSubmissionOperation<TCallbackAction>(this IHostBuilder hostBuilder)
            where TCallbackAction : class, IAggregationSubmissionCallbackAction
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitAggregationOperation>(SubmitAggregationOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IAggregationSubmissionCallbackAction, TCallbackAction>();
            });
        }

        public static IHostBuilder UseAuditEventSubmissionOperation(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitAuditEventOperation>(SubmitAuditEventOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>();
            });
        }

        public static IHostBuilder UseAuditEventSubmissionOperation<TCallbackAction>(this IHostBuilder hostBuilder)
            where TCallbackAction : class, IAuditEventSubmissionCallbackAction
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitAuditEventOperation>(SubmitAuditEventOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IAuditEventSubmissionCallbackAction, TCallbackAction>();
            });
        }

        public static IHostBuilder UseRecordDisposalOperation<TAction>(this IHostBuilder hostBuilder)
            where TAction : class, IRecordDisposalAction
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<RecordDisposalOperation>(RecordDisposalOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IRecordDisposalAction, TAction>();
            });
        }
    }
}