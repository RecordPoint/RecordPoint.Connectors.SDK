using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Operational state for a Channel Discovery Operation
    /// </summary>
    public class ChannelDiscoveryState
    {
        /// <summary>
        /// The number of seconds the last execution was delayed by
        /// </summary>
        /// <remarks>
        /// Used for exponential backoff
        /// </remarks>
        public int LastBackOffDelaySeconds { get; set; } = 0;

        /// <summary>
        /// Latest version of the sync state type
        /// </summary>
        public static string LatestStateType => nameof(ChannelDiscoveryState);

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ChannelDiscoveryState? Deserialize(string stateType, string stateText)
        {
            if (string.IsNullOrEmpty(stateType))
                return new ChannelDiscoveryState();
            return JsonSerializer.Deserialize<ChannelDiscoveryState>(stateText);
        }
    }
}
