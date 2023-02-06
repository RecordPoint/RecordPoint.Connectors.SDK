﻿using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Operational state for a Content Synchronisation Operation
    /// </summary>
    public class ContentSynchronisationState
    {
        /// <summary>
        /// The number of seconds the last execution was delayed by
        /// </summary>
        /// <remarks>
        /// Used for exponential backoff
        /// </remarks>
        public int LastBackOffDelaySeconds { get; set; } = 0;

        /// <summary>
        /// Current position in syncing
        /// </summary>
        public string Cursor { get; set; } = string.Empty;

        /// <summary>
        /// Latest version of the synchronisation state type
        /// </summary>
        public static string LatestStateType => nameof(ContentSynchronisationState);

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ContentSynchronisationState? Deserialize(string stateType, string stateText)
        {
            if (string.IsNullOrEmpty(stateType))
                return new ContentSynchronisationState();
            return JsonSerializer.Deserialize<ContentSynchronisationState>(stateText);
        }
    }
}