namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// Defines the connector configuration manager which is used to access stored connector configurations
    /// </summary>
    public interface IConnectorConfigurationManager
    {
        /// <summary>
        /// Does a connector configuration exist?
        /// </summary>
        /// <param name="connectorId">Connector Configuration Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the secret exists, otherwise false</returns>
        Task<bool> ConnectorConfigurationExistsAsync(string connectorId, CancellationToken cancellationToken);

        /// <summary>
        /// Get a connect configuration
        /// </summary>
        /// <param name="connectorId">Connector Configuration Id</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Connector data model</returns>
        Task<ConnectorConfigurationModel?> GetConnectorConfigurationAsync(string connectorId, CancellationToken cancellationToken);

        /// <summary>
        /// Set connector configuration
        /// </summary>
        /// <param name="connectorData">Connector data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task SetConnectorConfigurationAsync(ConnectorConfigurationModel connectorData, CancellationToken cancellationToken);

        /// <summary>
        /// Delete connector configuration
        /// </summary>
        /// <param name="connectorId">Id of connector configuration to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task DeleteConnectorConfigurationAsync(string connectorId, CancellationToken cancellationToken);

        /// <summary>
        /// Get all connector configurations
        /// </summary>
        /// <returns>List of Connector Configuration models</returns>
        Task<List<ConnectorConfigurationModel>> GetAllConnectorConfigurationsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get the connector status
        /// </summary>
        /// <param name="connectorId">Id of the connector to get the status for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Connector status. Always returns a value, even if the connector doesn't exist.</returns>
        Task<ConnectorFeatureStatus> GetConnectorStatusAsync(string connectorId, CancellationToken cancellationToken);

        /// <summary>
        /// Get the submission feature status
        /// </summary>
        /// <param name="connectorId">Id of the connector to get the submission feature status for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Submission feature status. Always returns a value, even if the connector doesn't exist.</returns>
        Task<ConnectorFeatureStatus> GetSubmissionStatusAsync(string connectorId, CancellationToken cancellationToken);

        /// <summary>
        /// Get the binary submission feature status
        /// </summary>
        /// <param name="connectorId">ID of the connector to get the status for</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ConnectorFeatureStatus> GetBinarySubmissionStatusAsync(string connectorId, CancellationToken cancellationToken);
    }
}