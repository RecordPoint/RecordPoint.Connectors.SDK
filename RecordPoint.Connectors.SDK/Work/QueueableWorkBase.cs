using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Base Implementation of unmanaged work that is submitted to a queue for execution.
    /// </summary>
    public abstract class QueueableWorkBase<TParameter> : WorkBase<TParameter>, IQueueableWork
    {
        #region Dependencies
        /// <summary>
        /// Public constructor. Used to inject dependencies
        /// </summary>
        protected QueueableWorkBase(
            IServiceProvider serviceProvider,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            ILogger logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _serviceProvider = serviceProvider;
            _semaphoreLockManager = serviceProvider.GetService<ISemaphoreLockManager>();

            SystemContext = systemContext;
        }

        /// <summary>
        /// System context
        /// </summary>
        protected ISystemContext SystemContext { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        protected readonly ISemaphoreLockManager _semaphoreLockManager;
        #endregion

        #region Work Context
        /// <summary>
        /// Required override that identifies the service the work belongs to
        /// </summary>
        public abstract string ServiceName { get; }

        /// <summary>
        /// Work Request we are handling
        /// </summary>
        public WorkRequest WorkRequest { get; protected set; }

        /// <summary>
        /// Submit Start Time
        /// </summary>
        public DateTimeOffset SubmitDateTime { get; protected set; }

        /// <summary>
        /// Time work must finish by
        /// </summary>
        public DateTimeOffset MustFinishDateTime { get; protected set; }

        /// <summary>
        /// Required override that deserializes the parameter from the work request
        /// </summary>
        /// <returns>Deserialized parameters</returns>
        protected virtual TParameter DeserializeParameter()
        {
            return JsonSerializer.Deserialize<TParameter>(WorkRequest.Body);
        }
        #endregion

        #region Run
        /// <summary>
        /// Execute the work request
        /// </summary>
        /// <param name="workRequest">Work request that defines the work to execute</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        public virtual Task RunWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            if (workRequest.WorkType != WorkType)
                throw new InvalidOperationException($"Work request of type {workRequest.WorkType} does not match this work item");

            WorkRequest = workRequest;
            Id = workRequest.WorkId;
            SubmitDateTime = workRequest.SubmitDateTime;
            MustFinishDateTime = workRequest.MustFinishDateTime;
            var parameter = DeserializeParameter();
            return RunAsync(parameter, cancellationToken);
        }

        #nullable enable
        /// <summary>
        /// Checks to see if a semphore lock has been applied and defers execution until the lock expires
        /// </summary>
        /// <param name="connectorConfigModel"></param>
        /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if a lock has been applied</returns>
        /// enable
        protected async Task<bool> CheckSemaphoreLockAsync(ConnectorConfigModel connectorConfigModel, object? context, CancellationToken cancellationToken)
        {
            if (_semaphoreLockManager != null)
            {
                _semaphoreLockManager.ConnectorConfiguration = connectorConfigModel;
                var semaphoreLockExpiry = await _semaphoreLockManager.GetSemaphoreAsync(WorkType, context, cancellationToken);
                if (semaphoreLockExpiry.HasValue)
                {
                    //Semaphore lock is active so backoff until the semaphore lock expires
                    Deferred($"Semaphore Lock enabled, deferring {WorkType}.", semaphoreLockExpiry);
                    return true;
                }
            }
            return false;
        }
        #nullable disable

        #endregion

        #region Result

        /// <summary>
        /// Deferral wait till time
        /// </summary>
        public DateTimeOffset? WaitTill { get; protected set; }

        /// <summary>
        /// Time from when a request was made, till it was completed.
        /// Includes time spent in queues, getting retried, etc.
        /// </summary>
        public virtual TimeSpan ResultDuration => FinishDateTime - SubmitDateTime;

        /// <summary>
        /// Outcome for the work item
        /// </summary>
        public WorkResult WorkResult => new()
        {
            ResultType = ResultType,
            Reason = ResultReason,
            WaitTill = WaitTill,
            Exception = Exception,
            Duration = ResultDuration
        };

        /// <summary>
        /// Record that work has been abandoned but should be retried later
        /// </summary>
        protected void Abandoned(string reason)
        {
            EnsureIncomplete();
            HasResult = true;
            ResultType = WorkResultType.Abandoned;
            ResultReason = reason;
        }

        /// <summary>
        /// Record that this work item has been abandoned due to an exception and should should be retried later
        /// </summary>
        /// <param name="exception">Exception that is cause of the failure</param>
        protected void Abandoned(Exception exception)
        {
            EnsureIncomplete();
            HasResult = true;
            ResultType = WorkResultType.Abandoned;
            ResultReason = exception.Message;
            Exception = exception;
        }

        /// <summary>
        /// Record that this work item will be deferred to a future time
        /// </summary>
        protected void Deferred(string reason, DateTimeOffset? waitTill)
        {
            EnsureIncomplete();
            HasResult = true;
            ResultType = WorkResultType.Deferred;
            ResultReason = reason;
            WaitTill = waitTill;
        }

        #nullable enable
        /// <summary>
        /// Defers the work and applies a semaphore lock
        /// </summary>
        /// <param name="connectorConfigModel"></param>
        /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
        /// <param name="semaphoreLockType"></param>
        /// <param name="nextDelay"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="RequiredValueNullException"></exception>
        protected async Task HandleBackOffResultAsync(ConnectorConfigModel connectorConfigModel, object? context, SemaphoreLockType? semaphoreLockType, int? nextDelay, CancellationToken cancellationToken)
        {
            if (!semaphoreLockType.HasValue)
                throw new RequiredValueNullException(nameof(semaphoreLockType));

            if (!nextDelay.HasValue)
                throw new RequiredValueNullException(nameof(nextDelay));

            if (_semaphoreLockManager == null)
                throw new RequiredValueNullException(nameof(_semaphoreLockManager));

            _semaphoreLockManager.ConnectorConfiguration = connectorConfigModel;
            await _semaphoreLockManager.SetSemaphoreAsync(semaphoreLockType.Value, WorkType, context, nextDelay.Value, cancellationToken);

            var delay = DateTimeOffset.Now.AddSeconds(nextDelay.Value);
            Deferred("Semaphore Lock enabled, deferring Channel Discovery.", delay);
        }
        #nullable disable

        #endregion

        #region Observability
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Dimensions GetCoreKeyDimensions()
        {
            var dimensions = base.GetCoreKeyDimensions();
            dimensions[StandardDimensions.COMPANY] = SystemContext.GetCompanyName();
            dimensions[StandardDimensions.SYSTEM] = SystemContext.GetConnectorName();
            dimensions[StandardDimensions.SERVICE] = ServiceName;
            dimensions[StandardDimensions.CONNECTOR_ID] = WorkRequest.ConnectorConfigId;
            dimensions[StandardDimensions.TENANT_ID] = WorkRequest.TenantId;
            dimensions[StandardDimensions.TENANT_DOMAIN_NAME] = WorkRequest.TenantDomainName;
            return dimensions;
        }

        /// <summary>
        /// Add additional core outcome measures
        /// </summary>
        /// <returns></returns>
        protected override Measures GetCoreResultMeasures()
        {
            var dimensions = base.GetCoreResultMeasures();
            dimensions[StandardMeasures.OUTCOME_SECONDS] = ResultDuration.TotalSeconds;
            return dimensions;
        }
        #endregion

        #region Disposable
        private bool _hasDisposed = false;

        /// <summary>
        /// Public access to check if the object has been disposed
        /// </summary>
        public bool HasDisposed
        {
            get
            {
                if (!_hasDisposed) return _hasDisposed;
                throw new ObjectDisposedException(WorkType);
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        protected virtual void InnerDispose()
        {
            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (!_hasDisposed)
            {
                InnerDispose();
                _hasDisposed = true;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
