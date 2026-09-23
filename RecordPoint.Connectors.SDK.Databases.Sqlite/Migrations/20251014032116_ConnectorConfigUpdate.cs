using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordPoint.Connectors.SDK.Databases.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class ConnectorConfigUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportLocation",
                table: "Connectors");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChannelDiscoveryEnqueuedDate",
                table: "Connectors",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChannelDiscoveryExecutedDate",
                table: "Connectors",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Channels",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "MetaData",
                table: "Channels",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ParentExternalId",
                table: "Aggregations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Aggregations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentExternalId",
                table: "Aggregations");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Aggregations");

            migrationBuilder.DropColumn(
                name: "ChannelDiscoveryEnqueuedDate",
                table: "Connectors");

            migrationBuilder.DropColumn(
                name: "ChannelDiscoveryExecutedDate",
                table: "Connectors");

            migrationBuilder.AddColumn<string>(
                name: "ReportLocation",
                table: "Connectors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Channels",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaData",
                table: "Channels",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
