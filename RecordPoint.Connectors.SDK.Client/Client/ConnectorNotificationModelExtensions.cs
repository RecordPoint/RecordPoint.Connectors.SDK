using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Client
{
    public static class ConnectorNotificationModelExtensions
    {
        /// <summary>
        /// Creates a ConnectorNotificationAcknowledgeModel to be used to acknowledge the results
        /// of processing of a connector notification.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="processingResult"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ConnectorNotificationAcknowledgeModel ToAcknowledge(this ConnectorNotificationModel model,
            ProcessingResult processingResult,
            string message = "")
        {
            return new ConnectorNotificationAcknowledgeModel
            {
                ConnectorId = model.ConnectorId,
                NotificationId = model.Id,
                ProcessingResult = processingResult.ToString(),
                ConnectorStatusMessage = message
            };
        }
    }
}
