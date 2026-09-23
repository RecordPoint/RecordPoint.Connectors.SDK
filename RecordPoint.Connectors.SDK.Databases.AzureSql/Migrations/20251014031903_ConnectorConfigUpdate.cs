using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Migrations
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
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ChannelDiscoveryExecutedDate",
                table: "Connectors",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Channels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MetaData",
                table: "Channels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ParentExternalId",
                table: "Aggregations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Aggregations",
                type: "nvarchar(max)",
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
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Channels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MetaData",
                table: "Channels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
