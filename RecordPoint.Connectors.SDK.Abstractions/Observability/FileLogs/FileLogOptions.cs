namespace RecordPoint.Connectors.SDK.Observability.FileLogs
{
    public class FileLogOptions
    {
        public const string OPTION_NAME = "FileLogOptions";
        public string LogPath { get; set; } = string.Empty; // Path to write the log to. Note that the name may be changed if rollingInterval is enabled; a datetime will be appended (e.g. "log-<date>.txt" if "log.txt" was the specified name).
        public string RollingInterval { get; set; } = "Day"; // How often to roll over to a new file, based on time.
        public bool RollOnFileSizeLimit { get; set; } = true; // Whether to roll over to a new file when the file size limit is reached (1gb by default). If false, will simply stop logging.
        public int FileSizeLimitBytes { get; set; } = 314572800; // How large to allow a single log file to grow. Listed value here is 300mb.
        public int RetainedFileCountLimit { get; set; } = 10; // Number of files to retain at any one time.
        public string OutputTemplate { get; set; } = string.Empty; // Format for log events
    }
}
