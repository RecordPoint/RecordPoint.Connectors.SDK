namespace RecordPoint.Connectors.SDK.Configuration.AzureKeyVault
{
    /// <summary>
    /// Azure key vault options
    /// </summary>
    public class AzureKeyVaultOptions
    {
        /// <summary>
        /// Configuration section name
        /// </summary>
        public const string SECTION_NAME = "AzureKeyVault";

        /// <summary>
        /// Key vault name
        /// </summary>
        public string? KeyVaultName { get; set; }

        /// <summary>
        /// Time in seconds to wait between attempts at polling the Azure Key Vault for changes.
        /// </summary>
        /// <remarks>Reloading cannot be disabled. If a value of 0 is provided, the timeout will default to 5 mins.</remarks>
        public int ReloadInterval { get; set; } = 300;
    }
}
