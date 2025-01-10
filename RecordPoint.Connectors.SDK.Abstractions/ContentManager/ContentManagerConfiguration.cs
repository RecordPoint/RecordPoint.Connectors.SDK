using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Content Manager operation configuration
    /// </summary>
    public class ContentManagerConfiguration
    {
        /// <summary>
        /// Gets the configuration type.
        /// </summary>
        public static string ConfigurationType => nameof(ContentManagerConfiguration);

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
        public static ContentManagerConfiguration? Deserialize(string configurationType, string configurationText)
        {
            if (configurationType != nameof(ContentManagerConfiguration))
                throw new ArgumentOutOfRangeException(nameof(configurationType));

            return JsonSerializer.Deserialize<ContentManagerConfiguration>(configurationText);
        }

    }
}
