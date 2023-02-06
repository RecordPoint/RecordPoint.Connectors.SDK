﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecordPoint.Connectors.SDK.Databases.AzureSql;

#nullable disable

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Migrations
{
    [DbContext(typeof(AzureSqlConnectorDbContext))]
    [Migration("20220214034413_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel", b =>
                {
                    b.Property<string>("ConnectorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConnectorTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConnectorId");

                    b.ToTable("Connectors");
                });

            modelBuilder.Entity("RecordPoint.Connectors.SDK.Content.ChannelModel", b =>
                {
                    b.Property<string>("ConnectorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MetaData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConnectorId", "ExternalId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Configuration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfigurationType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConnectorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastStatusUpdate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Status");

                    b.Property<string>("WorkId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ManagedWorkStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}
