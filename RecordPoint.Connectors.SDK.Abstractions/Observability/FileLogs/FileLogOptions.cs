namespace RecordPoint.Connectors.SDK.Observability.FileLogs
{
    /// <summary>
    /// The file log options.
    /// </summary>
    public class FileLogOptions
    {
        /// <summary>
        /// The OPTION NAME.
        /// </summary>
        public const string OPTION_NAME = "FileLogOptions";
        /// <summary>
        /// Gets or sets the log path.
        /// </summary>
        public string LogPath { get; set; } = string.Empty; // Path to write the log to. Note that the name may be changed if rollingInterval is enabled; a datetime will be appended (e.g. "log-<date>.txt" if "log.txt" was the specified name).
        /// <summary>
        /// Gets or sets the rolling interval.
        /// </summary>
        public string RollingInterval { get; set; } = "Day"; // How often to roll over to a new file, based on time.
        /// <summary>
        /// Gets or sets a value indicating whether roll on file size limit.
        /// </summary>
        public bool RollOnFileSizeLimit { get; set; } = true; // Whether to roll over to a new file when the file size limit is reached (1gb by default). If false, will simply stop logging.
        /// <summary>
        /// Gets or sets the file size limit bytes.
        /// </summary>
        public int FileSizeLimitBytes { get; set; } = 314572800; // How large to allow a single log file to grow. Listed value here is 300mb.
        /// <summary>
        /// Gets or sets the retained file count limit.
        /// </summary>
        public int RetainedFileCountLimit { get; set; } = 10; // Number of files to retain at any one time.
        /// <summary>
        /// Gets or sets the output template.
        /// </summary>
        public string OutputTemplate { get; set; } = string.Empty; // Format for log events
    }
}
