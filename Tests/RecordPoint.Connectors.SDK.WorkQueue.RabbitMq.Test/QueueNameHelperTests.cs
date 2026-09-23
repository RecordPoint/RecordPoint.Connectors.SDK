#nullable enable
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test
{
    public class QueueNameHelperTests
    {
        [Theory]
        [InlineData("Content Manager", "", "content-manager")]
        [InlineData("Content Manager", "prefix", "prefix-content-manager")]
        [InlineData("SIMPLE", "", "simple")]
        [InlineData("Multi Word Type", "abc", "abc-multi-word-type")]
        public void GetQueueName_FormatsAndAppliesPrefix(string workType, string prefix, string expected)
        {
            Assert.Equal(expected, QueueNameHelper.GetQueueName(workType, prefix));
        }

        [Theory]
        [InlineData("Content Manager", "", "content-manager-DL")]
        [InlineData("Content Manager", "prefix", "prefix-content-manager-DL")]
        [InlineData("SIMPLE", "", "simple-DL")]
        public void GetDLQueueName_FormatsAndAppliesPrefixWithSuffix(string workType, string prefix, string expected)
        {
            Assert.Equal(expected, QueueNameHelper.GetDLQueueName(workType, prefix));
        }

        [Fact]
        public void GetQueueName_NullPrefix_TreatedAsEmpty()
        {
            Assert.Equal("work-type", QueueNameHelper.GetQueueName("Work Type", null!));
        }

        [Fact]
        public void GetDLQueueName_NullPrefix_TreatedAsEmpty()
        {
            Assert.Equal("work-type-DL", QueueNameHelper.GetDLQueueName("Work Type", null!));
        }

        [Fact]
        public void DeadLetterSuffix_IsExpectedValue()
        {
            Assert.Equal("DL", QueueNameHelper.DeadLetterSuffix);
        }
    }
}
