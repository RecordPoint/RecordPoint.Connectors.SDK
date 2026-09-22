using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    public class ConnectorNotificationModelExtensionsTest
    {
        [Fact]
        public void ToAcknowledge_MapsFields_AndUsesDefaultEmptyMessage()
        {
            var model = new ConnectorNotificationModel
            {
                ConnectorId = "connector-1",
                Id = "notification-1"
            };

            var result = model.ToAcknowledge(ProcessingResult.OK);

            Assert.Equal("connector-1", result.ConnectorId);
            Assert.Equal("notification-1", result.NotificationId);
            Assert.Equal(nameof(ProcessingResult.OK), result.ProcessingResult);
            Assert.Empty(result.ConnectorStatusMessage);
        }

        [Fact]
        public void ToAcknowledge_UsesSuppliedMessage_AndProcessingResult()
        {
            var model = new ConnectorNotificationModel
            {
                ConnectorId = "connector-2",
                Id = "notification-2"
            };

            var result = model.ToAcknowledge(ProcessingResult.NotificationError, "something failed");

            Assert.Equal(nameof(ProcessingResult.NotificationError), result.ProcessingResult);
            Assert.Equal("something failed", result.ConnectorStatusMessage);
        }
    }
}
