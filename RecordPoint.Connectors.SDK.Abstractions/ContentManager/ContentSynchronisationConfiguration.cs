﻿using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Content Synchronisation operation configuration
    /// </summary>
    public class ContentSynchronisationConfiguration : ContentSubmissionConfiguration
    {
        /// <summary>
        /// External id for the Channel we are syncing content for
        /// </summary>
        public string? ChannelExternalId { get; set; }

        /// <summary>
        /// Channel title
        /// </summary>
        public string? ChannelTitle { get; set; }

        public static string ConfigurationType => $"{nameof(ContentSynchronisationConfiguration)}";

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ContentSynchronisationConfiguration? Deserialize(string configurationType, string configurationText)
        {
            if (configurationType != ConfigurationType) throw new ArgumentOutOfRangeException(nameof(configurationType));
            return JsonSerializer.Deserialize<ContentSynchronisationConfiguration>(configurationText);
        }
    }
}