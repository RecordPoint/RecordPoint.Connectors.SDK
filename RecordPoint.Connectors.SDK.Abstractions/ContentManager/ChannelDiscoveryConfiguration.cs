using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Channel discovery operation configuration
    /// </summary>
    public class ChannelDiscoveryConfiguration
    {
        /// <summary>
        /// Id of the Connector we are performing the operation on. Content modules can use this to identify the target content 
        /// </summary>
        public string? ConnectorConfigurationId { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public string? TenantId { get; set; }

        /// <summary>
        /// Tenant name
        /// </summary>
        public string? TenantDomainName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string ConfigurationType => nameof(ChannelDiscoveryConfiguration);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationType"></param>
        /// <param name="configurationText"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static ChannelDiscoveryConfiguration? Deserialize(string configurationType, string configurationText)
        {
            if (configurationType != nameof(ChannelDiscoveryConfiguration))
                throw new ArgumentOutOfRangeException(nameof(configurationType));

            return JsonSerializer.Deserialize<ChannelDiscoveryConfiguration>(configurationText);
        }

    }
}
