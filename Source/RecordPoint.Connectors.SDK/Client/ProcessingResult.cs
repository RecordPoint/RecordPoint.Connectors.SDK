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
        Unknown,
        ConnectorDisabled,
        ConnectorNotSubscribed,
        ConnectorNotReachable,
        /// <summary>
        /// An error occured during processing of a notification.
        /// </summary>
        NotificationError
    }
}
