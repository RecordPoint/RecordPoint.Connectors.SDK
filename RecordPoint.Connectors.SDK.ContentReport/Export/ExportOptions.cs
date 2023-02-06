namespace RecordPoint.Connectors.SDK.ContentReport.Export
{
    public class ExportOptions
    {
        /// <summary>
        /// Format of folder to store exported csv files per report request
        /// </summary>
        public string DirectoryFormat { get; set; }

        /// <summary>
        /// Maxiumn records before rolling to new file
        /// </summary>
        public int MaximumRecordsRollover { get; set; }
    }
}
