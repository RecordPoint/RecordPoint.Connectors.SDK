using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// The managed queueable work base.
    /// </summary>
    /// <typeparam name="TConfiguration"/>
    /// <typeparam name="TState"/>
    public abstract class ManagedQueueableWorkBase<TConfiguration, TState> : QueueableWorkBase<TState>
        where TConfiguration : class
        where TState : class
    {
        /// <summary>
        /// The SEMAPHORE GLOBAL.
        /// </summary>
        protected const string SEMAPHORE_GLOBAL = "SEMAPHORE_GLOBAL";
        /// <summary>
        /// The SEMAPHORE CONNECTOR CONFIGURATION.
        /// </summary>
        protected const string SEMAPHORE_CONNECTOR_CONFIGURATION = "SEMAPHORE_CONNECTOR_CONFIGURATION_";

        #region Dependencies
        /// <summary>
        /// The managed work factory.
        /// </summary>
        private readonly IManagedWorkFactory _managedWorkFactory;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="managedWorkFactory">The managed work factory.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="observabilityScope">The scope manager.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        protected ManagedQueueableWorkBase(
            IServiceProvider serviceProvider,
            IManagedWorkFactory managedWorkFactory,
            ISystemContext systemContext,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
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
                TelemetryTracker.TrackException(ex);
                await FaultedAsync(ex.Message, ex, cancellationToken);
            }
            finally
            {
                FinishDateTime = DateTimeProvider.UtcNow;
                TrackFinish();
            }
        }

        /// <summary>
        /// Determines if a connector is enabled and if not, back off until it is re-enabled or exceeds the threshold
        /// </summary>
        /// <param name="connectorConfiguration">The Connector Configuration</param>
        /// <param name="options">Configured options for the work operation</param>
        /// <param name="maxDisabledConnectorAge">The maximum time in seconds a Connector can be disabled before being abandoned</param>
        /// <param name="state">The state object of the work operation</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        protected async Task<bool> CheckConnectorEnabledStatusAsync(ConnectorConfigModel connectorConfiguration, ContentManagerOperationOptionsBase options, IContentDiscoveryState state, int maxDisabledConnectorAge, CancellationToken cancellationToken)
        {
            if (connectorConfiguration == null)
            {
                // If connector not found assume its been deleted
                await AbandonedAsync("Connector not found", cancellationToken);
                return false;
            }

            if (connectorConfiguration.IsEnabled())
                return true;

            //Check the time the connector was disabled. If the time exceeds the configured threshold, we should abandon the work.
            //We may not have a disabled date in which case, we should just abandon the work immediately.
            //If the Max Disabled Connector Age is less than zero, we will retry indefinitely until the connector is re-enabled.
            if (connectorConfiguration.IsDisabledConnectorExpired(maxDisabledConnectorAge))
            {
                //Connector has been disabled for too long, abandon the work
                await AbandonedAsync("Connector disabled", cancellationToken);
            }
            else
            {
                //The Connector Configuration is disabled and is within the configured threshold, so backoff until it is re-enabled or exceeds the threshold
                var backOffSeconds = CalculateBackOffSeconds(
                    options,
                    false,
                    state.LastBackOffDelaySeconds);

                state.LastBackOffDelaySeconds = backOffSeconds;

                await ContinueAsync("Connector disabled", State, DateTimeProvider.UtcNow.AddSeconds(backOffSeconds), cancellationToken);
            }

            return false;
        }
        #endregion

        #region Run Result
        /// <summary>
        /// Record that the long-running job has completed and no more attempts should be made
        /// to run it
        /// </summary>
        /// <remarks>Reason why the work item is complete</remarks>
        /// <param name="reason">Reason why the work item is complete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        protected new async virtual Task CompleteAsync(string reason, CancellationToken cancellationToken)
        {
            await base.CompleteAsync(reason, cancellationToken);
            SetOutcome(await WorkManager.CompleteAsync(reason, cancellationToken));
        }

        /// <summary>
        /// Record that the long-running job should continue working
        /// </summary>
        /// <param name="reason">Reason why the job is continuing</param>
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
        /// <param name="exception">Optional cause of the failure</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Outcome to pass onto the work queue</returns>
        public async Task FailAsync(string reason, Exception exception, CancellationToken cancellationToken)
        {
            reason ??= exception?.Message ?? throw new ArgumentNullException(nameof(reason));
            EnsureIncomplete();
            SetOutcome(await WorkManager.FailedAsync(reason, cancellationToken));
            Exception = exception;
            TelemetryTracker.TrackException(exception);
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
            SetOutcome(await WorkManager.FaultyAsync(reason, exception, cancellationToken, WorkRequest?.FaultedCount ?? 0));
            Exception = exception;
            TelemetryTracker.TrackException(exception);
        }

        /// <summary>
        /// Record that the long-running job has completed and no more attempts should be made
        /// to run it
        /// </summary>
        /// <remarks>Reason why the work item is complete</remarks>
        /// <param name="reason">Reason why the work item is complete</param>
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

        /// <summary>
        /// Calculate back off seconds.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="hasResult">If true, has result.</param>
        /// <param name="lastDelaySeconds">The last delay seconds.</param>
        /// <returns>An int</returns>
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

        #region Disposal
        /// <summary>
        /// Dispose of resources
        /// </summary>
        protected override void InnerDispose()
        {
            Configuration = null;
            State = null;
            WorkManager?.Dispose();
            WorkManager = null;
        }
        #endregion
    }
}
