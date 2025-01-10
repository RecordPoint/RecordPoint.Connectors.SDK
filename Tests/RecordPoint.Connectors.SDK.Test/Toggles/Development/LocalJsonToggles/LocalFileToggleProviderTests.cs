using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Toggles.Development.LocalJsonToggles
{
    public class LocalFileToggleProviderTests
    {
        private readonly string _mockJson = @"
{
  ""TrueToggle"": {
    ""Value"": true,
    ""UserKeyOverrides"": {
      ""Tenant"": false
    }
  },
  ""FalseToggle"": {
    ""Value"": false,
    ""UserKeyOverrides"": {
      ""Tenant"": true
    }
  }
}";

        private readonly string _flippedMockJson = @"
{
  ""TrueToggle"": {
    ""Value"": false,
    ""UserKeyOverrides"": {
      ""Tenant"": false
    }
  },
  ""FalseToggle"": {
    ""Value"": true,
    ""UserKeyOverrides"": {
      ""Tenant"": true
    }
  }
}";

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetToggleValue_WhenToggleExists_ReturnsTrueValue(bool defaultValue)
        {
            // Arrange
            var options = new LocalFeatureToggleOptions
            {
                JsonFilePath = "test.json"
            };
            var mockFileReader = new Mock<IFileReader>();
            // return a json string matching featureToggles.json
            mockFileReader.Setup(x => x.ReadAllText("test.json")).Returns(_mockJson);
            var toggleProvider = new LocalFileToggleProvider(Options.Create(options), mockFileReader.Object);

            // Act
            var result = toggleProvider.GetToggleBool("TrueToggle", defaultValue);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetToggleValue_WhenToggleExists_ReturnsFalseValue(bool defaultValue)
        {
            // Arrange
            var options = new LocalFeatureToggleOptions
            {
                JsonFilePath = "test.json"
            };
            var mockFileReader = new Mock<IFileReader>();
            // return a json string matching featureToggles.json
            mockFileReader.Setup(x => x.ReadAllText("test.json")).Returns(_mockJson);
            var toggleProvider = new LocalFileToggleProvider(Options.Create(options), mockFileReader.Object);

            // Act
            var result = toggleProvider.GetToggleBool("FalseToggle", defaultValue);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetToggleValue_WhenToggleDoesNotExist_ReturnsDefaultValue(bool defaultValue)
        {
            // Arrange
            var options = new LocalFeatureToggleOptions
            {
                JsonFilePath = "test.json"
            };
            var mockFileReader = new Mock<IFileReader>();
            // return a json string matching featureToggles.json
            mockFileReader.Setup(x => x.ReadAllText("test.json")).Returns(_mockJson);
            var toggleProvider = new LocalFileToggleProvider(Options.Create(options), mockFileReader.Object);

            // Act
            var result = toggleProvider.GetToggleBool("NonExistentToggle", defaultValue);

            // Assert
            Assert.Equal(defaultValue, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetToggleValue_WhenUserKeyOverrideExists_ReturnsFalseUserKeyOverrideValue(bool defaultValue)
        {
            // Arrange
            var options = new LocalFeatureToggleOptions
            {
                JsonFilePath = "test.json"
            };
            var mockFileReader = new Mock<IFileReader>();
            // return a json string matching featureToggles.json
            mockFileReader.Setup(x => x.ReadAllText("test.json")).Returns(_mockJson);
            var toggleProvider = new LocalFileToggleProvider(Options.Create(options), mockFileReader.Object);

            // Act
            var result = toggleProvider.GetToggleBool("TrueToggle", "Tenant", defaultValue);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetToggleValue_WhenUserKeyOverrideExists_ReturnsTrueUserKeyOverrideValue(bool defaultValue)
        {
            // Arrange
            var options = new LocalFeatureToggleOptions
            {
                JsonFilePath = "test.json"
            };
            var mockFileReader = new Mock<IFileReader>();
            // return a json string matching featureToggles.json
            mockFileReader.Setup(x => x.ReadAllText("test.json")).Returns(_mockJson);
            var toggleProvider = new LocalFileToggleProvider(Options.Create(options), mockFileReader.Object);

            // Act
            var result = toggleProvider.GetToggleBool("FalseToggle", "Tenant", defaultValue);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetToggleValueWorksWhenJsonChanges()
        {
            // Arrange
            var options = new LocalFeatureToggleOptions
            {
                JsonFilePath = "test.json"
            };
            var mockFileReader = new Mock<IFileReader>();
            // return a json string matching featureToggles.json
            mockFileReader.Setup(x => x.ReadAllText("test.json")).Returns(_mockJson);
            var toggleProvider = new LocalFileToggleProvider(Options.Create(options), mockFileReader.Object);

            // Act
            var result = toggleProvider.GetToggleBool("TrueToggle", true);

            // Assert
            Assert.True(result);

            // Arrange
            //This represents a change in the TrueToggle value in the json file
            mockFileReader.Setup(x => x.ReadAllText("test.json")).Returns(_flippedMockJson);
            // Act
            var changedResult = toggleProvider.GetToggleBool("TrueToggle", true);

            // Assert
            Assert.False(changedResult);
        }
    }
}
