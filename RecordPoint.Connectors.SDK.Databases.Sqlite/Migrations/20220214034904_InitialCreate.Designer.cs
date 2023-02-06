﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecordPoint.Connectors.SDK.Databases.Sqlite;

#nullable disable

namespace RecordPoint.Connectors.SDK.Databases.Sqlite.Migrations
{
    [DbContext(typeof(SqliteConnectorDbContext))]
    [Migration("20220214034904_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel", b =>
                {
                    b.Property<string>("ConnectorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConnectorTypeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReportLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ConnectorId");

                    b.ToTable("Connectors");
                });

            modelBuilder.Entity("RecordPoint.Connectors.SDK.Content.ChannelModel", b =>
                {
                    b.Property<string>("ConnectorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExternalId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MetaData")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ConnectorId", "ExternalId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Configuration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConfigurationType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConnectorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("LastStatusUpdate")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Status");

                    b.Property<string>("WorkId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ManagedWorkStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}
