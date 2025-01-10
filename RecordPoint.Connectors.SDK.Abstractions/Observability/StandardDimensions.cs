namespace RecordPoint.Connectors.SDK.Observability
{

    /// <summary>
    /// Set of standard cross-cutting dimensions
    /// </summary>
    public static class StandardDimensions
    {
        /// <summary>
        /// 
        /// </summary>
        public const string COMPANY = "Company";
        /// <summary>
        /// 
        /// </summary>
        public const string SYSTEM = "System";
        /// <summary>
        /// 
        /// </summary>
        public const string SERVICE = "Service";
        /// <summary>
        /// 
        /// </summary>
        public const string SERVICE_ID = "ServiceId";
        /// <summary>
        /// 
        /// </summary>
        public const string WORK = "Work";   // Type of work being executed
        /// <summary>
        /// 
        /// </summary>
        public const string WORK_ID = "WorkId";  // Unique correlation id of the unit of work
        /// <summary>
        /// 
        /// </summary>
        public const string WORK_COMPLETE = "WorkComplete";  // Was all outstanding work completed, if relevant
        /// <summary>
        /// 
        /// </summary>
        public const string TASK = "Task";   // Type of major task within a unit of work
        /// <summary>
        /// 
        /// </summary>
        public const string DEPENDANCY_TYPE = "DependancyType";
        /// <summary>
        /// 
        /// </summary>
        public const string DEPENDANCY = "Dependancy";
        /// <summary>
        /// 
        /// </summary>
        public const string EVENT_TYPE = "EventType";
        /// <summary>
        /// 
        /// </summary>
        public const string ACTION_RESULT_REASON = "ActionResultReason";
        /// <summary>
        /// 
        /// </summary>
        public const string OUTCOME = "Outcome";
        /// <summary>
        /// 
        /// </summary>
        public const string OUTCOME_REASON = "OutcomeReason";
        /// <summary>
        /// 
        /// </summary>
        public const string DECISON = "Decision";
        /// <summary>
        /// 
        /// </summary>
        public const string EXCEPTION = "Exception";
        /// <summary>
        /// 
        /// </summary>
        public const string EXTERNAL_ID = "ExternalId";
        /// <summary>
        /// /
        /// </summary>
        public const string CONNECTOR_ID = "ConnectorId";
        /// <summary>
        /// 
        /// </summary>
        public const string CHANNEL_EXTERNAL_ID = "ChannelExternalId";
        /// <summary>
        /// 
        /// </summary>
        public const string CHANNEL_TITLE = "ChannelTitle";
        /// <summary>
        /// 
        /// </summary>
        public const string TENANT_ID = "TenantId";
        /// <summary>
        /// 
        /// </summary>
        public const string TENANT_DOMAIN_NAME = "TenantDomainName";

    }
}
