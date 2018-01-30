namespace RecordPoint.Connectors.SDK
{
    public static class Fields
    {
        public static string Title = nameof(Title);
        public static string ItemTypeId = nameof(ItemTypeId);
        public static string ExternalId = nameof(ExternalId);
        public static string SourceLastModifiedBy = nameof(SourceLastModifiedBy);
        public static string SourceLastModifiedDate = nameof(SourceLastModifiedDate);
        public static string SourceCreatedBy = nameof(SourceCreatedBy);
        public static string SourceCreatedDate = nameof(SourceCreatedDate);
        public static string Author = nameof(Author);
        public static string MimeType = nameof(MimeType);
        public static string ContentVersion = nameof(ContentVersion);
        public static string Location = nameof(Location);
        public static string MediaType = nameof(MediaType);
        public static string ConnectorId = nameof(ConnectorId);
        public static string ParentExternalId = nameof(ParentExternalId);

        public static class AuditEvent
        {
            public static string EventExternalId = nameof(EventExternalId);
            public static string ExternalId = nameof(ExternalId);
            public static string Created = nameof(Created);
            public static string Description = nameof(Description);
            public static string EventType = nameof(EventType);
            public static string UserName= nameof(UserName);
            public static string UserId = nameof(UserId);
        }
    }
}
