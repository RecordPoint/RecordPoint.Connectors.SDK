using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Content Manager operation configuration
    /// </summary>
    public class ContentManagerConfiguration
    {
        public static string ConfigurationType => nameof(ContentManagerConfiguration);

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ContentManagerConfiguration? Deserialize(string configurationType, string configurationText)
        {
            if (configurationType != nameof(ContentManagerConfiguration))
                throw new ArgumentOutOfRangeException(nameof(configurationType));

            return JsonSerializer.Deserialize<ContentManagerConfiguration>(configurationText);
        }

    }
}
