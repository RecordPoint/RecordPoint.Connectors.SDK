using RecordPoint.Connectors.SDK.Helpers;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Helpers
{
    public class ExceptionHelperTest
    {
        [Fact]
        public void IsTaskCancellation_ReturnsTrue_ForTaskCanceledException_WhenTokenCancelled()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            var ex = new TaskCanceledException();

            Assert.True(ex.IsTaskCancellation(cts.Token));
        }

        [Fact]
        public void IsTaskCancellation_ReturnsFalse_ForTaskCanceledException_WhenTokenNotCancelled()
        {
            var ex = new TaskCanceledException();

            Assert.False(ex.IsTaskCancellation(CancellationToken.None));
        }

        [Fact]
        public void IsTaskCancellation_UnwrapsAggregateException()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            var ex = new AggregateException(new TaskCanceledException());

            Assert.True(ex.IsTaskCancellation(cts.Token));
        }

        [Fact]
        public void IsTaskCancellation_ReturnsFalse_ForOtherException()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            var ex = new InvalidOperationException();

            Assert.False(ex.IsTaskCancellation(cts.Token));
        }

        [Fact]
        public void IsAssignableFrom_ReturnsTrue_WhenTypeMatchesAndConditionSatisfied()
        {
            var ex = new InvalidOperationException("boom");

            Assert.True(ex.IsAssignableFrom<InvalidOperationException>(e => e.Message == "boom"));
        }

        [Fact]
        public void IsAssignableFrom_ReturnsFalse_WhenConditionNotSatisfied()
        {
            var ex = new InvalidOperationException("boom");

            Assert.False(ex.IsAssignableFrom<InvalidOperationException>(e => e.Message == "other"));
        }

        [Fact]
        public void IsAssignableFrom_ReturnsFalse_WhenTypeDoesNotMatch()
        {
            var ex = new InvalidOperationException("boom");

            Assert.False(ex.IsAssignableFrom<ArgumentException>(e => true));
        }

        [Fact]
        public void IsAssignableFrom_HandlesAggregateException_AllInnerMatch()
        {
            var ex = new AggregateException(
                new InvalidOperationException("boom"),
                new InvalidOperationException("boom"));

            Assert.True(ex.IsAssignableFrom<InvalidOperationException>(e => e.Message == "boom"));
        }

        [Fact]
        public void IsAssignableFrom_HandlesAggregateException_NotAllInnerMatch()
        {
            var ex = new AggregateException(
                new InvalidOperationException("boom"),
                new ArgumentException("other"));

            Assert.False(ex.IsAssignableFrom<InvalidOperationException>(e => e.Message == "boom"));
        }
    }
}
