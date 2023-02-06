using RecordPoint.Connectors.SDK.Content;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Content
{
    public class AggregationSut : CommonSutBase
    {

    }

    public class AggregationTests : CommonTestBase<AggregationSut>
    {
        internal static Aggregation CreateAggregation()
            => new()
            {
                ExternalId = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                Author = Guid.NewGuid().ToString(),
                ContentVersion = Guid.NewGuid().ToString(),
                Location = Guid.NewGuid().ToString(),
                SourceCreatedBy = Guid.NewGuid().ToString(),
                SourceCreatedDate = DateTime.UtcNow,
                SourceLastModifiedBy = Guid.NewGuid().ToString(),
                SourceLastModifiedDate = DateTime.UtcNow,
                MetaDataItems = new List<MetaDataItem> {
                    new MetaDataItem {
                        Name = Guid.NewGuid().ToString(),
                        Type = "String",
                        Value = Guid.NewGuid().ToString()
                    }
                }
            };

        [Fact]
        public async Task AggregationEquality_ClonedAggregation_IsEqual()
        {
            await StartSutAsync();

            var aggregation1 = CreateAggregation();
            var aggregation2 = Clone(aggregation1);

            Assert.True(aggregation1.Equals(aggregation2));
        }

        [Fact]
        public async Task AggregationEquality_DifferentAggregation_IsNotEqual()
        {
            await StartSutAsync();

            var aggregation1 = CreateAggregation();
            var aggregation2 = CreateAggregation();

            Assert.False(aggregation1.Equals(aggregation2));
        }

        [Fact]
        public async Task AggregationEquality_UpdatedAggregationMetaData_IsNotEqual()
        {
            await StartSutAsync();

            var aggregation1 = CreateAggregation();
            var aggregation2 = Clone(aggregation1);

            aggregation2.MetaDataItems.Add(new MetaDataItem
            {
                Name = Guid.NewGuid().ToString(),
                Type = "String",
                Value = Guid.NewGuid().ToString()
            });

            Assert.False(aggregation1.Equals(aggregation2));
        }

        [Fact]
        public async Task AggregationEquality_ClonedAggregation_WithNullValues_IsEqual()
        {
            await StartSutAsync();

            var aggregation1 = CreateAggregation();
            aggregation1.ExternalId = null;
            var metaDataItem = aggregation1.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;
            var aggregation2 = Clone(aggregation1);

            Assert.True(aggregation1.Equals(aggregation2));
        }

        [Fact]
        public async Task AggregationEquality_UpdatedAggregation_WithNullValues_IsNotEqual()
        {
            await StartSutAsync();

            var aggregation1 = CreateAggregation();
            var aggregation2 = Clone(aggregation1);
            aggregation2.ExternalId = null;
            var metaDataItem = aggregation2.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;

            Assert.False(aggregation1.Equals(aggregation2));
        }

        [Fact]
        public async Task AggregationEquality_NullObject_IsNotEqual()
        {
            await StartSutAsync();

            var aggregation1 = CreateAggregation();
            object aggregation2 = null;

            Assert.False(aggregation1.Equals(aggregation2));
        }

        [Fact]
        public async Task AggregationEquality_NullValue_IsNotEqual()
        {
            await StartSutAsync();

            var aggregation1 = CreateAggregation();
            Aggregation aggregation2 = null;

            Assert.False(aggregation1.Equals(aggregation2));
        }

        [Fact]
        public async Task AggregationEquality_DifferentType_IsNotEqual()
        {
            await StartSutAsync();

            var aggregation1 = CreateAggregation();
            var notAggregation = new NotAggregation();

            Assert.False(aggregation1.Equals(notAggregation));
        }

        private record NotAggregation
        {
        }
    }
}
