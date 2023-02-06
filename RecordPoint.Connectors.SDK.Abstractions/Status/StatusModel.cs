namespace RecordPoint.Connectors.SDK.Status
{
    public class StatusModel
    {
        public string ConnectorId { get; set; } = string.Empty;
        public List<string> Status { get; set; } = new();
    }
}
