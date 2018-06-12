namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Describes the results a processing operation for a connector notification.
    /// </summary>
    public enum ProcessingResult
    {
        /// <summary>
        /// Indicates that the required processing for the notification completed successfully.
        /// </summary>
        OK,
        /// <summary>
        /// Indicates that the result of processing a notification is unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Indicates that the notification was not processed because the 
        /// connector is not enabled.
        /// </summary>
        ConnectorDisabled,
        /// <summary>
        /// Indicates that the notification was not processed because the 
        /// connector is not subscribed to the notification type.
        /// </summary>
        ConnectorNotSubscribed,
        /// <summary>
        /// Indicates that the notification was not processed because the
        /// connector could not be reached.
        /// </summary>
        ConnectorNotReachable,
        /// <summary>
        /// Indicates that an error occured during processing of a notification.
        /// </summary>
        NotificationError
    }
}
