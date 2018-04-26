using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.TaskRunner
{
    public class TaskRunnerInformation<T> : TaskRunnerInformationBase
    {
        /// <summary>
        /// A reference to the task object that corresponds to this TaskRunnerInformation object.
        /// </summary>
        public T TaskObject { get; set; }

        public TaskRunnerInformation(Guid tenantId, Guid connectorConfigId, Func<Task> function, ExceptionHandlerFunction exceptionHandler, RepeatedTaskFailureTimeFunction repeatedTaskFailureTimeHandler, T taskObject, CancellationToken cancellationToken)
            : base(tenantId, connectorConfigId, function, exceptionHandler, repeatedTaskFailureTimeHandler, cancellationToken)
        {
            TaskObject = taskObject;
        }
    }
}
