using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Work
{
    public abstract class ManagedQueueableWorkBase<TConfiguration, TState> : QueueableWorkBase<TState>
    {
        protected const string SEMAPHORE_GLOBAL = "SEMAPHORE_GLOBAL";
        protected const string SEMAPHORE_CONNECTOR_CONFIGURATION = "SEMAPHORE_CONNECTOR_CONFIGURATION_";

        #region Dependencies
        private readonly IManagedWorkFactory _managedWorkFactory;

        protected ManagedQueueableWorkBase(
            IServiceProvider serviceProvider,
            IManagedWorkFactory managedWorkFactory,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            ILogger logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _managedWorkFactory = managedWorkFactory;

            Id = Guid.NewGuid().ToString();
        }
        #endregion

        #region Work Context
        /// <summary>
        /// Managed Work that we are running
        /// </summary>
        public IManagedWorkManager WorkManager { get; protected set; }

        /// <summary>
        /// Work configuration. Effectively the unchanging "input parameters" for the work.
        /// </summary>
        /// <remarks>
        /// The Work configuration cannot change over time.
        /// </remarks>
        public TConfiguration Configuration { get; protected set; }

        /// <summary>
        /// State of the Work when started
        /// </summary>
        /// <remarks>
        /// State is the context for work that has continued execution and is intended to record the ongoing progress between executions.
        /// It can be used to record information such as cursors, or tasks that have already been completed etc.
        /// </remarks>
        public TState State { get { return Parameter; } set { Parameter = value; } }

        /// <summary>
        /// Required override that deserializes the configuration from the work request
        /// </summary>
        /// <param name="configurationType">String that describes how the configuration is structured. Mainly required for supporting upgrade scenarios.</param>
        /// <param name="configurationText">Text that needs to be deserialized</param>
        /// <returns>Deserialized parameters</returns>
        protected abstract TConfiguration DeserializeConfiguration(string configurationType, string configurationText);

        /// <summary>
        /// Override that deserializes the state from the job
        /// </summary>
        /// <param name="stateType">String that describes how the state is structured. Mainly required for supporting upgrade scenarios</param>
        /// <param name="stateText">State text that needs to be serialized</param>
        /// <returns>Deserialized parameters</returns>
        protected abstract TState DeserializeState(string stateType, string stateText);

        /// <summary>
        /// Override that serialized the state
        /// </summary>
        /// <returns>StateType, State text tuple</returns>
        protected abstract (string, string) SerializeState(TState state);
        #endregion

        #region Run
        /// <summary>
        /// Execute the work request
        /// </summary>
        /// <param name="workRequest">Work request that defines the work to execute</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        public async override Task RunWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            if (workRequest.WorkType != WorkType)
                throw new InvalidOperationException($"Work request of type {workRequest.WorkType} does not match this work item");

            StartDateTime = DateTimeProvider.UtcNow;

            WorkRequest = workRequest;
            Id = workRequest.WorkId;
            SubmitDateTime = workRequest.SubmitDateTime;
            MustFinishDateTime = workRequest.MustFinishDateTime;

            var workStatus = ManagedWorkStatusModel.Deserialize(workRequest.Body);
            WorkManager = _managedWorkFactory.LoadWork(workStatus);
            Configuration = DeserializeConfiguration(WorkManager.WorkStatus.ConfigurationType, WorkManager.WorkStatus.Configuration);
            State = DeserializeState(WorkManager.WorkStatus.StateType, WorkManager.WorkStatus.State);

            var workOutcome = await WorkManager.CheckAsync(cancellationToken);
            if (workOutcome != null)
            {
                SetOutcome(workOutcome);
                return;
            }

            using var workScope = BeginObservabilityScope();
            try
            {
                TrackStart();
                await InnerRunAsync(cancellationToken);
                EnsureHasOutcome();
            }
            catch (Exception ex)
            {
                await FaultedAsync(ex.Message, ex, cancellationToken);
                Logger?.LogError(ex, nameof(IQueueableWork));
            }
            finally
            {
                FinishDateTime = DateTimeProvider.UtcNow;
                TrackFinish();
            }
        }
        #endregion

        #region Run Result
        /// <summary>
        /// Record that the long-running job has completed and no more attempts should be made
        /// to run it
        /// </summary>
        /// <remarks>Reason why the work item is complete</remarks>
        /// <param name="cancellationToken">Cancellation token</param>
        protected new async virtual Task CompleteAsync(string reason, CancellationToken cancellationToken)
        {
            await base.CompleteAsync(reason, cancellationToken);
            SetOutcome(await WorkManager.CompleteAsync(reason, cancellationToken));
        }

        /// <summary>
        /// Record that the long-running job should continue working
        /// </summary>
        /// <param name="state">State to use for the next run</param>
        /// <param name="waitTill">Time to wait for the next run</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected async virtual Task ContinueAsync(string reason, TState state, DateTimeOffset waitTill, CancellationToken cancellationToken)
        {
            EnsureIncomplete();
            (var stateType, var stateText) = SerializeState(state);
            SetOutcome(await WorkManager.ContinueAsync(stateType, stateText, waitTill, cancellationToken));
            ResultReason = reason;
            State = state;
        }

        /// <summary>
        /// Set that the job has permanently failed
        /// </summary>
        /// <param name="reason">Reason why the job has failed</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Outcome to pass onto the work queue</returns>
        public async Task FailAsync(string reason, Exception exception, CancellationToken cancellationToken)
        {
            reason ??= exception?.Message ?? throw new ArgumentNullException(nameof(reason));
            EnsureIncomplete();
            SetOutcome(await WorkManager.FailedAsync(reason, cancellationToken));
            Exception = exception;
        }

        /// <summary>
        /// Set that the job has had a fault and work has not advanced
        /// but the job has not critically failed and should continue
        /// </summary>
        /// <param name="reason">Reason why the job has faulted</param>
        /// <param name="exception">Optional cause of the fault</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        public async Task FaultedAsync(string reason, Exception exception, CancellationToken cancellationToken)
        {
            reason ??= exception?.Message ?? throw new ArgumentNullException(nameof(reason));
            EnsureIncomplete();
            SetOutcome(await WorkManager.FaultyAsync(reason, cancellationToken, WorkRequest?.FaultedCount ?? 0));
            Exception = exception;
        }

        /// <summary>
        /// Record that the long-running job has completed and no more attempts should be made
        /// to run it
        /// </summary>
        /// <remarks>Reason why the work item is complete</remarks>
        /// <param name="cancellationToken">Cancellation token</param>
        protected async virtual Task AbandonedAsync(string reason, CancellationToken cancellationToken)
        {
            base.Abandoned(reason);
            SetOutcome(await WorkManager.AbandonedAsync(reason, cancellationToken));
        }

        /// <summary>
        /// Set internal outcome from an externally provided outcome
        /// </summary>
        /// <param name="workOutcome">Provided outcome</param>
        protected void SetOutcome(WorkResult workOutcome)
        {
            ResultType = workOutcome.ResultType;
            ResultReason = workOutcome.Reason;
            WaitTill = workOutcome.WaitTill;
            Exception = workOutcome.Exception;
            HasResult = true;
        }

        protected int CalculateBackOffSeconds(ContentManagerOperationOptionsBase settings, bool hasResult, int lastDelaySeconds)
        {
            if (settings.ImmediateReExecution && hasResult) return 0;

            var delay = settings.DelaySeconds;
            if (settings.RandomDelay)
            {
                delay += Random.Shared.Next(settings.DelaySeconds);
            }
            
            if (!settings.ExponentialBackOff) return delay;

            var newDelay = lastDelaySeconds == 0 ? delay : lastDelaySeconds * 2;
            return newDelay < settings.MaxDelaySeconds ? newDelay : settings.MaxDelaySeconds;
        }
        #endregion

        #region Observability
        /// <summary>
        /// Get the core key dimensions for the work item
        /// </summary>
        /// <returns>Key dimensions that will be included in the work items observability scope</returns>
        protected override Dimensions GetCoreKeyDimensions() => new()
        {
            [StandardDimensions.COMPANY] = SystemContext.GetCompanyName(),
            [StandardDimensions.SERVICE] = ServiceName,
            [StandardDimensions.SYSTEM] = SystemContext.GetConnectorName(),
            [StandardDimensions.WORK] = WorkType,
            [StandardDimensions.WORK_ID] = Id
        };
        #endregion
    }
}
