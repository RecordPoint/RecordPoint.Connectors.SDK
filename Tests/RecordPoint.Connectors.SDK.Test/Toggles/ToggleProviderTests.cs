#nullable enable
using RecordPoint.Connectors.SDK.Toggles.Development;
using RecordPoint.Connectors.SDK.Toggles.Null;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Toggles
{
    public class DictionaryToggleProviderTests
    {
        [Fact]
        public void ReturnsDefault_WhenToggleMissing()
        {
            var provider = new DictionaryToggleProvider();
            Assert.True(provider.GetToggleBool("missing", true));
            Assert.False(provider.GetToggleBool("missing", "user", false));
            Assert.Equal(7, provider.GetToggleNumber("missing", 7));
            Assert.Equal(9, provider.GetToggleNumber("missing", "user", 9));
            Assert.Equal("def", provider.GetToggleString("missing", "def"));
            Assert.Equal("def2", provider.GetToggleString("missing", "user", "def2"));
        }

        [Fact]
        public void ReturnsStoredValues()
        {
            var provider = new DictionaryToggleProvider();
            provider.Toggles["flagBool"] = true;
            provider.Toggles["flagNum"] = 42;
            provider.Toggles["flagStr"] = "value";

            Assert.True(provider.GetToggleBool("flagBool", false));
            Assert.True(provider.GetToggleBool("flagBool", "user", false));
            Assert.Equal(42, provider.GetToggleNumber("flagNum", 0));
            Assert.Equal(42, provider.GetToggleNumber("flagNum", "user", 0));
            Assert.Equal("value", provider.GetToggleString("flagStr"));
            Assert.Equal("value", provider.GetToggleString("flagStr", "user"));
        }
    }

    public class NullToggleProviderTests
    {
        [Fact]
        public void AlwaysReturnsDefault()
        {
            var provider = new NullToggleProvider();
            Assert.True(provider.GetToggleBool("x", true));
            Assert.False(provider.GetToggleBool("x", false));
            Assert.True(provider.GetToggleBool("x", "user", true));
            Assert.Equal(5, provider.GetToggleNumber("x", 5));
            Assert.Equal(6, provider.GetToggleNumber("x", "user", 6));
            Assert.Equal("d", provider.GetToggleString("x", "d"));
            Assert.Equal("d2", provider.GetToggleString("x", "user", "d2"));
            Assert.Null(provider.GetToggleString("x"));
        }
    }
}
