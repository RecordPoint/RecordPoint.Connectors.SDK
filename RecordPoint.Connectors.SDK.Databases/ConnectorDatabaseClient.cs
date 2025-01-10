namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// Connector database client
    /// </summary>
    public class ConnectorDatabaseClient : ProviderDatabaseClient<ConnectorDbContext, IConnectorDatabaseProvider>,
        IConnectorDatabaseClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseProvider"></param>
        public ConnectorDatabaseClient(IConnectorDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }
}