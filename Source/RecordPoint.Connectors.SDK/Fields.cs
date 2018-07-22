namespace RecordPoint.Connectors.SDK
{
    public static class Fields
    {
        public const string Title = nameof(Title);
        public const string ItemTypeId = nameof(ItemTypeId);
        public const string ExternalId = nameof(ExternalId);
        public const string SourceLastModifiedBy = nameof(SourceLastModifiedBy);
        public const string SourceLastModifiedDate = nameof(SourceLastModifiedDate);
        public const string SourceCreatedBy = nameof(SourceCreatedBy);
        public const string SourceCreatedDate = nameof(SourceCreatedDate);
        public const string Author = nameof(Author);
        public const string MimeType = nameof(MimeType);
        public const string ContentVersion = nameof(ContentVersion);
        public const string Location = nameof(Location);
        public const string MediaType = nameof(MediaType);
        public const string BarcodeType = nameof(BarcodeType);
        public const string BarcodeValue = nameof(BarcodeValue);
        public const string RecordCategoryID = nameof(RecordCategoryID);
        public const string ConnectorId = nameof(ConnectorId);
        public const string ParentExternalId = nameof(ParentExternalId);

        public static class AuditEvent
        {
            public const string EventExternalId = nameof(EventExternalId);
            public const string ExternalId = nameof(ExternalId);
            public const string Created = nameof(Created);
            public const string Description = nameof(Description);
            public const string EventType = nameof(EventType);
            public const string UserName= nameof(UserName);
            public const string UserId = nameof(UserId);
        }

        public static class MessageCode
        {
            public const string ConnectorNotEnabled = "ConnectorNotEnabled";
            public const string ProtectionNotEnabled = "ProtectionNotEnabled";
        }
    }
}
