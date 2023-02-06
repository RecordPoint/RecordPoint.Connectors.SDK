namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// Connector database client
    /// </summary>
    public class ConnectorDatabaseClient : ProviderDatabaseClient<ConnectorDbContext, IConnectorDatabaseProvider>,
        IConnectorDatabaseClient
    {
        public ConnectorDatabaseClient(IConnectorDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }
}