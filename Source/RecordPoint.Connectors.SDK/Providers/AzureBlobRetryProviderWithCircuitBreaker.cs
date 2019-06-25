﻿using Microsoft.WindowsAzure.Storage;
using Polly;
using Polly.CircuitBreaker;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Interfaces;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Providers
{
    /// <summary>
    /// Retry provider with circuit breaker for Azure blob storage 
    /// </summary>
    public class AzureBlobRetryProviderWithCircuitBreaker : AzureBlobRetryProvider
    {
        private TimeSpan WaitFor { get; set; }

        // The CircuitBreakerPolicy needs to be a singleton - Either the implementing class needs to be a singleton, or the policy needs to be a
        // singleton. Static would be good for ensuring the policy is a singleton even if we mess up our IoC, but this would:
        // - Prevent RetryProviderWithCircuitBreaker being abstracted into a base class for multiple resources, as they would all share the same underlying circuit
        // - Complicates testing of the policy, as it does not reset in between calls (Can get around this by nulling it with reflection if required)
        private CircuitBreakerPolicy _circuitBreaker;
        private Policy _fallback;
        private object _waitUntilLock = new object();
        private DateTime _waitUntil = DateTime.MinValue;
        private CircuitBreakerOptions _options;
        private bool _useCircuit;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="useCircuit"></param>
        public AzureBlobRetryProviderWithCircuitBreaker(CircuitBreakerOptions options, bool useCircuit)
        {
            _options = options;
            _fallback = GetFallbackPolicy();
            _circuitBreaker = GetCircuitBreakerPolicy();
            _useCircuit = useCircuit;
        }

        /// <summary>
        /// Check if the circuit is closed and set a waiting period if the circuit is open
        /// </summary>
        /// <param name="waitFor"></param>
        public bool IsCircuitClosed(out TimeSpan waitFor)
        {
            if (_circuitBreaker.CircuitState == CircuitState.Closed || _circuitBreaker.CircuitState == CircuitState.HalfOpen)
            {
                waitFor = TimeSpan.Zero;
                return true;
            }

            // Lock the waitUntil value while it is retrieved.
            DateTime internalWaitUntil;
            lock (_waitUntilLock)
            {
                internalWaitUntil = _waitUntil;
            };

            // We can use a DateTime (rather than a Provider) as this class only uses it internally. 
            // I.e. it just uses it relatively to keep track of how much time has passed.
            var waitTime = internalWaitUntil - DateTime.UtcNow;
            // If circuit state changes or issue arrises with date time, this could be in the past.
            // Thus, if the timespan is negative, we return zero instead.
            waitFor = waitTime > TimeSpan.Zero ? waitTime : TimeSpan.Zero;
            return false;
        }

        // Returns a Task to match the parameter requirements of FallbackAsync
        private Exception ThrowTooManyRequests()
        {
            var waitUntil = DateTime.UtcNow.AddSeconds(_options.DurationOfBreakS);

            // Lock the waitUntil value while it is set.
            lock (_waitUntilLock)
            {
                _waitUntil = waitUntil;
            }

            return new TooManyRequestsException(string.Empty, _waitUntil);
        }

        private Policy GetFallbackPolicy()
        {
            return Policy.Handle<BrokenCircuitException>().FallbackAsync((ct) =>
            {
                throw ThrowTooManyRequests();
            });
        }

        private CircuitBreakerPolicy GetCircuitBreakerPolicy()
        {
            return Policy.Handle<StorageException>(ex => IsCircuitBreakerAzureBlobException(ex))
                    .AdvancedCircuitBreakerAsync(
                        failureThreshold: _options.FailureThreshold,
                        samplingDuration: TimeSpan.FromSeconds(_options.SamplingDurationS),
                        minimumThroughput: _options.MinimumAttempts,
                        durationOfBreak: TimeSpan.FromSeconds(_options.DurationOfBreakS),
                        onBreak: (ex, ts) =>
                        {
                            // Performed when the Circuit breaks
                            Log?.LogWarning(
                                typeof(AzureBlobRetryProviderWithCircuitBreaker),
                                nameof(GetCircuitBreakerPolicy),
                                $"Circuit open for [{ts}]. Final exception [{ex}]."
                            );
                        },
                        onReset: () =>
                        {
                            // Performed when the Circuit returns to normal operation
                            Log?.LogMessage(
                                typeof(AzureBlobRetryProviderWithCircuitBreaker),
                                nameof(GetCircuitBreakerPolicy),
                                "Circuit reset."
                            );
                        }
                    );
        }

        /// <summary>
        /// Retry Policy
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        protected override Policy GetRetryPolicy(Type type, string methodName)
        {
            // Wrap the retry policy with a fallback if using the circuit breaker
            if (_useCircuit)
            {
                return Policy.WrapAsync(_fallback, base.GetRetryPolicy(type, methodName), _circuitBreaker);
            }

            return base.GetRetryPolicy(type, methodName);
        }

        private bool IsCircuitBreakerAzureBlobException(StorageException ex)
        {
            if (ex.RequestInformation.HttpStatusCode == 503)
            {
                /* Returned from blob in the following variations:
                 *  "The server is busy."
                 *  "The server is currently unable to receive requests. Please retry your request"
                 *  "Ingress is over the account limit."
                 *  "Egress is over the account limit."
                 *  "Operations per second is over the account limit."
                */
                return true;
            }
            else if (ex.RequestInformation.HttpStatusCode == 500)
            {
                /* Returned from blob in the following variations:
                 *  "The operation could not be completed within the permitted time."
                */
                return true;
            }
            return false;
        }
    }
}
