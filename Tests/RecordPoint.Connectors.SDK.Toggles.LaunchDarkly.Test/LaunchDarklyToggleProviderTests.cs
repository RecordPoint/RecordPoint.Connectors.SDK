#nullable enable
using LDContext = LaunchDarkly.Sdk.Context;
using Microsoft.Extensions.Options;
using System.Reflection;
using Xunit;

namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.Test
{
    public class LaunchDarklyToggleProviderTests
    {
        private const string Toggle = "my-toggle";
        private const string UserKey = "user-123";
        private const string SystemUserId = "00000000-0000-0000-0000-000000000000";

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetToggleBool_NonTenanted_ReturnsDefaultWhenToggleCannotBeResolved(bool @default)
        {
            var sut = CreateSut();

            var result = sut.GetToggleBool(Toggle, @default);

            Assert.Equal(@default, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetToggleBool_Tenanted_ReturnsDefaultWhenToggleCannotBeResolved(bool @default)
        {
            var sut = CreateSut();

            var result = sut.GetToggleBool(Toggle, UserKey, @default);

            Assert.Equal(@default, result);
        }

        [Fact]
        public void GetToggleNumber_NonTenanted_ReturnsDefaultWhenToggleCannotBeResolved()
        {
            var sut = CreateSut();

            var result = sut.GetToggleNumber(Toggle, 7);

            Assert.Equal(7, result);
        }

        [Fact]
        public void GetToggleNumber_Tenanted_ReturnsDefaultWhenToggleCannotBeResolved()
        {
            var sut = CreateSut();

            var result = sut.GetToggleNumber(Toggle, UserKey, 11);

            Assert.Equal(11, result);
        }

        [Fact]
        public void GetToggleString_NonTenanted_ReturnsProvidedDefaultWhenToggleCannotBeResolved()
        {
            var sut = CreateSut();

            var result = sut.GetToggleString(Toggle, "fallback");

            Assert.Equal("fallback", result);
        }

        [Fact]
        public void GetToggleString_NonTenanted_ReturnsNullWhenDefaultIsNull()
        {
            var sut = CreateSut();

            var result = sut.GetToggleString(Toggle);

            Assert.Null(result);
        }

        [Fact]
        public void GetToggleString_Tenanted_ReturnsProvidedDefaultWhenToggleCannotBeResolved()
        {
            var sut = CreateSut();

            var result = sut.GetToggleString(Toggle, UserKey, "tenant-fallback");

            Assert.Equal("tenant-fallback", result);
        }

        [Fact]
        public void GetToggleString_Tenanted_ReturnsNullWhenDefaultIsNull()
        {
            var sut = CreateSut();

            var result = sut.GetToggleString(Toggle, UserKey, null);

            Assert.Null(result);
        }

        [Fact]
        public void DefaultContext_UsesConfiguredDefaultUserKey()
        {
            var sut = CreateSut(defaultUserKey: "configured-user");

            var context = InvokeDefaultContext(sut);

            Assert.Equal("configured-user", context.Key);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DefaultContext_UsesSystemUserIdWhenDefaultUserKeyIsMissing(string? defaultUserKey)
        {
            var sut = CreateSut(defaultUserKey);

            var context = InvokeDefaultContext(sut);

            Assert.Equal(SystemUserId, context.Key);
        }

        private static LaunchDarklyToggleProvider CreateSut(string? defaultUserKey = "default-user")
        {
            var options = Options.Create(new LaunchDarklyOptions
            {
                SdkKey = "invalid-sdk-key",
                DefaultUserKey = defaultUserKey ?? string.Empty
            });

            return new LaunchDarklyToggleProvider(options);
        }

        private static LDContext InvokeDefaultContext(LaunchDarklyToggleProvider sut)
        {
            var defaultContextMethod = typeof(LaunchDarklyToggleProvider)
                .GetMethod("DefaultContext", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.NotNull(defaultContextMethod);

            var context = defaultContextMethod!.Invoke(sut, null);

            return Assert.IsType<LDContext>(context);
        }
    }
}
