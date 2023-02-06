using RecordPoint.Connectors.SDK.Content;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Content
{
    public class ChannelSut : CommonSutBase
    {

    }

    public class ChannelTests : CommonTestBase<ChannelSut>
    {
        internal static Channel CreateChannel()
            => new()
            {
                ExternalId = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                MetaDataItems = new List<MetaDataItem> {
                    new MetaDataItem {
                        Name = Guid.NewGuid().ToString(),
                        Type = "String",
                        Value = Guid.NewGuid().ToString()
                    }
                }
            };

        [Fact]
        public async Task ChannelEquality_ClonedChannel_IsEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannel();
            var channel2 = Clone(channel1);

            Assert.True(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelEquality_DifferentChannel_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannel();
            var channel2 = CreateChannel();

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelEquality_UpdatedChannelMetaData_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannel();
            var channel2 = Clone(channel1);

            channel2.MetaDataItems.Add(new MetaDataItem
            {
                Name = Guid.NewGuid().ToString(),
                Type = "String",
                Value = Guid.NewGuid().ToString()
            });

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelEquality_ClonedChannel_WithNullValues_IsEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannel();
            channel1.ExternalId = null;
            var metaDataItem = channel1.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;
            var channel2 = Clone(channel1);

            Assert.True(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelEquality_UpdatedChannel_WithNullValues_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannel();
            var channel2 = Clone(channel1);
            channel2.ExternalId = null;
            var metaDataItem = channel2.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelEquality_NullObject_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannel();
            object channel2 = null;

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelEquality_NullValue_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannel();
            Channel channel2 = null;

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelEquality_DifferentType_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannel();
            var notChannel = new NotChannel();

            Assert.False(channel1.Equals(notChannel));
        }

        private record NotChannel
        {
        }
    }
}
