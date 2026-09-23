using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordPoint.Connectors.SDK.Databases.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddChannelDiscoveryWorkId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastStatusUpdate",
                table: "ManagedWorkStatuses",
                newName: "WorkRequestDate");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "WorkInitiatedDate",
                table: "ManagedWorkStatuses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChannelDiscoveryWorkId",
                table: "Connectors",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentSynchronisationWorkId",
                table: "Channels",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkInitiatedDate",
                table: "ManagedWorkStatuses");

            migrationBuilder.DropColumn(
                name: "ChannelDiscoveryWorkId",
                table: "Connectors");

            migrationBuilder.DropColumn(
                name: "ContentSynchronisationWorkId",
                table: "Channels");

            migrationBuilder.RenameColumn(
                name: "WorkRequestDate",
                table: "ManagedWorkStatuses",
                newName: "LastStatusUpdate");
        }
    }
}
