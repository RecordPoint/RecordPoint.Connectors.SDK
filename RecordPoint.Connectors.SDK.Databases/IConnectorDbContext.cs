using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectorDbContext
    {
        /// <summary>
        /// Connectors Data
        /// </summary>
        DbSet<ConnectorConfigurationModel> Connectors { get; set; }

        /// <summary>
        /// Channel Data
        /// </summary>
        DbSet<ChannelModel> Channels { get; set; }

        /// <summary>
        /// Aggregation Data
        /// </summary>
        DbSet<AggregationModel> Aggregations { get; set; }

        /// <summary>
        /// Managed Work Status Data
        /// </summary>
        DbSet<ManagedWorkStatusModel> ManagedWorkStatuses { get; set; }
    }
}