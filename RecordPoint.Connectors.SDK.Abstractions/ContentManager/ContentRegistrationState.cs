using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Operational state for a Content Registration Operation
    /// </summary>
    public class ContentRegistrationState
    {
        /// <summary>
        /// The number of seconds the last execution was delayed by
        /// </summary>
        /// <remarks>
        /// Used for exponential backoff
        /// </remarks>
        public int LastBackOffDelaySeconds { get; set; } = 0;

        /// <summary>
        /// Current position in registration
        /// </summary>
        public string Cursor { get; set; } = string.Empty;

        /// <summary>
        /// Latest version of the synchronisation state type
        /// </summary>
        public static string LatestStateType => nameof(ContentRegistrationState);

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ContentRegistrationState? Deserialize(string stateType, string stateText)
        {
            if (string.IsNullOrEmpty(stateType))
                return new ContentRegistrationState();
            return JsonSerializer.Deserialize<ContentRegistrationState>(stateText);
        }
    }
}