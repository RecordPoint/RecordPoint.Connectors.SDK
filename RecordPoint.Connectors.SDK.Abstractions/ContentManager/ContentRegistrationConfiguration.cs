using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Content Registration operation configuration
    /// </summary>
    public class ContentRegistrationConfiguration : ContentSubmissionConfiguration
    {
        /// <summary>
        /// External id for the Channel we are syncing content for
        /// </summary>
        public string? ChannelExternalId { get; set; }

        /// <summary>
        /// Channel title
        /// </summary>
        public string? ChannelTitle { get; set; }

        /// <summary>
        /// Context for the Content Registration
        /// </summary>
        public IDictionary<string, string> Context { get; set; } = new Dictionary<string, string>();

        public static string ConfigurationType => nameof(ContentRegistrationConfiguration);

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ContentRegistrationConfiguration? Deserialize(string configurationType, string configurationText)
        {
            if (configurationType != nameof(ContentRegistrationConfiguration))
                throw new ArgumentOutOfRangeException(nameof(configurationType));

            return JsonSerializer.Deserialize<ContentRegistrationConfiguration>(configurationText);
        }

    }
}
