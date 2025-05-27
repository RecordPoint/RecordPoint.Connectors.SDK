using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Host builder extensions for work management
/// </summary>
public static class WorkBuilderExtensions
{
    /// <summary>
    /// Use the standard work manager
    /// </summary>
    /// <param name="hostBuilder">Host builder to customize</param>
    /// <returns>Updated host builder</returns>
    public static IHostBuilder UseWorkManager(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureServices(svcs => svcs.AddSingleton<IQueueableWorkManager, QueueableWorkManager>());
    }

    /// <summary>
    /// Add a work queue item for a given work type
    /// </summary>
    /// <param name="services">Target services collection</param>
    /// <returns>Original service collection</returns>
    public static IServiceCollection AddQueueableWorkOperation<TQueueableWork>(this IServiceCollection services)
        where TQueueableWork : class, IQueueableWork
    {
        return services.AddScoped<IQueueableWork, TQueueableWork>();
    }
}
