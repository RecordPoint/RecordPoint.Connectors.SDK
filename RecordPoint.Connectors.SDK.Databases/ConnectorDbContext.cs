using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// Connectors Entity framework DB Context Base class
    /// </summary>
    /// <remarks>
    /// Concrete instances of this class are created by the Provider module
    /// </remarks>
    public abstract class ConnectorDbContext : DbContext, IConnectorDbContext
    {
        protected ConnectorDbContext(DbContextOptions<ConnectorDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        public DbSet<ConnectorConfigurationModel> Connectors { get; set; }

        /// <inheritdoc/>
        public DbSet<ChannelModel> Channels { get; set; }

        /// <inheritdoc/>
        public DbSet<AggregationModel> Aggregations { get; set; }

        /// <inheritdoc/>
        public DbSet<ManagedWorkStatusModel> ManagedWorkStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            MapChannels(modelBuilder);
            MapManagedWorkStatuses(modelBuilder);
            MapAggregations(modelBuilder);
        }

        protected virtual string GetSchema() => null;

        protected virtual void MapChannels(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<ChannelModel>();
            entityBuilder.HasKey(a => new { a.ConnectorId, a.ExternalId });
        }

        protected virtual void MapAggregations(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<AggregationModel>();
            entityBuilder.HasKey(a => new { a.ConnectorId, a.ExternalId });
        }
        protected virtual void MapManagedWorkStatuses(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<ManagedWorkStatusModel>();
            entityBuilder.Property(x => x.Status).HasColumnName("Status").HasConversion(new EnumToStringConverter<ManagedWorkStatuses>());
        }
    }
}