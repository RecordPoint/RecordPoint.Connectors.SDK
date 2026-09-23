#nullable enable
using System;
using Moq;
using RecordPoint.Connectors.SDK.Providers;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Providers
{
    public class SettableCircuitProviderTests
    {
        private static SettableCircuitProvider Create(DateTime now, out Mock<IDateTimeProvider> clock)
        {
            clock = new Mock<IDateTimeProvider>();
            clock.SetupGet(x => x.UtcNow).Returns(now);
            return new SettableCircuitProvider { DateTimeProvider = clock.Object };
        }

        [Fact]
        public void IsCircuitClosed_TrueByDefault()
        {
            var provider = Create(DateTime.UtcNow, out _);
            Assert.True(provider.IsCircuitClosed(out var waitFor));
            Assert.Equal(TimeSpan.Zero, waitFor);
        }

        [Fact]
        public void SetOpenUntil_FutureTime_CircuitOpen()
        {
            var now = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var provider = Create(now, out _);

            provider.SetOpenUntil(now.AddMinutes(5));

            Assert.False(provider.IsCircuitClosed(out var waitFor));
            Assert.True(waitFor > TimeSpan.Zero);
            Assert.True(waitFor <= TimeSpan.FromMinutes(5));
        }

        [Fact]
        public void SetOpenUntil_PastTime_CircuitClosed()
        {
            var now = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var provider = Create(now, out _);

            provider.SetOpenUntil(now.AddMinutes(-5));

            Assert.True(provider.IsCircuitClosed(out var waitFor));
            Assert.Equal(TimeSpan.Zero, waitFor);
        }

        [Fact]
        public void SetOpenUntil_DoesNotReduceExistingLaterTime()
        {
            var now = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var provider = Create(now, out _);

            provider.SetOpenUntil(now.AddMinutes(10));
            provider.SetOpenUntil(now.AddMinutes(2)); // earlier - should be ignored

            Assert.False(provider.IsCircuitClosed(out var waitFor));
            Assert.True(waitFor > TimeSpan.FromMinutes(5));
        }

        [Fact]
        public void SetOpenUntil_ExtendsToLaterTime()
        {
            var now = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);
            var provider = Create(now, out _);

            provider.SetOpenUntil(now.AddMinutes(2));
            provider.SetOpenUntil(now.AddMinutes(10)); // later - should replace

            Assert.False(provider.IsCircuitClosed(out var waitFor));
            Assert.True(waitFor > TimeSpan.FromMinutes(5));
        }
    }
}
