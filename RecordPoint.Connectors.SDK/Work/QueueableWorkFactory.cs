using Microsoft.Extensions.DependencyInjection;
using System;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Work item factory that uses a service provider
    /// </summary>
    public class QueueableWorkFactory : IQueueableWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QueueableWorkFactory(string workType, Type implementationType, IServiceProvider serviceProvider)
        {
            WorkType = workType;
            ImplementationType = implementationType;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Work type identifier
        /// </summary>
        public string WorkType { get; set; }

        /// <summary>
        /// Work item type identifier
        /// </summary>
        public Type ImplementationType { get; set; }

        /// <summary>
        /// Create the requested work operation
        /// </summary>
        /// <returns></returns>
        public IQueueableWork CreateWorkOperation()
        {
            return _serviceProvider.GetRequiredService(ImplementationType) as IQueueableWork;
        }
    }
}
