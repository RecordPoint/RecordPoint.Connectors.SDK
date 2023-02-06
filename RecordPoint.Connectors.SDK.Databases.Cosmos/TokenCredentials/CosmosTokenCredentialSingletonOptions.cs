using Azure.Core;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.TokenCredentials
{
    public class CosmosTokenCredentialSingletonOptions : ISingletonOptions
    {
        public virtual TokenCredential? TokenCredential { get; private set; }

        public void Initialize(IDbContextOptions options)
        {
            // ReSharper disable once UsePatternMatching
            var tokenCredentialOptions = options.FindExtension<CosmosTokenCredentialOptionsExtension>();
            if (tokenCredentialOptions != null)
            {
                TokenCredential = tokenCredentialOptions.TokenCredential;
            }
        }

        public void Validate(IDbContextOptions options)
        {
            // ReSharper disable once UsePatternMatching
#pragma warning disable CA1062 // Validate arguments of public methods
            var tokenCredentialOptions = options.FindExtension<CosmosTokenCredentialOptionsExtension>();
#pragma warning restore CA1062 // Validate arguments of public methods
            if (tokenCredentialOptions != null && TokenCredential != tokenCredentialOptions.TokenCredential)
            {
                throw new InvalidOperationException("Singleton options changed.");
            }
        }
    }
}
