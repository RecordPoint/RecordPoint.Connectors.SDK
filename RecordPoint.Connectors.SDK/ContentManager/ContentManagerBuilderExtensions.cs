using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// The content manager builder extensions.
    /// </summary>
    public static class ContentManagerBuilderExtensions
    {
        /// <summary>
        /// Use content manager service.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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

        /// <summary>
        /// Use content manager service.
        /// </summary>
        /// <typeparam name="TCallbackAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseContentManagerService<TCallbackAction>(this IHostBuilder hostBuilder)
            where TCallbackAction : class, IContentManagerCallbackAction
        {
            return hostBuilder
                .UseContentManagerService()
                .ConfigureServices(services =>
                {
                    services
                        .AddTransient<IContentManagerCallbackAction, TCallbackAction>();
                });
        }

        /// <summary>
        /// Use channel discovery operation.
        /// </summary>
        /// <typeparam name="TAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseChannelDiscoveryOperation<TAction>(this IHostBuilder hostBuilder)
            where TAction : class, IChannelDiscoveryAction
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .Configure<ChannelDiscoveryOperationOptions>(configuration.GetSection(ChannelDiscoveryOperationOptions.SECTION_NAME))
                    .Configure<ContentManagerOptions>(configuration.GetSection(ContentManagerOptions.SECTION_NAME))
                    .AddAddQueueableWorkOperation<ChannelDiscoveryOperation>(ChannelDiscoveryOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IChannelDiscoveryAction, TAction>();
            });
        }

        /// <summary>
        /// Use content synchronisation operation.
        /// </summary>
        /// <typeparam name="TAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseContentSynchronisationOperation<TAction>(this IHostBuilder hostBuilder)
            where TAction : class, IContentSynchronisationAction
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .Configure<ContentSynchronisationOperationOptions>(configuration.GetSection(ContentSynchronisationOperationOptions.SECTION_NAME))
                    .Configure<ContentManagerOptions>(configuration.GetSection(ContentManagerOptions.SECTION_NAME))
                    .AddAddQueueableWorkOperation<ContentSynchronisationOperation>(ContentSynchronisationOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IContentSynchronisationAction, TAction>();
            });
        }

        /// <summary>
        /// Use content registration operation.
        /// </summary>
        /// <typeparam name="TAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseContentRegistrationOperation<TAction>(this IHostBuilder hostBuilder)
            where TAction : class, IContentRegistrationAction
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .Configure<ContentRegistrationOperationOptions>(configuration.GetSection(ContentRegistrationOperationOptions.SECTION_NAME))
                    .Configure<ContentManagerOptions>(configuration.GetSection(ContentManagerOptions.SECTION_NAME))
                    .AddAddQueueableWorkOperation<ContentRegistrationOperation>(ContentRegistrationOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IContentRegistrationAction, TAction>();
            });
        }

        /// <summary>
        /// Use record submission operation.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseRecordSubmissionOperation(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitRecordOperation>(SubmitRecordOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>();
            });
        }

        /// <summary>
        /// Use record submission operation.
        /// </summary>
        /// <typeparam name="TCallbackAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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

        /// <summary>
        /// Use binary submission operation.
        /// </summary>
        /// <typeparam name="TAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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

        /// <summary>
        /// Use binary submission operation.
        /// </summary>
        /// <typeparam name="TAction"/>
        /// <typeparam name="TCallbackAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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

        /// <summary>
        /// Use aggregation submission operation.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseAggregationSubmissionOperation(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitAggregationOperation>(SubmitAggregationOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>();
            });
        }

        /// <summary>
        /// Use aggregation submission operation.
        /// </summary>
        /// <typeparam name="TCallbackAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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

        /// <summary>
        /// Use audit event submission operation.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseAuditEventSubmissionOperation(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<SubmitAuditEventOperation>(SubmitAuditEventOperation.WORK_TYPE)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>();
            });
        }

        /// <summary>
        /// Use audit event submission operation.
        /// </summary>
        /// <typeparam name="TCallbackAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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

        /// <summary>
        /// Use record disposal operation.
        /// </summary>
        /// <typeparam name="TAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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

        /// <summary>
        /// Use a generic QueueableWork operation.
        /// </summary>
        /// <typeparam name="TOperation"/>
        /// <typeparam name="TAction"/>
        /// <typeparam name="TInput"/>
        /// <typeparam name="TOutput"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="workType">String to represent the type of work</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseGenericQueueableWorkOperation<TOperation, TAction, TInput, TOutput>(this IHostBuilder hostBuilder, string workType)
            where TOperation : IQueueableWork
            where TAction : class, IGenericAction<TInput, TOutput>
            where TOutput : ActionResultBase
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddAddQueueableWorkOperation<TOperation>(workType)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IGenericAction<TInput, TOutput>, TAction>();
            });
        }

        /// <summary>
        /// Use a generic managed work operation.
        /// Like queueable work, managed work operations still implement IQueueableWork.
        /// The difference here is this will configure options that are usually required by managed work operations.
        /// If you don't need the configured Options, use 'UseGenericQueueableWorkOperation' instead.
        /// </summary>
        /// <typeparam name="TOperation"/>
        /// <typeparam name="TAction"/>
        /// <typeparam name="TInput"/>
        /// <typeparam name="TOutput"/>
        /// <typeparam name="TOptions"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="workType">String to represent the type of work</param>
        /// <param name="configSectionName">Section name for where to find the Options in the configuration root</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseGenericManagedWorkOperation<TOperation, TAction, TInput, TOutput, TOptions>(this IHostBuilder hostBuilder, string workType, string configSectionName)
            where TOperation : IQueueableWork
            where TAction : class, IGenericManagedAction<TInput, TOutput>
            where TOutput : ActionResultBase
            where TOptions : class
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .Configure<TOptions>(configuration.GetSection(configSectionName))
                    .AddAddQueueableWorkOperation<TOperation>(workType)
                    .AddTransient<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddTransient<IGenericManagedAction<TInput, TOutput>, TAction>();
            });
        }
    }
}