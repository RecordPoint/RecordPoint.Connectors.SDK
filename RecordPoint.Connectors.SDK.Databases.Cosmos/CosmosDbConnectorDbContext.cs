using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    /// <summary>
    /// Cosmos Db Connector Database Context
    /// </summary>
    public class CosmosDbConnectorDbContext : ConnectorDbContext
    {

        private const string CHANNEL_CONTAINER_NAME = "channels";
        private const string CONNECTOR_CONTAINER_NAME = "connectors";
        private const string WORK_STATUS_CONTAINER_NAME = "managedworkstatuses";
        private const string AGGREGATION_CONTAINER_NAME = "aggregations";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public CosmosDbConnectorDbContext(DbContextOptions<ConnectorDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChannelModel>()
                .HasNoDiscriminator()
                .ToContainer(CHANNEL_CONTAINER_NAME)
                .HasPartitionKey(a => a.ConnectorId)
                .HasKey(nameof(ChannelModel.ExternalId));

            modelBuilder.Entity<AggregationModel>()
                .HasNoDiscriminator()
                .ToContainer(AGGREGATION_CONTAINER_NAME)
                .HasPartitionKey(a => a.ConnectorId)
                .HasKey(nameof(AggregationModel.ExternalId));

            modelBuilder.Entity<ConnectorConfigurationModel>()
                .HasNoDiscriminator()
                .ToContainer(CONNECTOR_CONTAINER_NAME)
                .HasPartitionKey(a => a.ConnectorId)
                .HasKey(nameof(ConnectorConfigurationModel.ConnectorId));

            modelBuilder.Entity<ManagedWorkStatusModel>()
                .HasNoDiscriminator()
                .ToContainer(WORK_STATUS_CONTAINER_NAME)
                .HasPartitionKey(a => a.ConnectorId)
                .HasKey(nameof(ManagedWorkStatusModel.Id));

            base.OnModelCreating(modelBuilder);
        }
    }
}
