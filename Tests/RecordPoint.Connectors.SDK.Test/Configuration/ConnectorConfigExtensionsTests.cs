#nullable enable
using System;
using System.Text.Json;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Configuration
{
    public class ConnectorConfigExtensionsTests
    {
        [Fact]
        public void GetConsentAuthorizedOn_ReturnsNull_WhenNotSet()
        {
            var config = new ConnectorConfigModel();
            Assert.Null(config.GetConsentAuthorizedOn());
        }

        [Fact]
        public void SetAndGetConsentAuthorizedOn_RoundTrips()
        {
            var config = new ConnectorConfigModel();
            var value = new DateTimeOffset(2026, 5, 4, 3, 2, 1, TimeSpan.Zero);

            config.SetConsentAuthorizedOn(value);
            var result = config.GetConsentAuthorizedOn();

            Assert.NotNull(result);
            Assert.Equal(value, result!.Value);
        }

        [Fact]
        public void GetConsentAuthorizedOn_ReturnsNull_WhenUnparseable()
        {
            var config = new ConnectorConfigModel();
            config.SetProperty(ConnectorConfigExtensions.CONSENT_AUTHORIZED_ON_PROPERTY, "not-a-date");
            Assert.Null(config.GetConsentAuthorizedOn());
        }

        [Fact]
        public void ConvertToConnectorConfig_DeserializesData()
        {
            var original = new ConnectorConfigModel
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "My Connector",
                Status = "Enabled"
            };
            var data = new ConnectorConfigurationModel
            {
                Data = JsonSerializer.Serialize(original)
            };

            var result = data.ConvertToConnectorConfig();

            Assert.NotNull(result);
            Assert.Equal(original.Id, result.Id);
            Assert.Equal("My Connector", result.DisplayName);
        }

        [Fact]
        public void GetBinarySubmissionEnabled_PropertyNotSet_ReturnsNull()
        {
            var config = new ConnectorConfigModel();

            Assert.Null(config.GetBinarySubmissionEnabled());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetBinarySubmissionEnabled_PropertySet_ReturnsStoredValue(bool value)
        {
            var config = new ConnectorConfigModel();
            config.SetBinarySubmissionEnabled(value);

            Assert.Equal(value, config.GetBinarySubmissionEnabled());
        }

        [Fact]
        public void GetBinarySubmissionEnabled_PropertyNotParseable_ReturnsNull()
        {
            var config = new ConnectorConfigModel();
            config.SetProperty(ConnectorConfigExtensions.BINARY_SUBMISSION_ENABLED_PROPERTY, "not-a-bool");

            Assert.Null(config.GetBinarySubmissionEnabled());
        }
    }
}
