﻿using Moq;
using Polly;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Interfaces;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.TaskRunner;
using RecordPoint.Connectors.SDK.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static RecordPoint.Connectors.SDK.TaskRunner.TaskRunnerInformationBase;

namespace RecordPoint.Connectors.SDK.Test.TaskRunner
{
    public class TaskRunnerBaseTest
    {
        [Fact]
        public async Task TaskRunnerBase_Awaits_AfterAllComplete()
        {
            var counter = 0;

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                {
                    GetTaskRunnerInformation<object>(() => { counter++; return Task.FromResult(0); }, null, null, null, ct),
                };
            }));

            await RunSUT(sut, () =>
            {
                Assert.True(counter == 1, $"Expected thread to only run once, ran {counter} times instead");
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task TaskRunnerBase_OnlyRestartsFaultedThread_AfterFault()
        {
            var counter = 0;
            var exceptionCounter = 0;

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                {
                    GetTaskRunnerInformation<object>(() => { counter++; return Task.FromResult(0); }, null, null, null, ct),
                    GetTaskRunnerInformation<object>(async () => { exceptionCounter ++; await Task.Delay(TimeSpan.FromSeconds(1)); throw(new Exception()); }, null, null, null, ct),
                };
            }));

            await RunSUT(sut, () =>
            {
                Assert.True(counter == 1, $"Expected thread to only run once, ran {counter} times instead");
                Assert.True(exceptionCounter == sut.Settings.MaxUnhandledExceptions + 1, $"Expected exception handler to run {sut.Settings.MaxUnhandledExceptions + 1} times, was run {exceptionCounter} times instead");
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task TaskRunnerBase_DoesNotShutDownFaultedThread_AfterRepeatedFaultsWithOverriddenRepeatedTaskFailureTime()
        {
            var counter = 0;
            var exceptionHandlerCounter = 0;

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                    {
                        GetTaskRunnerInformation<object>(
                            () => { counter++; throw (new Exception()); },
                            (ex, info) => { exceptionHandlerCounter++; return Task.FromResult(0); },
                            (ex, ts) => { return TimeSpan.FromDays(1); },
                            null,
                            ct
                        )
                    };
            })
            );

            sut.Settings.MaxUnhandledExceptions = 1;
            // This setting would allow the task be retried forever if respected
            sut.Settings.DefaultRepeatedTaskFailureTime = TimeSpan.FromDays(-1);

            await RunSUT(sut, () =>
            {
                Assert.True(counter > sut.Settings.MaxUnhandledExceptions + 1, $"Expected thread to be tried once and retried many times, was run {counter} times instead");
                //We expect the exception handler NOT to run on counter runs up to MaxUnhandledExceptions.
                //For example, with MUH = 2, we expect an initial run (counter = 0), and 1 more (counter = 1) which do not call the exception handler
                var expectedExceptionHandlerCount = counter - (sut.Settings.MaxUnhandledExceptions);
                Assert.True(exceptionHandlerCounter == expectedExceptionHandlerCount, $"Expected exception handler to be run {expectedExceptionHandlerCount} times, was ran {exceptionHandlerCounter} times instead");
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task TaskRunnerBase_DoesNotShutDownFaultedThread_AfterRepeatedSeparateFaultsWithOverriddenRepeatedTaskFailureTime()
        {
            var counter = 0;
            var exceptionHandlerCounter = 0;

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                    {
                        GetTaskRunnerInformation<object>(
                            () => { counter++; throw (new Exception()); },
                            (ex, info) => { exceptionHandlerCounter++; return Task.FromResult(0); },
                            (ex, ts) => { return TimeSpan.FromDays(-1); },
                            null,
                            ct
                        ),
                    };
            })
            );

            await RunSUT(sut, () =>
            {
                Assert.True(counter > sut.Settings.MaxUnhandledExceptions + 1, $"Expected thread to be tried once and retried many times, was run {counter} times instead");
                Assert.True(exceptionHandlerCounter == 0, $"Expected exception handler to not be run, was ran {exceptionHandlerCounter} times instead");
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task TaskRunnerBase_DoesNotShutDownFaultedThread_AfterRepeatedFaults()
        {
            var counter = 0;
            var exceptionHandlerCounter = 0;

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                    {
                        GetTaskRunnerInformation<object>(
                            () => { counter++; throw (new Exception()); },
                            (ex, info) => { exceptionHandlerCounter++; return Task.FromResult(0); },
                            null,
                            null,
                            ct
                        )
                    };
            })
            );

            await RunSUT(sut, () =>
            {
                Assert.True(counter > sut.Settings.MaxUnhandledExceptions + 1, $"Expected thread to be tried once and retried many times, was run {counter} times instead");
                //We expect the exception handler NOT to run on counter runs up to MaxUnhandledExceptions.
                //For example, with MUH = 2, we expect an initial run (counter = 0), and 1 more (counter = 1) which do not call the exception handler
                var expectedExceptionHandlerCount = counter - (sut.Settings.MaxUnhandledExceptions);
                Assert.True(exceptionHandlerCounter == expectedExceptionHandlerCount, $"Expected exception handler to be run {expectedExceptionHandlerCount} times, was ran {exceptionHandlerCounter} times instead");
            }).ConfigureAwait(false);
        }

        [Fact(Skip = "Fails intermittently")]
        public async Task TaskRunnerBase_DoesNotThrow_ExceptionsFromExceptionHandler()
        {
            var counter = 0;
            var exceptionHandlerCounter = 0;

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                    {
                        GetTaskRunnerInformation<object>(
                            () => { counter++; throw new Exception(); },
                            (ex, info) => { exceptionHandlerCounter++; throw new Exception(); },
                            null,
                            null,
                            ct
                        )
                    };
            })
            );

            await RunSUT(sut, () =>
            {
                VerifySubmittedLogWarning(sut.MockLog, "ClearTasks", "", Times.Never());
                VerifySubmittedLogError(sut.MockLog, "RunTask", Times.Exactly(counter - sut.Settings.MaxUnhandledExceptions + 1));
                VerifySubmittedLogWarning(sut.MockLog, "RunTask", "Unhandled exception in final exception handler", Times.Exactly(counter - sut.Settings.MaxUnhandledExceptions));

                Assert.True(exceptionHandlerCounter == counter - sut.Settings.MaxUnhandledExceptions, $"Expected exception handler to run {counter - sut.Settings.MaxUnhandledExceptions} times, ran {exceptionHandlerCounter} times instead");
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task TaskRunnerBase_DoesNotShutDownFaultedThread_AfterRepeatedSeparateFaults()
        {
            var counter = 0;
            var exceptionHandlerCounter = 0;

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(x => x.UtcNow).Returns(() => { return DateTime.UtcNow.AddDays(counter); });

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                    {
                        GetTaskRunnerInformation<object>(
                            () => { counter++; throw (new Exception()); },
                            (ex, info) => { exceptionHandlerCounter++; return Task.FromResult(0); },
                            null,
                            null,
                            ct
                        ),
                    };
            }),
                dateTimeProvider.Object
            );

            await RunSUT(sut, () =>
            {
                Assert.True(counter > sut.Settings.MaxUnhandledExceptions + 1, $"Expected thread to be tried once and retried many times, was run {counter} times instead");
                Assert.True(exceptionHandlerCounter == 0, $"Expected exception handler to not be run, was ran {exceptionHandlerCounter} times instead");
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task TaskRunnerBase_DoesNotShutDownFaultedThread_AfterRepeatedSeparateFaultsWithOccasionalSequentialFaults()
        {
            var counter = 0;
            var exceptionHandlerCounter = 0;

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(x => x.UtcNow).Returns(() => {
                // Half the time, return a value that makes it look like it's been a long time since the thread faulted
                //Note that the "+1" is required here to prevent the spoofed date occuring on the same counter as the
                //first exceptionCount that is not under the MaxUnhandledExceptions, as this would erroneously cause
                //the test to fail (as only the UtcNow used for the taskStartTime would be increased by the mock,
                //not the one within ShouldRespondToException).
                if ((counter + 1) % 2 == 0)
                {
                    return DateTime.UtcNow.AddDays(counter);
                }

                // The rest of the time, return the current time, which will look like it's been no time since the task faulted
                return DateTime.UtcNow;
            });

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                    {
                        GetTaskRunnerInformation<object>(
                            () => { counter++; throw (new Exception()); },
                            (ex, info) => { exceptionHandlerCounter++; return Task.FromResult(0); },
                            null,
                            null,
                            ct
                        ),
                    };
            }),
                dateTimeProvider.Object
            );

            await RunSUT(sut, () =>
            {
                Assert.True(counter > sut.Settings.MaxUnhandledExceptions + 1, $"Expected thread to be tried once and retried many times, was run {counter} times instead");
                Assert.True(exceptionHandlerCounter == 0, $"Expected exception handler to not be run, was ran {exceptionHandlerCounter} times instead");
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task TaskRunnerBase_Cancels_AfterCancellationRequested()
        {
            var counter = 0;

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                {
                    GetTaskRunnerInformation<object>(async () => { counter++; await Task.Delay(Timeout.Infinite); }, null, null, null, ct)
                };
            }));

            var source = new CancellationTokenSource();
            var task = Task.Run(() => sut.OuterRun(source.Token));

            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            source.Cancel();

            Assert.True(counter == 1, $"Expected thread to only run once, ran {counter} times instead");
            Assert.True(sut.InitialCancellationToken.IsCancellationRequested == true, $"Expected cancellation token passed into SUT to be cancelled");
            Assert.True(sut.LinkedCancellationToken.IsCancellationRequested == true, $"Expected linked cancellation token created in SUT to be cancelled");

            await task.AwaitCompleteOrCancel();
        }

        [Fact]
        public async Task TaskRunnerBase_DoesNotThrow_FailureInRepeatedTaskFailureTimeHandler()
        {
            var counter = 0;
            var exceptionHandlerCounter = 0;

            var sut = new TaskRunnerActual((ct =>
            {
                return new List<TaskRunnerInformationBase>()
                    {
                        GetTaskRunnerInformation<object>(
                            () => { counter++; throw new Exception(); },
                            (ex, info) => { exceptionHandlerCounter++; return Task.FromResult(0); },
                            (ex, ts) => { throw new Exception(); },
                            null,
                            ct
                        )
                    };
            })
            );

            await RunSUT(sut, () =>
            {
                Assert.True(counter > sut.Settings.MaxUnhandledExceptions + 1, $"Expected thread to be tried once and retried many times, was run {counter} times instead");
                //We expect the exception handler NOT to run on counter runs up to MaxUnhandledExceptions.
                //For example, with MUH = 2, we expect an initial run (counter = 0), and 1 more (counter = 1) which do not call the exception handler
                var expectedExceptionHandlerCount = counter - (sut.Settings.MaxUnhandledExceptions);
                Assert.True(exceptionHandlerCounter == expectedExceptionHandlerCount, $"Expected exception handler to be run {expectedExceptionHandlerCount} times, was ran {exceptionHandlerCounter} times instead");
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task TaskRunnerBase_Throws_PartitionKillerException()
        {
            var knownKillerExceptions = new List<Exception>()
            {
                new OutOfMemoryException()
            };

            foreach (var exception in knownKillerExceptions)
            {
                var sut = new TaskRunnerActual((ct =>
                {
                    return new List<TaskRunnerInformationBase>()
                    {
                        GetTaskRunnerInformation<object>(() => { throw(exception); }, null, null, null, ct)
                    };
                }));

                var task = Task.Run(() => sut.OuterRun(CancellationToken.None));

                var e = await Assert.ThrowsAsync<AggregateException>(async () =>
                {
                    await task.ConfigureAwait(false);
                });

                Assert.True(e.InnerException.GetType() == exception.GetType(),
                    $"Expected exception to be {exception.GetType()}, was {task.Exception.InnerException.GetType()} instead");
            }
        }

        private async Task RunSUT(TaskRunnerActual sut, Action code)
        {
            var source = new CancellationTokenSource();
            var task = Task.Run(() => sut.Run(source.Token));

            await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

            code();

            source.Cancel();
            await task.AwaitCompleteOrCancel();
        }

        private TaskRunnerInformationBase GetTaskRunnerInformation<T>(Func<Task> code, ExceptionHandlerFunction exceptionHandler, RepeatedTaskFailureTimeFunction taskFailureAllowanceTimeHandler, T taskObject, CancellationToken ct)
            where T : new()
        {
            return new TaskRunnerInformation<T>(
                Guid.NewGuid(),
                Guid.NewGuid(),
                code,
                exceptionHandler,
                taskFailureAllowanceTimeHandler,
                taskObject,
                ct);
        }

        private class TaskRunnerActual : TaskRunnerBase
        {
            public CancellationToken InitialCancellationToken { get; set; }
            public CancellationToken LinkedCancellationToken { get; set; }

            public Mock<ILog> MockLog { get; set; }

            private readonly TaskCreator _taskCreator;

            public async Task OuterRun(CancellationToken ct)
            {
                InitialCancellationToken = ct;

                await Run(ct).ConfigureAwait(false);
            }

            public TaskRunnerActual(TaskCreator taskCreator,
                IDateTimeProvider dateTimeProvider = null)
            {
                if (dateTimeProvider == null)
                {
                    dateTimeProvider = RecordPoint.Connectors.SDK.Providers.DateTimeProvider.Instance;
                }

                _taskCreator = taskCreator;
                DateTimeProvider = dateTimeProvider;
                MockLog = new Mock<ILog>();
                Log = MockLog.Object;
                Settings = new TaskRunnerBaseSettings()
                {
                    MaxUnhandledExceptions = 2,
                    DefaultRepeatedTaskFailureTime = TimeSpan.FromSeconds(2),
                    WaitTimePowBase = 1,
                    WaitTimeSecondsBase = 1
                };
                IsKillerException = IsOutOfMemoryException;
            }

            protected override Task<IEnumerable<TaskRunnerInformationBase>> GetTaskRunnerInformation(CancellationToken ct)
            {
                LinkedCancellationToken = ct;

                return Task.FromResult(_taskCreator(ct));
            }
            /// <summary>
            /// Determines if an exception is OutOfMemoryException and should be permitted to kill a long running task.
            /// </summary>
            /// <param name="ex"></param>
            /// <param name="cancellationToken">
            /// <returns></returns>
            public bool IsOutOfMemoryException(Exception ex, CancellationToken cancellationToken)
            {
                var knownPartitionKillerExceptions = new List<Type>()
            {
                typeof(OutOfMemoryException),
            };

                // There are certain cases where OperationCanceledException is thrown and we do *not* want
                // to kill the task. For example, HttpClient will throw 
                // TaskCanceledException (which inherits from OperationCanceledException)
                // if the HTTP call times out. In this unfortunate case, the exception should *not*
                // kill the task.
                // To handle these cases, only stop the tasks if the cancellationToken for the service
                // is canceled.
                if (cancellationToken.IsCancellationRequested)
                {
                    knownPartitionKillerExceptions.Add(typeof(OperationCanceledException));
                }

                return IsAssignableFrom(ex, knownPartitionKillerExceptions);
            }

            private bool IsAssignableFrom(Exception ex, List<Type> types)
            {
                bool isAssignable = false;

                if (ex is AggregateException)
                {
                    var ae = ex as AggregateException;
                    ae = ae.Flatten();
                    if (ae.InnerExceptions.Any(ie => types.Any(ket => ket.IsAssignableFrom(ie.GetType()))))
                    {
                        isAssignable = true;
                    }
                }
                else if (types.Any(ket => ket.IsAssignableFrom(ex.GetType())))
                {
                    isAssignable = true;
                }

                return isAssignable;
            }
        }

        private delegate IEnumerable<TaskRunnerInformationBase> TaskCreator(CancellationToken ct);

        private static void VerifySubmittedLogWarning(Mock<ILog> log, string methodName, string messagePattern, Times times, int timeoutSeconds = 10)
        {
            var policy = GetMockVerificationRetryPolicy(timeoutSeconds);
            policy.Execute(() =>
            {
                log.Verify(
                    x => x.LogWarning(
                        It.IsAny<Type>(),
                        methodName,
                        It.Is<string>(s => s.Contains(messagePattern)),
                        null),
                    times);
            });
        }

        private static void VerifySubmittedLogError(Mock<ILog> log, string methodName, Times times, int timeoutSeconds = 10)
        {
            var policy = GetMockVerificationRetryPolicy(timeoutSeconds);
            policy.Execute(() =>
            {
                log.Verify(
                    x => x.LogError(
                        It.IsAny<Type>(),
                        methodName,
                        It.IsAny<Exception>(),
                        It.IsAny<string>()),
                    times);
            });
        }

        public static Policy GetMockVerificationRetryPolicy(int timeoutSeconds)
        {
            return Policy.Handle<MockException>().WaitAndRetry(timeoutSeconds, x => TimeSpan.FromSeconds(1));
        }

    }
}
