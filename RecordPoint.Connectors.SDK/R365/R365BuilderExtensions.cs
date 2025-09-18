using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;

namespace RecordPoint.Connectors.SDK.R365
{

    /// <summary>
    /// R365 Builder Extensions
    /// </summary>
    public static class R365BuilderExtensions
    {
        /// <summary>
        /// Use the R365 Integration Client Components
        /// </summary>
        /// <param name="hostBuilder">Host builder to target</param>
        public static IHostBuilder UseR365Integration(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var options = hostContext.Configuration
                    .GetSection(RecordSubmissionOptions.SECTION_NAME)
                    .Get<RecordSubmissionOptions>()
                    ?? new RecordSubmissionOptions();
                
                services.AddR365Integration(options.SubmitRecordAndBinariesSynchronously);
            });
        }

        /// <summary>
        /// Add R365 Access component
        /// </summary>
        /// <param name="services"></param>
        /// <param name="submitRecordAndBinariesSynchronously"></param>
        /// <returns></returns>
        public static IServiceCollection AddR365Integration(this IServiceCollection services, bool submitRecordAndBinariesSynchronously)
        {
            services
                .AddR365ClientComponents()
                .AddSingleton<ILog, SDKLogAdapter>()
                .AddSingleton<IR365Client, R365Client>()
                .AddSingleton(provider => CreatePipelines(provider, submitRecordAndBinariesSynchronously));
            return services;
        }

        private static IServiceCollection AddR365ClientComponents(this IServiceCollection services)
        {
            services.AddSingleton<IApiClientFactory, ApiClientFactory>();

            // The CircuitBreaker that DirectSubmitBinaryPipelineElement will use
            services.AddSingleton(typeof(AzureBlobRetryProviderWithCircuitBreaker), (provider) =>
            {
                var options = new CircuitBreakerOptions();
                return new AzureBlobRetryProviderWithCircuitBreaker(options, true);
            });
            services.AddSingleton(typeof(ISdkAzureBlobCircuitProvider), (provider) =>
            {
                return provider.GetService<AzureBlobRetryProviderWithCircuitBreaker>();
            });
            services.AddSingleton(typeof(ISdkAzureBlobRetryProvider), (provider) =>
            {
                return provider.GetService<AzureBlobRetryProviderWithCircuitBreaker>();
            });

            var cloudBlobFactory = new Func<string, BlobClient>(SASToken =>
            {
                var blobClientOptions = new BlobClientOptions();
                blobClientOptions.Retry.MaxRetries = 0;
                return new BlobClient(new Uri(SASToken), options: blobClientOptions);
            });
            services.AddSingleton(cloudBlobFactory);

            // The CircuitProvider to handle TooManyRequests from connector api
            services.AddSingleton(typeof(ISettableCircuitProvider), (provider) =>
            {
                var dt = provider.GetService<IDateTimeProvider>();
                return new SettableCircuitProvider()
                {
                    DateTimeProvider = dt
                };
            });
            return services;
        }

        private static IR365Pipelines CreatePipelines(this IServiceProvider provider, bool submitRecordAndBinariesSynchronously)
        {
            return new R365Pipelines(
                CreateRecordPipeline(provider),
                CreateBinaryPipeline(provider, submitRecordAndBinariesSynchronously),
                CreateAggregationPipeline(provider),
                CreateAuditEventPipeline(provider));
        }


        /// <summary>
        /// Setup multiple pipeline for submission
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private static ISubmission CreateRecordPipeline(this IServiceProvider provider)
        {

            var httpSubmitItemPipelineElement = new HttpSubmitItemPipelineElement(null)
            {
                ApiClientFactory = provider.GetService<IApiClientFactory>(),
                Log = provider.GetService<ILog>()
            };

            httpSubmitItemPipelineElement.ApiClientFactory = provider.GetService<IApiClientFactory>();

            // Start submission with Filter then HttpSubmitItem
            var filterSubmission = new FilterPipelineElement(httpSubmitItemPipelineElement)
            {
                Log = provider.GetService<ILog>()
            };

            return filterSubmission;
        }

        private static DirectSubmitBinaryPipelineElement CreateBinaryPipeline(this IServiceProvider provider, bool submitRecordAndBinariesSynchronously)
        {
            return new DirectSubmitBinaryPipelineElement(null, submitRecordAndBinariesSynchronously)
            {
                BlobFactory = provider.GetRequiredService<Func<string, BlobClient>>(),
                ApiClientFactory = provider.GetRequiredService<IApiClientFactory>(),
                CircuitProvider = provider.GetRequiredService<ISdkAzureBlobCircuitProvider>(),
                RetryProvider = provider.GetRequiredService<ISdkAzureBlobRetryProvider>(),
                Log = provider.GetService<ILog>()
            };
        }

        private static HttpSubmitAggregationPipelineElement CreateAggregationPipeline(this IServiceProvider provider)
        {
            return new HttpSubmitAggregationPipelineElement(null)
            {
                ApiClientFactory = provider.GetService<IApiClientFactory>(),
                Log = provider.GetRequiredService<ILog>()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static HttpSubmitAuditEventPipelineElement CreateAuditEventPipeline(this IServiceProvider provider)
        {
            return new HttpSubmitAuditEventPipelineElement(null)
            {
                ApiClientFactory = provider.GetService<IApiClientFactory>(),
                Log = provider.GetRequiredService<ILog>()
            };
        }

    }
}
