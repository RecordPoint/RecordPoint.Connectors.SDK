namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// Outcome of a notification
    /// </summary>
    public class NotificationOutcome
    {

        /// <summary>
        /// Unit of work has completed succcessfully
        /// </summary>
        /// <returns></returns>
        public static NotificationOutcome OK()
        {
            return new NotificationOutcome()
            {
                OutcomeType = NotificationOutcomeType.Ok
            };
        }

        /// <summary>
        /// Notification handling has failed
        /// </summary>
        /// <returns></returns>
        public static NotificationOutcome Failed(string reason)
        {
            return new NotificationOutcome()
            {
                OutcomeType = NotificationOutcomeType.Failed,
                Reason = reason
            };
        }

        /// <summary>
        /// Notification outcome type
        /// </summary>
        public NotificationOutcomeType OutcomeType { get; set; }

        /// <summary>
        /// Message that describes the reason for the outcome
        /// </summary>
        public string Reason { get; set; } = string.Empty;

    }
}
