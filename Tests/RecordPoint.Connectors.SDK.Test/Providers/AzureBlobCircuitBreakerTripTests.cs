#nullable enable
using System;
using System.Reflection;
using System.Threading.Tasks;
using Azure;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Providers;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Providers
{
    /// <summary>
    /// Exercises the circuit-trip / fallback path of
    /// <see cref="AzureBlobRetryProviderWithCircuitBreaker"/>.
    /// </summary>
    public class AzureBlobCircuitBreakerTripTests
    {
        private static CircuitBreakerOptions Options() => new()
        {
            FailureThreshold = 0.5,
            MinimumAttempts = 2,
            SamplingDurationS = 30,
            DurationOfBreakS = 30
        };

        [Fact]
        public async Task PersistentTransientFailures_TripCircuit_ThenThrowTooManyRequests()
        {
            var provider = new AzureBlobRetryProviderWithCircuitBreaker(Options(), useCircuit: true);

            var broke = false;
            provider.BreakEvent += (_, _) => broke = true;

            // Persistent 503 failures should trip the circuit and, once open, the fallback
            // converts the broken-circuit exception into a TooManyRequestsException.
            await Assert.ThrowsAsync<TooManyRequestsException>(() =>
                provider.ExecuteWithRetry(
                    () => throw new RequestFailedException(503, "server busy"),
                    typeof(AzureBlobCircuitBreakerTripTests),
                    nameof(PersistentTransientFailures_TripCircuit_ThenThrowTooManyRequests)));

            Assert.True(broke, "Expected the circuit BreakEvent to fire.");

            // Circuit should now report open with a positive wait time.
            Assert.False(provider.IsCircuitClosed(out var waitFor));
            Assert.True(waitFor > TimeSpan.Zero);
        }

        [Theory]
        [InlineData(503, true)]
        [InlineData(500, true)]
        [InlineData(502, false)]
        [InlineData(404, false)]
        public void IsCircuitBreakerAzureBlobException_ClassifiesStatusCodes(int status, bool expected)
        {
            var provider = new AzureBlobRetryProviderWithCircuitBreaker(Options(), useCircuit: true);
            var method = typeof(AzureBlobRetryProviderWithCircuitBreaker)
                .GetMethod("IsCircuitBreakerAzureBlobException", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.NotNull(method);

            var result = (bool)method!.Invoke(provider, new object[] { new RequestFailedException(status, "x") })!;

            Assert.Equal(expected, result);
        }
    }
}
