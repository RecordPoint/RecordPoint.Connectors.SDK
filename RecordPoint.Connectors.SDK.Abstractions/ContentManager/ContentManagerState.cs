using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Operational state for a Content Manager Operation
    /// </summary>
    public class ContentManagerState
    {
        /// <summary>
        /// The number of Channel Discovery Operations that have been started
        /// </summary>
        public int ChannelDiscoveryOperationsStarted { get; set; } = 0;


        /// <summary>
        /// The number of Content Synchronisation Operations that have been started
        /// </summary>
        /// <remarks>
        /// This is the number of operations that have been started via correction of the known channels versus running Content Synchronisation Operations
        /// and excludes Content Synchronisation Operations that have been started via the Channel Discovery Operation
        /// </remarks>
        public int ContentSynchronisationOperationsStarted { get; set; } = 0;

        /// <summary>
        /// Latest version of the sync state type
        /// </summary>
        public static string LatestStateType => nameof(ContentManagerState);

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
        /// <param name="stateType"></param>
        /// <param name="stateText"></param>
        /// <returns></returns>
        public static ContentManagerState? Deserialize(string stateType, string stateText)
        {
            if (string.IsNullOrEmpty(stateType))
                return new ContentManagerState();
            return JsonSerializer.Deserialize<ContentManagerState>(stateText);
        }
    }
}
