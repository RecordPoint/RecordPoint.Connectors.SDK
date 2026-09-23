using RecordPoint.Connectors.SDK.Providers;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Providers
{
    public class DateTimeProviderTest
    {
        [Fact]
        public void Instance_ReturnsSingleton()
        {
            var first = DateTimeProvider.Instance;
            var second = DateTimeProvider.Instance;

            Assert.NotNull(first);
            Assert.Same(first, second);
        }

        [Fact]
        public void UtcNow_ReturnsCurrentUtcTime()
        {
            var before = DateTime.UtcNow;

            var value = DateTimeProvider.Instance.UtcNow;

            var after = DateTime.UtcNow;

            Assert.True(value >= before && value <= after);
            Assert.Equal(DateTimeKind.Utc, value.Kind);
        }
    }
}
