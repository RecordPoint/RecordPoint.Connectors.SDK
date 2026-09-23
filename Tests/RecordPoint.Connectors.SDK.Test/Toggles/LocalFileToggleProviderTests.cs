#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Toggles
{
    public class FileReaderTests
    {
        [Fact]
        public void ReadAllText_ReturnsFileContents()
        {
            var path = Path.GetTempFileName();
            try
            {
                File.WriteAllText(path, "hello world");
                var reader = new FileReader();
                Assert.Equal("hello world", reader.ReadAllText(path));
            }
            finally
            {
                File.Delete(path);
            }
        }
    }

    public class LocalFileToggleProviderTests
    {
        private static IOptions<LocalFeatureToggleOptions> OptionsFor(string path)
            => Options.Create(new LocalFeatureToggleOptions { JsonFilePath = path });

        private static string SerializeToggles(Dictionary<string, FeatureToggleModel> toggles)
            => JsonConvert.SerializeObject(toggles);

        private static Mock<IFileReader> ReaderReturning(string json)
        {
            var mock = new Mock<IFileReader>();
            mock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(json);
            return mock;
        }

        [Fact]
        public void EmptyJsonFilePath_UsesEmptyToggleSet()
        {
            var reader = new Mock<IFileReader>();
            var provider = new LocalFileToggleProvider(OptionsFor(string.Empty), reader.Object);

            Assert.True(provider.GetToggleBool("anything", true));
            Assert.False(provider.GetToggleBool("anything", false));
            reader.Verify(x => x.ReadAllText(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GetToggleBool_ReturnsStoredValue_OrDefault()
        {
            var json = SerializeToggles(new Dictionary<string, FeatureToggleModel>
            {
                ["enabled"] = new FeatureToggleModel { Value = true }
            });
            var provider = new LocalFileToggleProvider(OptionsFor("toggles.json"), ReaderReturning(json).Object);

            Assert.True(provider.GetToggleBool("enabled", false));
            Assert.True(provider.GetToggleBool("missing", true));
            Assert.False(provider.GetToggleBool("missing", false));
        }

        [Fact]
        public void GetToggleBool_WithUserKey_HonoursOverridesAndDefaults()
        {
            var json = SerializeToggles(new Dictionary<string, FeatureToggleModel>
            {
                ["feature"] = new FeatureToggleModel
                {
                    Value = false,
                    UserKeyOverrides = new Dictionary<string, bool> { ["vip"] = true }
                }
            });
            var provider = new LocalFileToggleProvider(OptionsFor("toggles.json"), ReaderReturning(json).Object);

            // User has an override
            Assert.True(provider.GetToggleBool("feature", "vip", false));
            // User has no override -> falls back to the toggle Value
            Assert.False(provider.GetToggleBool("feature", "other", true));
            // Toggle missing -> default
            Assert.True(provider.GetToggleBool("missing", "vip", true));
        }

        [Fact]
        public void GetToggleBool_WhenReReadFails_FallsBackToCachedToggles()
        {
            var json = SerializeToggles(new Dictionary<string, FeatureToggleModel>
            {
                ["enabled"] = new FeatureToggleModel { Value = true }
            });
            var reader = new Mock<IFileReader>();
            reader.SetupSequence(x => x.ReadAllText(It.IsAny<string>()))
                .Returns(json)                                  // constructor read succeeds
                .Throws(new IOException("file locked"));         // subsequent read fails

            var provider = new LocalFileToggleProvider(OptionsFor("toggles.json"), reader.Object);

            // The re-read throws, so the cached toggle dictionary should be used.
            Assert.True(provider.GetToggleBool("enabled", false));
        }

        [Fact]
        public void NullJsonContent_ResultsInEmptyToggleSet()
        {
            var provider = new LocalFileToggleProvider(OptionsFor("toggles.json"), ReaderReturning("null").Object);
            Assert.True(provider.GetToggleBool("enabled", true));
        }

        [Fact]
        public void UnsupportedToggleMethods_Throw()
        {
            var provider = new LocalFileToggleProvider(OptionsFor(string.Empty), new Mock<IFileReader>().Object);

            Assert.Throws<NotImplementedException>(() => provider.GetToggleNumber("x", 1));
            Assert.Throws<NotImplementedException>(() => provider.GetToggleNumber("x", "user", 1));
            Assert.Throws<NotImplementedException>(() => provider.GetToggleString("x"));
            Assert.Throws<NotImplementedException>(() => provider.GetToggleString("x", "user"));
        }
    }
}
