using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.TaskRunner
{
    public abstract class TaskRunnerInformationBase
    {
        /// <summary>
        /// Handler for the final exception thrown by a task which has restarted multiple times.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public delegate Task ExceptionHandlerFunction(Exception e, TaskRunnerInformationBase info);

        /// <summary>
        /// Based on an exception, returns how long the task would expect to have been running before it
        /// encountered this exception.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="defaultTimeSpan">The TimeSpan to be returned if the exception does not
        /// match any of the known exception cases.</param>
        /// <returns></returns>
        public delegate TimeSpan RepeatedTaskFailureTimeFunction(Exception e, TimeSpan defaultTimeSpan);

        /// <summary>
        /// Custom prefix using ILog.
        /// </summary>
        public string LogPrefix { get; set; }

        /// <summary>
        /// The Connector Config ID.
        /// </summary>
        public Guid ConnectorConfigId { get; set; }

        /// <summary>
        /// The Records365 vNext Tenant ID.
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// CorrelationId used to track a submission.
        /// </summary>
        public Guid CorrelationId { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// Linked CancellationToken for tasks.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// The actual task run function invoked in TaskRunnerBase.
        /// </summary>
        public Func<Task> Function { get; set; }

        /// <summary>
        /// Determines how non-task-killer exceptions are handled.
        /// </summary>
        public ExceptionHandlerFunction ExceptionHandler { get; set; }

        /// <summary>
        /// Determines the duration the task runner should wait if task fails. Set to null to use default time.
        /// </summary>
        public RepeatedTaskFailureTimeFunction RepeatedTaskFailureTimeHandler { get; set; }

        /// <summary>
        /// Constructs a new TaskRunnerInformationBase.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="connectorConfigId"></param>
        /// <param name="function"></param>
        /// <param name="exceptionHandler"></param>
        /// <param name="repeatedTaskFailureTimeHandler"></param>
        /// <param name="cancellationToken"></param>
        public TaskRunnerInformationBase(Guid tenantId, Guid connectorConfigId, Func<Task> function, ExceptionHandlerFunction exceptionHandler, RepeatedTaskFailureTimeFunction repeatedTaskFailureTimeHandler, CancellationToken cancellationToken)
        {
            TenantId = tenantId;
            ConnectorConfigId = connectorConfigId;
            Function = function;
            ExceptionHandler = exceptionHandler;
            RepeatedTaskFailureTimeHandler = repeatedTaskFailureTimeHandler;
            CancellationToken = cancellationToken;

            LogPrefix = $"CorrelationId [{CorrelationId}] for ConnectorConfigId [{ConnectorConfigId}] TenantId [{TenantId}]";
        }
    }
}
