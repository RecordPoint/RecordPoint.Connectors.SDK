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
                    .AddQueueableWorkOperation<ContentManagerOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
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
                        .AddScoped<IContentManagerCallbackAction, TCallbackAction>();
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
                    .AddQueueableWorkOperation<ChannelDiscoveryOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IChannelDiscoveryAction, TAction>();
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
                    .AddQueueableWorkOperation<ContentSynchronisationOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IContentSynchronisationAction, TAction>();
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
                    .AddQueueableWorkOperation<ContentRegistrationOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IContentRegistrationAction, TAction>();
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
                    .AddQueueableWorkOperation<SubmitRecordOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>();
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
                    .AddQueueableWorkOperation<SubmitRecordOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IRecordSubmissionCallbackAction, TCallbackAction>();
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
                    .AddQueueableWorkOperation<SubmitBinaryOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IBinaryRetrievalAction, TAction>();
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
                    .AddQueueableWorkOperation<SubmitBinaryOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IBinaryRetrievalAction, TAction>()
                    .AddScoped<IBinarySubmissionCallbackAction, TCallbackAction>();
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
                    .AddQueueableWorkOperation<SubmitAggregationOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>();
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
                    .AddQueueableWorkOperation<SubmitAggregationOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IAggregationSubmissionCallbackAction, TCallbackAction>();
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
                    .AddQueueableWorkOperation<SubmitAuditEventOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>();
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
                    .AddQueueableWorkOperation<SubmitAuditEventOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IAuditEventSubmissionCallbackAction, TCallbackAction>();
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
                    .AddQueueableWorkOperation<RecordDisposalOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IRecordDisposalAction, TAction>();
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
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseGenericQueueableWorkOperation<TOperation, TAction, TInput, TOutput>(this IHostBuilder hostBuilder)
            where TOperation : class, IQueueableWork
            where TAction : class, IGenericAction<TInput, TOutput>
            where TOutput : ActionResultBase
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddQueueableWorkOperation<TOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IGenericAction<TInput, TOutput>, TAction>();
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
        /// <param name="configSectionName">Section name for where to find the Options in the configuration root</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseGenericManagedWorkOperation<TOperation, TAction, TInput, TOutput, TOptions>(this IHostBuilder hostBuilder, string configSectionName)
            where TOperation : class, IQueueableWork
            where TAction : class, IGenericManagedAction<TInput, TOutput>
            where TOutput : ActionResultBase
            where TOptions : class
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .Configure<TOptions>(configuration.GetSection(configSectionName))
                    .AddQueueableWorkOperation<TOperation>()
                    .AddScoped<IContentManagerActionProvider, ContentManagerActionProvider>()
                    .AddScoped<IGenericManagedAction<TInput, TOutput>, TAction>();
            });
        }
    }
}