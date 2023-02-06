using RecordPoint.Connectors.SDK.Content;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Content
{
    public class AuditEventSut : CommonSutBase
    {

    }

    public class AuditEventTests : CommonTestBase<AuditEventSut>
    {
        internal static AuditEvent CreateAuditEvent()
            => new()
            {
                ExternalId = Guid.NewGuid().ToString(),
                EventExternalId = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow,
                Description = Guid.NewGuid().ToString(),
                EventType = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString(),
                MetaDataItems = new List<MetaDataItem> {
                    new MetaDataItem {
                        Name = Guid.NewGuid().ToString(),
                        Type = "String",
                        Value = Guid.NewGuid().ToString()
                    }
                }
            };

        [Fact]
        public async Task AuditEventEquality_ClonedAuditEvent_IsEqual()
        {
            await StartSutAsync();

            var auditEvent1 = CreateAuditEvent();
            var auditEvent2 = Clone(auditEvent1);

            Assert.True(auditEvent1.Equals(auditEvent2));
        }

        [Fact]
        public async Task AuditEventEquality_DifferentAuditEvent_IsNotEqual()
        {
            await StartSutAsync();

            var auditEvent1 = CreateAuditEvent();
            var auditEvent2 = CreateAuditEvent();

            Assert.False(auditEvent1.Equals(auditEvent2));
        }

        [Fact]
        public async Task AuditEventEquality_UpdatedAuditEventMetaData_IsNotEqual()
        {
            await StartSutAsync();

            var auditEvent1 = CreateAuditEvent();
            var auditEvent2 = Clone(auditEvent1);

            auditEvent2.MetaDataItems.Add(new MetaDataItem
            {
                Name = Guid.NewGuid().ToString(),
                Type = "String",
                Value = Guid.NewGuid().ToString()
            });

            Assert.False(auditEvent1.Equals(auditEvent2));
        }

        [Fact]
        public async Task AuditEventEquality_ClonedAuditEvent_WithNullValues_IsEqual()
        {
            await StartSutAsync();

            var auditEvent1 = CreateAuditEvent();
            auditEvent1.ExternalId = null;
            var metaDataItem = auditEvent1.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;
            var auditEvent2 = Clone(auditEvent1);

            Assert.True(auditEvent1.Equals(auditEvent2));
        }

        [Fact]
        public async Task AuditEventEquality_UpdatedAuditEvent_WithNullValues_IsNotEqual()
        {
            await StartSutAsync();

            var auditEvent1 = CreateAuditEvent();
            var auditEvent2 = Clone(auditEvent1);
            auditEvent2.ExternalId = null;
            var metaDataItem = auditEvent2.MetaDataItems.First();
            metaDataItem.Type = null;
            metaDataItem.Value = null;

            Assert.False(auditEvent1.Equals(auditEvent2));
        }

        [Fact]
        public async Task AuditEventEquality_NullObject_IsNotEqual()
        {
            await StartSutAsync();

            var auditEvent1 = CreateAuditEvent();
            object auditEvent2 = null;

            Assert.False(auditEvent1.Equals(auditEvent2));
        }

        [Fact]
        public async Task AuditEventEquality_NullValue_IsNotEqual()
        {
            await StartSutAsync();

            var auditEvent1 = CreateAuditEvent();
            AuditEvent auditEvent2 = null;

            Assert.False(auditEvent1.Equals(auditEvent2));
        }

        [Fact]
        public async Task AuditEventEquality_DifferentType_IsNotEqual()
        {
            await StartSutAsync();

            var auditEvent1 = CreateAuditEvent();
            var notAuditEvent = new NotAuditEvent();

            Assert.False(auditEvent1.Equals(notAuditEvent));
        }

        private record NotAuditEvent
        {
        }
    }
}
