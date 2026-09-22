#nullable enable
using System;
using System.Threading.Tasks;
using Azure;
using Polly;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Providers;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Providers
{
    /// <summary>
    /// Test subclass that removes the exponential backoff delay so retry behaviour
    /// can be exercised quickly.
    /// </summary>
    internal sealed class NoDelayAzureBlobRetryProvider : AzureBlobRetryProvider
    {
        protected override IAsyncPolicy GetRetryPolicy(Type type, string methodName)
        {
            return Policy
                .Handle<RequestFailedException>(ex =>
                    !((ex.Status >= 400 && ex.Status < 500) || ex.Status == 501 || ex.Status == 505))
                .WaitAndRetryAsync(3, _ => TimeSpan.Zero);
        }
    }

    public class AzureBlobRetryProviderTests
    {
        [Fact]
        public async Task ExecuteWithRetry_Success_RunsOnce()
        {
            var provider = new AzureBlobRetryProvider();
            var count = 0;
            await provider.ExecuteWithRetry(() =>
            {
                count++;
                return Task.CompletedTask;
            }, typeof(AzureBlobRetryProviderTests), "Test");
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task ExecuteWithRetry_NonTransient_ThrowsWithoutRetry()
        {
            var provider = new AzureBlobRetryProvider();
            var count = 0;
            await Assert.ThrowsAsync<RequestFailedException>(() =>
                provider.ExecuteWithRetry(() =>
                {
                    count++;
                    throw new RequestFailedException(400, "bad request");
                }, typeof(AzureBlobRetryProviderTests), "Test"));
            Assert.Equal(1, count); // no retries on 4xx
        }

        [Fact]
        public async Task ExecuteWithRetry_NonRequestFailedException_NotRetried()
        {
            var provider = new AzureBlobRetryProvider();
            var count = 0;
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                provider.ExecuteWithRetry(() =>
                {
                    count++;
                    throw new InvalidOperationException("boom");
                }, typeof(AzureBlobRetryProviderTests), "Test"));
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task ExecuteWithRetry_Transient_RetriesThenSucceeds()
        {
            var provider = new NoDelayAzureBlobRetryProvider();
            var count = 0;
            await provider.ExecuteWithRetry(() =>
            {
                count++;
                if (count < 2)
                    throw new RequestFailedException(500, "server error");
                return Task.CompletedTask;
            }, typeof(AzureBlobRetryProviderTests), "Test");
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task ExecuteWithRetry_Transient_ExhaustsRetries()
        {
            var provider = new NoDelayAzureBlobRetryProvider();
            var count = 0;
            await Assert.ThrowsAsync<RequestFailedException>(() =>
                provider.ExecuteWithRetry(() =>
                {
                    count++;
                    throw new RequestFailedException(503, "unavailable");
                }, typeof(AzureBlobRetryProviderTests), "Test"));
            Assert.Equal(4, count); // 1 initial + 3 retries
        }
    }

    public class AzureBlobRetryProviderWithCircuitBreakerTests
    {
        private static CircuitBreakerOptions Options() => new()
        {
            FailureThreshold = 0.5,
            MinimumAttempts = 2,
            SamplingDurationS = 30,
            DurationOfBreakS = 10
        };

        [Fact]
        public void IsCircuitClosed_TrueInitially()
        {
            var provider = new AzureBlobRetryProviderWithCircuitBreaker(Options(), useCircuit: true);
            Assert.True(provider.IsCircuitClosed(out var waitFor));
            Assert.Equal(TimeSpan.Zero, waitFor);
        }

        [Fact]
        public async Task ExecuteWithRetry_Success_WithCircuit()
        {
            var provider = new AzureBlobRetryProviderWithCircuitBreaker(Options(), useCircuit: true);
            var ran = false;
            await provider.ExecuteWithRetry(() => { ran = true; return Task.CompletedTask; },
                typeof(AzureBlobRetryProviderWithCircuitBreakerTests), "Test");
            Assert.True(ran);
        }

        [Fact]
        public async Task ExecuteWithRetry_Success_WithoutCircuit()
        {
            var provider = new AzureBlobRetryProviderWithCircuitBreaker(Options(), useCircuit: false);
            var ran = false;
            await provider.ExecuteWithRetry(() => { ran = true; return Task.CompletedTask; },
                typeof(AzureBlobRetryProviderWithCircuitBreakerTests), "Test");
            Assert.True(ran);
        }

        [Fact]
        public async Task ExecuteWithRetry_NonTransient_Throws()
        {
            var provider = new AzureBlobRetryProviderWithCircuitBreaker(Options(), useCircuit: true);
            await Assert.ThrowsAsync<RequestFailedException>(() =>
                provider.ExecuteWithRetry(() => throw new RequestFailedException(404, "not found"),
                    typeof(AzureBlobRetryProviderWithCircuitBreakerTests), "Test"));
        }
    }
}
