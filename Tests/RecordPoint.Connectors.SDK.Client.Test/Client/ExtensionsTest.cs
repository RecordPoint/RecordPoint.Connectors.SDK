using RecordPoint.Connectors.SDK.Client;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    public class ExtensionsTest
    {
        [Fact]
        public void TryAddQueryParameter_AddsFormattedAndEscapedValue_WhenNotNull()
        {
            var parameters = new List<string>();

            parameters.TryAddQueryParameter("a b&c", "Name={0}");

            Assert.Single(parameters);
            Assert.Equal("Name=a%20b%26c", parameters[0]);
        }

        [Fact]
        public void TryAddQueryParameter_DoesNothing_WhenValueNull()
        {
            var parameters = new List<string>();

            parameters.TryAddQueryParameter(null, "Name={0}");

            Assert.Empty(parameters);
        }
    }
}
