using RecordPoint.Connectors.SDK.Content;
using Xunit;
using BinaryMetaInfo = RecordPoint.Connectors.SDK.Content.BinaryMetaInfo;

namespace RecordPoint.Connectors.SDK.Test.Content
{
    public class BinaryMetaInfoSut : CommonSutBase
    {

    }

    public class BinaryMetaInfoTests : CommonTestBase<BinaryMetaInfoSut>
    {
        internal static BinaryMetaInfo CreateBinaryMetaInfo()
            => new()
            {
                ExternalId = Guid.NewGuid().ToString(),
                Author = Guid.NewGuid().ToString(),
                ContentVersion = Guid.NewGuid().ToString(),
                FileSize = 10000,
                SourceCreatedBy = Guid.NewGuid().ToString(),
                SourceCreatedDate = DateTime.UtcNow,
                SourceLastModifiedBy = Guid.NewGuid().ToString(),
                SourceLastModifiedDate = DateTime.UtcNow,
                Title = Guid.NewGuid().ToString(),
                ContentToken = Guid.NewGuid().ToString(),
                ContentTokenType = Guid.NewGuid().ToString(),
                FileHash = Guid.NewGuid().ToString(),
                FileName = Guid.NewGuid().ToString(),
                ItemExternalId = Guid.NewGuid().ToString(),
                MimeType = Guid.NewGuid().ToString(),
                MetaDataItems = new List<MetaDataItem> {
                    new MetaDataItem {
                        Name = Guid.NewGuid().ToString(),
                        Type = "String",
                        Value = Guid.NewGuid().ToString()
                    }
                }
            };

        [Fact]
        public async Task BinaryMetaInfoEquality_ClonedBinaryMetaInfo_IsEqual()
        {
            await StartSutAsync();

            var binaryMetaInfo1 = CreateBinaryMetaInfo();
            var binaryMetaInfo2 = Clone(binaryMetaInfo1);

            Assert.True(binaryMetaInfo1.Equals(binaryMetaInfo2));
        }

        [Fact]
        public async Task BinaryMetaInfoEquality_DifferentBinaryMetaInfo_IsNotEqual()
        {
            await StartSutAsync();

            var binaryMetaInfo1 = CreateBinaryMetaInfo();
            var binaryMetaInfo2 = CreateBinaryMetaInfo();

            Assert.False(binaryMetaInfo1.Equals(binaryMetaInfo2));
        }

        [Fact]
        public async Task BinaryMetaInfoEquality_UpdatedBinaryMetaInfoMetaData_IsNotEqual()
        {
            await StartSutAsync();

            var binaryMetaInfo1 = CreateBinaryMetaInfo();
            var binaryMetaInfo2 = Clone(binaryMetaInfo1);

            binaryMetaInfo2.MetaDataItems.Add(new MetaDataItem
            {
                Name = Guid.NewGuid().ToString(),
                Type = "String",
                Value = Guid.NewGuid().ToString()
            });

            Assert.False(binaryMetaInfo1.Equals(binaryMetaInfo2));
        }

        [Fact]
        public async Task BinaryMetaInfoEquality_ClonedBinaryMetaInfo_WithNullValues_IsEqual()
        {
            await StartSutAsync();

            var binaryMetaInfo1 = CreateBinaryMetaInfo();
            binaryMetaInfo1.ExternalId = null;
            var metaDataItem = binaryMetaInfo1.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;
            var binaryMetaInfo2 = Clone(binaryMetaInfo1);

            Assert.True(binaryMetaInfo1.Equals(binaryMetaInfo2));
        }

        [Fact]
        public async Task BinaryMetaInfoEquality_UpdatedBinaryMetaInfo_WithNullValues_IsNotEqual()
        {
            await StartSutAsync();

            var binaryMetaInfo1 = CreateBinaryMetaInfo();
            var binaryMetaInfo2 = Clone(binaryMetaInfo1);
            binaryMetaInfo2.ExternalId = null;
            var metaDataItem = binaryMetaInfo2.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;

            Assert.False(binaryMetaInfo1.Equals(binaryMetaInfo2));
        }

        [Fact]
        public async Task BinaryMetaInfoEquality_NullObject_IsNotEqual()
        {
            await StartSutAsync();

            var binaryMetaInfo1 = CreateBinaryMetaInfo();
            object binaryMetaInfo2 = null;

            Assert.False(binaryMetaInfo1.Equals(binaryMetaInfo2));
        }

        [Fact]
        public async Task BinaryMetaInfoEquality_NullValue_IsNotEqual()
        {
            await StartSutAsync();

            var binaryMetaInfo1 = CreateBinaryMetaInfo();
            BinaryMetaInfo binaryMetaInfo2 = null;

            Assert.False(binaryMetaInfo1.Equals(binaryMetaInfo2));
        }

        [Fact]
        public async Task BinaryMetaInfoEquality_DifferentType_IsNotEqual()
        {
            await StartSutAsync();

            var binaryMetaInfo1 = CreateBinaryMetaInfo();
            var notBinaryMetaInfo = new NotBinaryMetaInfo();

            Assert.False(binaryMetaInfo1.Equals(notBinaryMetaInfo));
        }

        [Fact]
        public async Task BinaryMetaInfo_CombinedExternalIdAndItemExternalIdExceedsMaxLength_ThrowsArgumentOutOfRangeException()
        {
            await StartSutAsync();

            const int maxBinaryExternalIdLength = 974;
            var binaryMetaInfo = new BinaryMetaInfo
            {
                ItemExternalId = new string('i', 100)
            };
            var tooLongExternalId = new string('a', maxBinaryExternalIdLength - 100 + 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => binaryMetaInfo.ExternalId = tooLongExternalId);
        }

        [Fact]
        public async Task BinaryMetaInfo_CombinedExternalIdAndItemExternalIdAtMaxLength_DoesNotThrow()
        {
            await StartSutAsync();

            const int maxBinaryExternalIdLength = 974;
            var itemExternalId = new string('i', 100);
            var externalId = new string('a', maxBinaryExternalIdLength - itemExternalId.Length);
            var binaryMetaInfo = new BinaryMetaInfo
            {
                ItemExternalId = itemExternalId
            };

            binaryMetaInfo.ExternalId = externalId;

            Assert.Equal(externalId, binaryMetaInfo.ExternalId);
            Assert.Equal(itemExternalId, binaryMetaInfo.ItemExternalId);
        }

        [Fact]
        public async Task BinaryMetaInfo_ItemExternalIdCausesCombinedLengthToExceedMax_ThrowsArgumentOutOfRangeException()
        {
            await StartSutAsync();

            const int maxBinaryExternalIdLength = 974;
            var binaryMetaInfo = new BinaryMetaInfo
            {
                ExternalId = new string('a', 500)
            };
            var tooLongItemExternalId = new string('i', maxBinaryExternalIdLength - 500 + 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => binaryMetaInfo.ItemExternalId = tooLongItemExternalId);
        }

        private record NotBinaryMetaInfo
        {
        }
    }
}
