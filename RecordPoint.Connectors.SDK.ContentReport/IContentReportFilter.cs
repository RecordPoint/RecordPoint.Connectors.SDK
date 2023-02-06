using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentReport
{
    public interface IContentReportFilter
    {
        Task<IList<Record>> GetRecords(ConnectorConfigModel config, IList<Record> input);
    }
}
