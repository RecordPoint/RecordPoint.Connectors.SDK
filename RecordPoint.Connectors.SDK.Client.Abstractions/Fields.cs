namespace RecordPoint.Connectors.SDK
{
    /// <summary>
    /// The fields.
    /// </summary>
    public static class Fields
    {
        /// <summary>
        /// The title.
        /// </summary>
        public const string Title = nameof(Title);
        /// <summary>
        /// The item type id.
        /// </summary>
        public const string ItemTypeId = nameof(ItemTypeId);
        /// <summary>
        /// The external id.
        /// </summary>
        public const string ExternalId = nameof(ExternalId);
        /// <summary>
        /// The source last modified by.
        /// </summary>
        public const string SourceLastModifiedBy = nameof(SourceLastModifiedBy);
        /// <summary>
        /// The source last modified date.
        /// </summary>
        public const string SourceLastModifiedDate = nameof(SourceLastModifiedDate);
        /// <summary>
        /// The source created by.
        /// </summary>
        public const string SourceCreatedBy = nameof(SourceCreatedBy);
        /// <summary>
        /// The source created date.
        /// </summary>
        public const string SourceCreatedDate = nameof(SourceCreatedDate);
        /// <summary>
        /// The author.
        /// </summary>
        public const string Author = nameof(Author);
        /// <summary>
        /// The mime type.
        /// </summary>
        public const string MimeType = nameof(MimeType);
        /// <summary>
        /// The content version.
        /// </summary>
        public const string ContentVersion = nameof(ContentVersion);
        /// <summary>
        /// The location.
        /// </summary>
        public const string Location = nameof(Location);
        /// <summary>
        /// The media type.
        /// </summary>
        public const string MediaType = nameof(MediaType);
        /// <summary>
        /// The barcode type.
        /// </summary>
        public const string BarcodeType = nameof(BarcodeType);
        /// <summary>
        /// The barcode value.
        /// </summary>
        public const string BarcodeValue = nameof(BarcodeValue);
        /// <summary>
        /// The record category ID.
        /// </summary>
        public const string RecordCategoryID = nameof(RecordCategoryID);
        /// <summary>
        /// The connector id.
        /// </summary>
        public const string ConnectorId = nameof(ConnectorId);
        /// <summary>
        /// The parent external id.
        /// </summary>
        public const string ParentExternalId = nameof(ParentExternalId);
        /// <summary>
        /// The security profile identifier.
        /// </summary>
        public const string SecurityProfileIdentifier = nameof(SecurityProfileIdentifier);

        public static class AuditEvent
        {
            /// <summary>
            /// The event external id.
            /// </summary>
            public const string EventExternalId = nameof(EventExternalId);
            /// <summary>
            /// The external id.
            /// </summary>
            public const string ExternalId = nameof(ExternalId);
            /// <summary>
            /// The created.
            /// </summary>
            public const string Created = nameof(Created);
            /// <summary>
            /// The description.
            /// </summary>
            public const string Description = nameof(Description);
            /// <summary>
            /// The event type.
            /// </summary>
            public const string EventType = nameof(EventType);
            /// <summary>
            /// The user name.
            /// </summary>
            public const string UserName = nameof(UserName);
            /// <summary>
            /// The user id.
            /// </summary>
            public const string UserId = nameof(UserId);
        }

        public static class MessageCode
        {
            /// <summary>
            /// The connector not enabled.
            /// </summary>
            public const string ConnectorNotEnabled = "ConnectorNotEnabled";
            /// <summary>
            /// The protection not enabled.
            /// </summary>
            public const string ProtectionNotEnabled = "ProtectionNotEnabled";
        }

        /// <summary>
        /// Names of metadata keys must be valid C# identifiers:
        /// https://docs.microsoft.com/en-us/rest/api/storageservices/Naming-and-Referencing-Containers--Blobs--and-Metadata#metadata-names
        /// </summary>
        public static class MetaDataKeys
        {
            /// <summary>
            /// The item binary file name.
            /// </summary>
            public const string ItemBinary_FileName = nameof(ItemBinary_FileName);
            /// <summary>
            /// The item binary correlation id.
            /// </summary>
            public const string ItemBinary_CorrelationId = nameof(ItemBinary_CorrelationId);
        }
    }
}
