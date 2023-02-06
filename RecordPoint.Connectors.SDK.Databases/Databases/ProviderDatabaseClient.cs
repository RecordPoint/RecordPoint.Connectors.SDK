using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases
{

    /// <summary>
    /// Database client that just redirects requests to a provider
    /// </summary>
    public class ProviderDatabaseClient<TDbContext, TDbProvider> : IDatabaseClient<TDbContext>
        where TDbContext : DbContext
        where TDbProvider : IDatabaseProvider<TDbContext>

    {
        public ProviderDatabaseClient(TDbProvider databaseProvider)
        {
            Provider = databaseProvider;
        }

        /// <summary>
        /// Underlying provider
        /// </summary>
        public TDbProvider Provider { get; }

        public TDbContext CreateDbContext()
        {
            return Provider.CreateDbContext();
        }


        public string GetExternalSystemName()
        {
            return Provider.GetExternalSystemName();
        }

        public Task ReadyAsync(CancellationToken cancellationToken)
        {
            return Provider.ReadyAsync(cancellationToken);
        }
    }
}
