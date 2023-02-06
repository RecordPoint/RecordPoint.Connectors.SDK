using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordPoint.Connectors.SDK.Databases.LocalDb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    ConnectorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => new { x.ConnectorId, x.ExternalId });
                });

            migrationBuilder.CreateTable(
                name: "Aggregations",
                columns: table => new
                {
                    ConnectorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aggregations", x => new { x.ConnectorId, x.ExternalId });
                });

            migrationBuilder.CreateTable(
                name: "Connectors",
                columns: table => new
                {
                    ConnectorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectorTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connectors", x => x.ConnectorId);
                });

            migrationBuilder.CreateTable(
                name: "ManagedWorkStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConnectorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfigurationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Configuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastStatusUpdate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedWorkStatuses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Connectors");

            migrationBuilder.DropTable(
                name: "ManagedWorkStatuses");
        }
    }
}
