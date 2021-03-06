﻿using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.TaskRunner
{
    /// <summary>
    /// Provides base functionality for managing a set of long running 
    /// tasks. 
    /// </summary>
    public abstract class TaskRunnerBase
    {
        public ILog Log { get; set; }

        private IDateTimeProvider _dateTimeProvider;

        public IDateTimeProvider DateTimeProvider
        {
            get
            {
                if(_dateTimeProvider == null)
                {
                    _dateTimeProvider = Providers.DateTimeProvider.Instance;
                }

                return _dateTimeProvider;
            }

            set
            {
                _dateTimeProvider = value;
            }
        }
        public TaskRunnerBaseSettings Settings { get; set; }
        /// <summary>
        /// Determines if an exception should be permitted to kill a long running task
        /// </summary>
        public Func<Exception, CancellationToken, bool> IsKillerException { get; set; } = (x, y) => false;

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationTokenSource _linkedCancellationTokenSource;
        private readonly object _cancellationTokenSourceSyncRoot = new object();
        private List<KeyValuePair<TaskRunnerInformationBase, Task>> _runningTasks = new List<KeyValuePair<TaskRunnerInformationBase, Task>>();
        private Guid _correlationGroup;
        protected abstract Task<IEnumerable<TaskRunnerInformationBase>> GetTaskRunnerInformation(CancellationToken ct);

        public IList<TaskRunnerInformationBase> GetAllRunningTasks()
        {
            return _runningTasks.Select(x => x.Key).ToList();
        }

        /// <summary>
        /// Runs all tasks until cancelled. 
        /// This method runs indefinitely until the passed cancellation token 
        /// is cancelled. 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _correlationGroup = Guid.NewGuid();
                    Log?.LogVerbose(GetType(), nameof(Run), $"CorrelationGroup [{_correlationGroup}] - Starting Tasks.");

                    lock (_cancellationTokenSourceSyncRoot)
                    {
                        _cancellationTokenSource = new CancellationTokenSource();
                    }

                    // Create a single cancellation token that links our internal cancellationtoken and the cancellationtoken that is passed in 
                    // to this method. We want to cancel the tasks if either of these cancellation tokens are cancelled.
                    using (_linkedCancellationTokenSource =
                        CancellationTokenSource.CreateLinkedTokenSource(cancellationToken,
                            _cancellationTokenSource.Token))
                    {
                        var linkedToken = _linkedCancellationTokenSource.Token;

                        _runningTasks = (await GetTaskRunnerInformation(linkedToken).ConfigureAwait(false)).Select(x =>
                            new KeyValuePair<TaskRunnerInformationBase, Task>(x, RunTask(x, linkedToken))
                        ).ToList();

                        // If we await the tasks (e.g. Task.WaitAll(), any exceptions from the Tasks will be thrown.
                        // Instead, evaluate them for partition killer exceptions in ClearTasks, which will
                        // be called when any task throws out.
                        await Task.Delay(Timeout.Infinite, linkedToken).ConfigureAwait(false);
                    }
                }
                catch (OperationCanceledException)
                {
                    // A task was canceled. This can happen either if
                    //  - Something changed and _cancellationTokenSource was canceled as a result
                    //  - One of the tasks faulted and we cancelled the linked cancellation token
                    //    in the continuation above

                    ClearTasks(cancellationToken);
                }
                catch (AggregateException ex)
                {
                    if (ex.InnerExceptions.All(iex => iex is OperationCanceledException))
                    {
                        ClearTasks(cancellationToken);
                    }
                    else
                    {
                        // Something else went wrong.
                        throw;
                    }
                }
#if DEBUG
                // catch here so I debug the other exceptions that is being thrown and not handled
                // by sticking a break point in the catch
                catch (Exception)
                {
                    throw;
                }
#endif
                finally
                {
                    // Wait before going back to the top of the loop in all cases,
                    // in order to avoid thrashing the CPU.
                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Cancels all running tasks. 
        /// This causes the running RunAsync method to call 
        /// CreateAndRunTasks again.
        /// </summary>
        public void Refresh()
        {
            Log?.LogVerbose(GetType(), nameof(Refresh), $"CorrelationGroup [{_correlationGroup}] - Restarting all Tasks.");

            lock (_cancellationTokenSourceSyncRoot)
            {
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }

        private Task RunTask(TaskRunnerInformationBase info, CancellationToken ct)
        {
            var task = Task.Run(async () =>
            {
                await GetTask(info, ct).ConfigureAwait(false);
            });

            task.ContinueWith(t =>
            {
                // Tasks should only fault on Partition Killer exceptions
                // when this happens, cancel all tasks and throw any exceptions
                Cancel();
            },
                ct,
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted,
                TaskScheduler.Current
            );

            return task;
        }

        private async Task GetTask(TaskRunnerInformationBase info, CancellationToken ct)
        {
            Log?.LogVerbose(GetType(), nameof(RunTask), $"{info.LogPrefix} (CorrelationGroup [{_correlationGroup}]) - Starting task");

            DateTime taskStartTime = DateTimeProvider.UtcNow;
            long exceptionCount = 0;

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    taskStartTime = DateTimeProvider.UtcNow;
                    await info.Function().ConfigureAwait(false);

                    Log?.LogVerbose(GetType(), nameof(RunTask), $"{info.LogPrefix} (CorrelationGroup [{_correlationGroup}]) - Task ran to completion");
                    return;
                }
                catch (Exception ex) when (!IsKillerException(ex, ct))
                {
                    var waitTime = Settings.GetWaitTime(exceptionCount);

                    //If below the MaxUnhandledExceptions count, we simply log a warning.
                    //Otherwise, if over the max, we attempt to handle via logging an error and calling Exceptionhandler.
                    if (!ShouldRespondToException(taskStartTime, GetRepeatedTaskFailureTime(ex, info, ct), ref exceptionCount, ct))
                    {
                        Log?.LogWarning(GetType(), nameof(RunTask), $"{info.LogPrefix} (CorrelationGroup [{_correlationGroup}]) - Restarting task in [{waitTime.TotalSeconds}s] after [{exceptionCount}] exceptions due to unhandled exception: [{ex}]");
                    }
                    else
                    {
                        //Log an error, adding exception data to aid in debugging.
                        Log?.LogError(GetType(), nameof(RunTask), ex, $"{info.LogPrefix} (CorrelationGroup [{_correlationGroup}]) - Restarting task in [{waitTime.TotalSeconds}s] after [{exceptionCount}] exceptions due to unhandled exception.");

                        //If an exceptionHandler was provided for the task, call it.
                        if (info.ExceptionHandler != null)
                        {
                            try
                            {
                                await info.ExceptionHandler(ex, info).ConfigureAwait(false);
                            }
                            catch (Exception iex) when (!IsKillerException(ex, ct))
                            {
                                Log?.LogWarning(GetType(), nameof(RunTask), $"{info.LogPrefix} (CorrelationGroup [{_correlationGroup}]) - Unhandled exception in final exception handler: [{iex}]");
                            }
                        }
                    }

                    //Increment the exception count regardless of scenario, and wait for an appropriate period
                    exceptionCount++;
                    await Task.Delay(waitTime, ct).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Gets the minimum amount of time that a Task should be able to run before encoutering an unhandled exception
        /// If an exception is encountered by a Task within this time period after it is restarted, we will start to back
        /// off and spend more time in betwen Task restarts.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="info"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private TimeSpan GetRepeatedTaskFailureTime(Exception ex, TaskRunnerInformationBase info, CancellationToken ct)
        {
            if (info.RepeatedTaskFailureTimeHandler != null)
            {
                try
                {
                    return info.RepeatedTaskFailureTimeHandler(ex, Settings.DefaultRepeatedTaskFailureTime);
                }
                catch (Exception iex) when (!IsKillerException(ex, ct))
                {
                    // Don't want this to throw out, it will go immediately to the final exception. Return the default TimeSpan.
                    Log?.LogWarning(GetType(), nameof(GetRepeatedTaskFailureTime), $"{info.LogPrefix} (CorrelationGroup [{_correlationGroup}]) - Unhandled exception: [{iex}]");
                }
            }
            return Settings.DefaultRepeatedTaskFailureTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskStartTime">The time when the task was last started</param>
        /// <param name="repeatedTaskFailureTime">The amount of time that a Task expects to run for before encountering an unhandled exception. If the task
        /// is able to run this long without an exception, then it's probably not repeatedly encountering the same exception</param>
        /// <param name="exceptionCount">Count of exceptions encountered by this task PRIOR to this exception</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private bool ShouldRespondToException(DateTime taskStartTime, TimeSpan repeatedTaskFailureTime, ref long exceptionCount, CancellationToken ct)
        {
            // If the number of exceptions is not above the max, continue without logging an error
            if (exceptionCount < Settings.MaxUnhandledExceptions)
            {
                return false;
            }
            // If the task has faulted repeatedly and has failed WITHIN the TaskFailureAllowanceTime after the wait period following an exception,
            // and the number of exceptions is above the max, we should log an error
            else if ((taskStartTime + repeatedTaskFailureTime) > DateTimeProvider.UtcNow)
            {
                return true;
            }
            // If the task has faulted repeatedly and has failed OUTSIDE the TaskFailureAllowanceTime after the wait period following an exception,
            // RESET the exception count
            else
            {
                exceptionCount = 0;
                return false;
            }
        }

        private void Cancel()
        {
            _linkedCancellationTokenSource.Cancel();
        }

        private void ClearTasks(CancellationToken cancellationToken)
        {
            var faultedTasks = _runningTasks
                .Where(kvp => kvp.Value.IsFaulted && kvp.Value.Exception != null)
                .ToList();

            var partitionKillerTasks = faultedTasks
                .Where(kvp => IsKillerException(kvp.Value.Exception, cancellationToken))
                .ToList();

            _runningTasks.Clear();

            if (partitionKillerTasks.Any())
            {
                if (partitionKillerTasks.Count == 1)
                {
                    throw partitionKillerTasks.Single().Value.Exception;
                }
                else
                {
                    throw new AggregateException(partitionKillerTasks.Select(kvp => kvp.Value.Exception).ToArray());
                }
            }

            // This shouldn't happen - Tasks should shut themselves down gracefully after they hit their maximum retries
            // In the event that we are seeing this logged, investigate root cause.
            if (faultedTasks.Any())
            {
                foreach (var faultedTask in faultedTasks)
                {
                    Log?.LogWarning(GetType(), nameof(ClearTasks), $"{faultedTask.Key.LogPrefix} (CorrelationGroup [{_correlationGroup}]) - Restarting all Tasks in partition due to exception: {faultedTask.Value.Exception}");
                }
            }
        }
    }
}
