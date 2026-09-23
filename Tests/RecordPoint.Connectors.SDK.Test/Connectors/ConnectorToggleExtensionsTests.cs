using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Toggles;
using System;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Connectors
{
    /// <summary>
    /// Tests for the connector toggle extensions, focused on the binary submission decision.
    /// </summary>
    public class ConnectorToggleExtensionsTests
    {
        private const string FeatureFlag = "Enable-Config-Driven-Binary-Submission";

        private static ConnectorConfigModel NewConfig() => new() { TenantId = Guid.NewGuid().ToString() };

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldSubmitBinaries_FeatureOn_UsesConfigProperty(bool storedValue)
        {
            var config = NewConfig();
            config.SetBinarySubmissionEnabled(storedValue);

            var toggle = new Mock<IToggleProvider>();
            toggle.Setup(t => t.GetToggleBool(FeatureFlag, config.TenantId, false)).Returns(true);
            var context = new Mock<ISystemContext>();

            var result = toggle.Object.ShouldSubmitBinaries(context.Object, config);

            Assert.Equal(storedValue, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldSubmitBinaries_FeatureOn_PropertyAbsent_UsesContentProtectionToggle(bool toggleValue)
        {
            // Feature is on, but the connector has no BinarySubmissionEnabled property (no switcher),
            // so it must fall back to the legacy content-protection toggle.
            var config = NewConfig();

            var context = new Mock<ISystemContext>();
            context.Setup(c => c.GetConnectorName()).Returns("Test");

            var toggle = new Mock<IToggleProvider>();
            toggle.Setup(t => t.GetToggleBool(FeatureFlag, config.TenantId, false)).Returns(true);
            toggle.Setup(t => t.GetToggleBool("Test-Content-Protection", config.TenantId, true)).Returns(toggleValue);

            var result = toggle.Object.ShouldSubmitBinaries(context.Object, config);

            Assert.Equal(toggleValue, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldSubmitBinaries_FeatureOff_UsesContentProtectionToggle(bool toggleValue)
        {
            var config = NewConfig();
            // Config says the opposite, to prove it is ignored while the feature is off.
            config.SetBinarySubmissionEnabled(!toggleValue);

            var context = new Mock<ISystemContext>();
            context.Setup(c => c.GetConnectorName()).Returns("Test");

            var toggle = new Mock<IToggleProvider>();
            toggle.Setup(t => t.GetToggleBool(FeatureFlag, config.TenantId, false)).Returns(false);
            toggle.Setup(t => t.GetToggleBool("Test-Content-Protection", config.TenantId, true)).Returns(toggleValue);

            var result = toggle.Object.ShouldSubmitBinaries(context.Object, config);

            Assert.Equal(toggleValue, result);
        }
    }
}
