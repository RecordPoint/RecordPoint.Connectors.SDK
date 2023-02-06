using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentReport
{

    /// <summary>
    /// Running state for the content report work item
    /// </summary>
    public class ContentReportState
    {

        /// <summary>
        /// Latest version of the sync state type
        /// </summary>
        public static string LatestStateType => nameof(ContentReportState);

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ContentReportState? Deserialize(string stateType, string stateText)
        {
            if (string.IsNullOrEmpty(stateType))
                return new ContentReportState();
            else
                return JsonSerializer.Deserialize<ContentReportState>(stateText);
        }

    }
}
