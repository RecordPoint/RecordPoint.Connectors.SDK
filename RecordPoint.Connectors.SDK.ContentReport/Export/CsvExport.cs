using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Content;
using System.Text;

namespace RecordPoint.Connectors.SDK.ContentReport.Export
{
    public class CsvExport : IExport
    {
        private readonly IOptions<ExportOptions> _exportOptions;
        private readonly ILogger<CsvExport> _log;
        private const string DEAULT_FILE_NAME = "{0}_{1}.csv";
        private static readonly ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
        private const string DELIMITER = ",";

        private readonly List<string> EXPORT_FIELDS =
                                                    new List<string>()
                                                    {
                                                        nameof(Record.Title),
                                                        nameof(Record.Location),
                                                        nameof(Record.SourceLastModifiedDate)
                                                    };

        public CsvExport(IOptions<ExportOptions> exportOptions, ILogger<CsvExport> logger)
        {
            _exportOptions = exportOptions;
            _log = logger;
        }


        public ExportOutcome ExportToDestination(ExportRequest request, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var exportResult = new ExportOutcome();

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.ExportLocation))
                throw new RequiredValueNullException(nameof(request.ExportLocation));

            if (request?.ExportRequestId == null)
                throw new RequiredValueNullException(nameof(request.ExportRequestId));

            try
            {
                // Create a folder to group export files for the 
                // format will be TenantId_ExportDateTime
                var directory = $"{request.ExportLocation}\\{string.Format(_exportOptions.Value.DirectoryFormat, request?.ConnectorConfig?.TenantId, request?.ExporDateTime)}";
                var header = BuildCsvHeader();
                var csvItem = BuildCsvData(request.Records);
                var fileName = string.Format(DEAULT_FILE_NAME, request.ExportRequestId, DateTimeOffset.UtcNow.Ticks); // Filename will be unique by combination of RequestId (guid) and save to disk time in ticks.

                SendToDestination(header, csvItem, fileName, directory);
                exportResult.ExportOutcomeType = ExportOutcomeType.Completed;

            }
            catch (Exception ex)
            {
                _log?.LogError(ex, nameof(CsvExport));
                exportResult.ExportOutcomeType = ExportOutcomeType.Failed;
                exportResult.Exception = ex;
            }

            return exportResult;
        }


        private string BuildCsvHeader()
        {
            return string.Join(DELIMITER, EXPORT_FIELDS);
        }

        private string BuildCsvData(List<Record> records)
        {
            var result = new StringBuilder();
            records.ForEach(x =>
            {
                result.AppendLine($"{x.Title}{DELIMITER}{x.Location}{DELIMITER}{x.SourceLastModifiedDate}");

            });
            return result.ToString();
        }

        private void SendToDestination(string header, string csvData, string fileName, string location)
        {
            _readWriteLock.EnterWriteLock();
            try
            {
                // This statement will create directory if it doesn't exist. Otherwise, return existing.
                Directory.CreateDirectory(location);

                // Append text to the file
                using StreamWriter sw = File.AppendText(Path.Combine(location, fileName));
                sw.WriteLine(header);
                sw.WriteLine(csvData);
                sw.Close();
            }
            finally
            {
                // Release lock
                _readWriteLock.ExitWriteLock();
            }
        }
    }
}
