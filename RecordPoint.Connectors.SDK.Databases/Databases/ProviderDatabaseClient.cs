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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseProvider"></param>
        public ProviderDatabaseClient(TDbProvider databaseProvider)
        {
            Provider = databaseProvider;
        }

        /// <summary>
        /// Underlying provider
        /// </summary>
        public TDbProvider Provider { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TDbContext CreateDbContext()
        {
            return Provider.CreateDbContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetExternalSystemName()
        {
            return Provider.GetExternalSystemName();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ReadyAsync(CancellationToken cancellationToken)
        {
            return Provider.ReadyAsync(cancellationToken);
        }
    }
}
