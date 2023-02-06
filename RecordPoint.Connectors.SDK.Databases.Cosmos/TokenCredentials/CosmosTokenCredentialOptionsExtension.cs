using Azure.Core;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.TokenCredentials
{
    public class CosmosTokenCredentialOptionsExtension : IDbContextOptionsExtension
    {
        private DbContextOptionsExtensionInfo? _info;

        public CosmosTokenCredentialOptionsExtension(TokenCredential? tokenCredential)
        {
            TokenCredential = tokenCredential;
        }

        public TokenCredential? TokenCredential { get; }

        public DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

        public void ApplyServices(IServiceCollection services)
        {
            new EntityFrameworkServicesBuilder(services)
                .TryAdd<ISingletonOptions, CosmosTokenCredentialSingletonOptions>(
                    sp => sp.GetRequiredService<CosmosTokenCredentialSingletonOptions>())
                .TryAddProviderSpecificServices(
                    sm => sm.TryAddSingleton(
                        sp => new CosmosTokenCredentialSingletonOptions()));
        }

        public void Validate(IDbContextOptions options)
        {
            // Do nothing.
        }

        private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
        {
            private int? _serviceProviderHash;

            public ExtensionInfo(IDbContextOptionsExtension extension)
                : base(extension)
            {
            }

            public override bool IsDatabaseProvider
                => true;

            public override string LogFragment => string.Empty;

            private new CosmosTokenCredentialOptionsExtension Extension
                => (CosmosTokenCredentialOptionsExtension)base.Extension;

            public override int GetServiceProviderHashCode()
            {
                if (_serviceProviderHash == null)
                {
#pragma warning disable SA1129 // Do not use default value type constructor
                    var hashCode = new HashCode();
#pragma warning restore SA1129 // Do not use default value type constructor
                    hashCode.Add(Extension.TokenCredential);

                    _serviceProviderHash = hashCode.ToHashCode();
                }

                return _serviceProviderHash.Value;
            }

            public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            {
                return other is ExtensionInfo otherInfo
                       && ReferenceEquals(Extension.TokenCredential, otherInfo.Extension.TokenCredential);
            }

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
                if (debugInfo == null)
                {
                    throw new ArgumentNullException(nameof(debugInfo));
                }

                debugInfo["Cosmos:" + nameof(Extension.TokenCredential)] =
                    (Extension.TokenCredential?.GetHashCode() ?? 0L).ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}