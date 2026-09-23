#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using RecordPoint.Connectors.SDK.Configuration.AzureKeyVault;
using Xunit;

namespace RecordPoint.Connectors.SDK.Configuration.AzureKeyVault.Test
{
    public class AzureKeyVaultConfigurationBuilderExtensionsTests
    {
        private static ConfigurationBuilder BuilderWith(Dictionary<string, string?> values)
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(values);
            return builder;
        }

        private static IConfigurationSource? FindKeyVaultSource(IConfigurationBuilder builder)
        {
            return builder.Sources.FirstOrDefault(s =>
                s.GetType().Name.Contains("AzureKeyVault", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Recursively searches the object graph for a TimeSpan member named "ReloadInterval".
        /// The AzureKeyVault configuration source stores the reload interval internally, so
        /// reflection lets us verify the interval the extension method actually configured.
        /// </summary>
        private static TimeSpan? FindReloadInterval(object? root)
        {
            if (root == null)
                return null;

            var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
            var queue = new Queue<object>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (!visited.Add(current))
                    continue;

                var type = current.GetType();

                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (prop.GetIndexParameters().Length != 0)
                        continue;

                    object? value;
                    try
                    {
                        value = prop.GetValue(current);
                    }
                    catch
                    {
                        continue;
                    }

                    if (value == null)
                        continue;

                    if (prop.Name == "ReloadInterval" && value is TimeSpan ts)
                        return ts;

                    EnqueueIfComposite(value, queue);
                }

                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    var value = field.GetValue(current);
                    if (value == null)
                        continue;

                    if (value is TimeSpan tsField &&
                        field.Name.Contains("ReloadInterval", StringComparison.OrdinalIgnoreCase))
                        return tsField;

                    EnqueueIfComposite(value, queue);
                }
            }

            return null;
        }

        private static void EnqueueIfComposite(object value, Queue<object> queue)
        {
            var valueType = value.GetType();
            if (valueType.IsPrimitive || value is string || value is TimeSpan || value is Uri)
                return;
            // Only recurse into types defined in the Azure configuration/secrets assemblies to keep the search bounded.
            var asmName = valueType.Assembly.GetName().Name ?? string.Empty;
            if (asmName.StartsWith("Azure", StringComparison.OrdinalIgnoreCase))
                queue.Enqueue(value);
        }

        [Fact]
        public void NoKeyVaultSection_ReturnsSameBuilderUnchanged()
        {
            var builder = BuilderWith(new Dictionary<string, string?>());
            var sourceCountBefore = builder.Sources.Count;

            var result = builder.UseAzureKeyVaultConfigurationProvider();

            Assert.Same(builder, result);
            Assert.Equal(sourceCountBefore, builder.Sources.Count);
            Assert.Null(FindKeyVaultSource(builder));
        }

        [Fact]
        public void RootSectionProvided_ButNoKeyVaultSection_ReturnsUnchanged()
        {
            var builder = BuilderWith(new Dictionary<string, string?>
            {
                ["SomethingElse:Value"] = "x"
            });
            var sourceCountBefore = builder.Sources.Count;

            var result = builder.UseAzureKeyVaultConfigurationProvider("MyRoot");

            Assert.Same(builder, result);
            Assert.Equal(sourceCountBefore, builder.Sources.Count);
            Assert.Null(FindKeyVaultSource(builder));
        }

        [Fact]
        public void WithKeyVaultSection_AddsKeyVaultConfigurationSource()
        {
            var builder = BuilderWith(new Dictionary<string, string?>
            {
                ["AzureKeyVault:KeyVaultName"] = "my-vault"
            });

            var result = builder.UseAzureKeyVaultConfigurationProvider();

            Assert.Same(builder, result);
            Assert.NotNull(FindKeyVaultSource(builder));
        }

        [Fact]
        public void WithKeyVaultSection_CustomReloadInterval_UsesConfiguredSeconds()
        {
            var builder = BuilderWith(new Dictionary<string, string?>
            {
                ["AzureKeyVault:KeyVaultName"] = "my-vault",
                ["AzureKeyVault:ReloadInterval"] = "120"
            });

            builder.UseAzureKeyVaultConfigurationProvider();

            var source = FindKeyVaultSource(builder);
            Assert.NotNull(source);
            var interval = FindReloadInterval(source);
            Assert.NotNull(interval);
            Assert.Equal(TimeSpan.FromSeconds(120), interval);
        }

        [Fact]
        public void WithKeyVaultSection_ZeroReloadInterval_DefaultsToFiveMinutes()
        {
            var builder = BuilderWith(new Dictionary<string, string?>
            {
                ["AzureKeyVault:KeyVaultName"] = "my-vault",
                ["AzureKeyVault:ReloadInterval"] = "0"
            });

            builder.UseAzureKeyVaultConfigurationProvider();

            var source = FindKeyVaultSource(builder);
            Assert.NotNull(source);
            var interval = FindReloadInterval(source);
            Assert.NotNull(interval);
            Assert.Equal(TimeSpan.FromMinutes(5), interval);
        }

        [Fact]
        public void WithRootSection_ReadsNestedKeyVaultAndAuthSections_AddsSource()
        {
            var builder = BuilderWith(new Dictionary<string, string?>
            {
                ["MyRoot:AzureKeyVault:KeyVaultName"] = "nested-vault",
                ["MyRoot:AzureKeyVault:ReloadInterval"] = "60",
                ["MyRoot:AzureAuthentication:TenantId"] = "tenant",
                ["MyRoot:AzureAuthentication:ClientId"] = "client",
                ["MyRoot:AzureAuthentication:ClientSecret"] = "secret"
            });

            var result = builder.UseAzureKeyVaultConfigurationProvider("MyRoot");

            Assert.Same(builder, result);
            Assert.NotNull(FindKeyVaultSource(builder));
        }

        [Fact]
        public void WithAuthenticationSection_UseVsCredentials_AddsSource()
        {
            var builder = BuilderWith(new Dictionary<string, string?>
            {
                ["AzureKeyVault:KeyVaultName"] = "my-vault",
                ["AzureAuthentication:UseVsCredentials"] = "true"
            });

            var result = builder.UseAzureKeyVaultConfigurationProvider();

            Assert.Same(builder, result);
            Assert.NotNull(FindKeyVaultSource(builder));
        }
    }
}
