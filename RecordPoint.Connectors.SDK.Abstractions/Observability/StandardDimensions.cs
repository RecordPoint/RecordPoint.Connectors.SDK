namespace RecordPoint.Connectors.SDK.Observability
{

    /// <summary>
    /// Set of standard cross-cutting dimensions
    /// </summary>
    public static class StandardDimensions
    {

        public const string COMPANY = "Company";
        public const string SYSTEM = "System";

        public const string SERVICE = "Service";
        public const string SERVICE_ID = "ServiceId";

        public const string WORK = "Work";   // Type of work being executed
        public const string WORK_ID = "WorkId";  // Unique correlation id of the unit of work
        public const string WORK_COMPLETE = "WorkComplete";  // Was all outstanding work completed, if relevant

        public const string TASK = "Task";   // Type of major task within a unit of work

        public const string DEPENDANCY_TYPE = "DependancyType";
        public const string DEPENDANCY = "Dependancy";

        public const string EVENT_TYPE = "EventType";

        public const string ACTION_RESULT_REASON = "ActionResultReason";

        public const string OUTCOME = "Outcome";
        public const string OUTCOME_REASON = "OutcomeReason";

        public const string DECISON = "Decision";

        public const string EXCEPTION = "Exception";

        public const string EXTERNAL_ID = "ExternalId";

        public const string CONNECTOR_ID = "ConnectorId";

        public const string CHANNEL_EXTERNAL_ID = "ChannelExternalId";
        public const string CHANNEL_TITLE = "ChannelTitle";

        public const string TENANT_ID = "TenantId";

        public const string TENANT_DOMAIN_NAME = "TenantDomainName";

    }
}
