using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Content;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Content
{
    public class ChannelModelTestsSut : CommonSutBase
    {

    }

    public class ChannelModelTests : CommonTestBase<ChannelModelTestsSut>
    {
        internal static ChannelModel CreateChannelModel()
            => new()
            {
                ExternalId = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                MetaData = JsonConvert.SerializeObject(new List<MetaDataItem> {
                    new MetaDataItem {
                        Name = Guid.NewGuid().ToString(),
                        Type = "String",
                        Value = Guid.NewGuid().ToString()
                    }
                })
            };


        [Fact]
        public async Task ChannelModelEquality_ClonedChannel_IsEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannelModel();
            var channel2 = Clone(channel1);

            Assert.True(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelModelEquality_DifferentChannel_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannelModel();
            var channel2 = CreateChannelModel();

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelModelEquality_UpdatedChannelMetaData_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannelModel();
            var channel2 = Clone(channel1);

            var channel2MetaDataItems = JsonConvert.DeserializeObject<List<MetaDataItem>>(channel2.MetaData);

            channel2MetaDataItems.Add(new MetaDataItem
            {
                Name = Guid.NewGuid().ToString(),
                Type = "String",
                Value = Guid.NewGuid().ToString()
            });

            channel2.MetaData = JsonConvert.SerializeObject(channel2MetaDataItems);

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelModelEquality_ClonedChannel_WithNullValues_IsEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannelModel();

            channel1.ExternalId = null;
            var channel1MetaDataItems = JsonConvert.DeserializeObject<List<MetaDataItem>>(channel1.MetaData);
            var metaDataItem = channel1MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;
            channel1.MetaData = JsonConvert.SerializeObject(channel1MetaDataItems);

            var channel2 = Clone(channel1);

            Assert.True(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelModelEquality_UpdatedChannel_WithNullValues_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannelModel();
            var channel2 = Clone(channel1);

            channel2.ExternalId = null;
            var channel2MetaDataItems = JsonConvert.DeserializeObject<List<MetaDataItem>>(channel2.MetaData);
            var metaDataItem = channel2MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;
            channel2.MetaData = JsonConvert.SerializeObject(channel2MetaDataItems);

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelModelEquality_NullObject_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannelModel();
            object channel2 = null;

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelModelEquality_NullValue_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannelModel();
            ChannelModel channel2 = null;

            Assert.False(channel1.Equals(channel2));
        }

        [Fact]
        public async Task ChannelModelEquality_DifferentType_IsNotEqual()
        {
            await StartSutAsync();

            var channel1 = CreateChannelModel();
            var notChannelModel = new NotChannelModel();

            Assert.False(channel1.Equals(notChannelModel));
        }

        private record NotChannelModel
        {
        }
    }
}
