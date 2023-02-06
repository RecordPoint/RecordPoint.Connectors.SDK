using Microsoft.EntityFrameworkCore.Cosmos.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Cosmos.Storage.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.TokenCredentials
{
    public static class CosmosEntityFrameworkDIExtensions
    {
        public static DbContextOptionsBuilder<TDbContext> UseCosmos<TDbContext>(
            this DbContextOptionsBuilder<TDbContext> optionsBuilder,
            string accountEndpoint,
            TokenCredential tokenCredential,
            string databaseName,
            Action<CosmosDbContextOptionsBuilder>? cosmosOptionsAction = null)
            where TDbContext : DbContext
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            if (tokenCredential == null)
            {
                throw new ArgumentNullException(nameof(tokenCredential));
            }

            var extension = optionsBuilder.Options.FindExtension<CosmosOptionsExtension>() ?? new CosmosOptionsExtension();
            extension = extension
                .WithAccountEndpoint(accountEndpoint)
                .WithAccountKey("__TokenCredential__")
                .WithDatabaseName(databaseName);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            var tokenCredentialExtension = new CosmosTokenCredentialOptionsExtension(tokenCredential);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(tokenCredentialExtension);

            cosmosOptionsAction?.Invoke(new CosmosDbContextOptionsBuilder(optionsBuilder));

            // ------------------- Substitution is happening HERE --------------------------
            optionsBuilder.ReplaceService<ISingletonCosmosClientWrapper, CosmosClientWrapperWithTokensSupport>();
            return optionsBuilder;
        }
    }
}
