namespace RecordPoint.Connectors.SDK.ContentReport.Export
{
    public interface IExport
    {

        ExportOutcome ExportToDestination(ExportRequest request, CancellationToken ct);
    }
}
