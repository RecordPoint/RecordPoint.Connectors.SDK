namespace RecordPoint.Connectors.SDK.Requests
{
    /// <summary>Request type identifiers. A new operation adds a constant here and a handler.</summary>
    public static class ConnectorRequestTypes
    {
        /// <summary>Returns the payload unchanged. Exercises the whole path without a source system.</summary>
        public const string Echo = nameof(Echo);

        /// <summary>Verifies the supplied credentials against the source and persists them on success.</summary>
        public const string Authenticate = nameof(Authenticate);

        /// <summary>What the connector can express, and the objects worth choosing. Takes no payload.</summary>
        public const string Bootstrap = nameof(Bootstrap);

        /// <summary>One object's detail and selectable fields. The payload names the object.</summary>
        public const string ObjectModel = nameof(ObjectModel);

        /// <summary>
        /// Whether an authoring document converts to a runnable native configuration, answered as
        /// per-field findings the configuration UI can display. The payload is the document itself.
        /// </summary>
        public const string ValidateModel = nameof(ValidateModel);
    }
}
