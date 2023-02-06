using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentReport.Export
{
    public class ExportRequest
    {
        public List<Record> Records { get; set; }

        public string ExportLocation { get; set; }

        public DateTimeOffset ExporDateTime { get; set; }

        public string ExportRequestId { get; set; }

        public ConnectorConfigModel ConnectorConfig { get; set; }
    }
}
