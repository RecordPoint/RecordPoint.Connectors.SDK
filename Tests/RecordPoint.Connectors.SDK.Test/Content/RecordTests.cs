using RecordPoint.Connectors.SDK.Content;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.Content
{
    public class RecordSut : CommonSutBase
    {

    }

    public class RecordTests : CommonTestBase<RecordSut>
    {
        internal static Record CreateRecord()
            => new()
            {
                ExternalId = Guid.NewGuid().ToString(),
                Author = Guid.NewGuid().ToString(),
                ContentVersion = Guid.NewGuid().ToString(),
                FileSize = 10000,
                Location = Guid.NewGuid().ToString(),
                MediaType = Guid.NewGuid().ToString(),
                MimeType = Guid.NewGuid().ToString(),
                ParentExternalId = Guid.NewGuid().ToString(),
                SourceCreatedBy = Guid.NewGuid().ToString(),
                SourceCreatedDate = DateTime.UtcNow,
                SourceLastModifiedBy = Guid.NewGuid().ToString(),
                SourceLastModifiedDate = DateTime.UtcNow,
                Title = Guid.NewGuid().ToString(),
                Binaries = new List<BinaryMetaInfo>
                {
                    BinaryMetaInfoTests.CreateBinaryMetaInfo()
                },
                MetaDataItems = new List<MetaDataItem> {
                    new MetaDataItem {
                        Name = Guid.NewGuid().ToString(),
                        Type = "String",
                        Value = Guid.NewGuid().ToString()
                    }
                }
            };

        [Fact]
        public async Task RecordEquality_ClonedRecord_IsEqual()
        {
            await StartSutAsync();

            var record1 = CreateRecord();
            var record2 = Clone(record1);

            Assert.True(record1.Equals(record2));
        }

        [Fact]
        public async Task RecordEquality_DifferentRecord_IsNotEqual()
        {
            await StartSutAsync();

            var record1 = CreateRecord();
            var record2 = CreateRecord();

            Assert.False(record1.Equals(record2));
        }

        [Fact]
        public async Task RecordEquality_UpdatedRecordMetaData_IsNotEqual()
        {
            await StartSutAsync();

            var record1 = CreateRecord();
            var record2 = Clone(record1);

            record2.MetaDataItems.Add(new MetaDataItem
            {
                Name = Guid.NewGuid().ToString(),
                Type = "String",
                Value = Guid.NewGuid().ToString()
            });

            Assert.False(record1.Equals(record2));
        }

        [Fact]
        public async Task RecordEquality_ClonedRecord_WithNullValues_IsEqual()
        {
            await StartSutAsync();

            var record1 = CreateRecord();
            record1.ExternalId = null;
            var metaDataItem = record1.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;
            var record2 = Clone(record1);

            Assert.True(record1.Equals(record2));
        }

        [Fact]
        public async Task RecordEquality_UpdatedRecord_WithNullValues_IsNotEqual()
        {
            await StartSutAsync();

            var record1 = CreateRecord();
            var record2 = Clone(record1);
            record2.ExternalId = null;
            var metaDataItem = record2.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;

            Assert.False(record1.Equals(record2));
        }

        [Fact]
        public async Task RecordEquality_NullObject_IsNotEqual()
        {
            await StartSutAsync();

            var record1 = CreateRecord();
            object record2 = null;

            Assert.False(record1.Equals(record2));
        }

        [Fact]
        public async Task RecordEquality_NullValue_IsNotEqual()
        {
            await StartSutAsync();

            var record1 = CreateRecord();
            Record record2 = null;

            Assert.False(record1.Equals(record2));
        }

        [Fact]
        public async Task RecordEquality_DifferentType_IsNotEqual()
        {
            await StartSutAsync();

            var record1 = CreateRecord();
            var notRecord = new NotRecord();

            Assert.False(record1.Equals(notRecord));
        }

        private record NotRecord
        {
        }
    }
}
