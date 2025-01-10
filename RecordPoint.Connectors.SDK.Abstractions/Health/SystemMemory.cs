namespace RecordPoint.Connectors.SDK.Health
{
    /// <summary>
    /// The system memory.
    /// </summary>
    public class SystemMemory
    {
        /// <summary>
        /// Gets or sets the free phyiscal memory.
        /// </summary>
        public long FreePhyiscalMemory { get; set; }

        /// <summary>
        /// Gets or sets the total physical memory size.
        /// </summary>
        public long TotalPhysicalMemorySize { get; set; }

        /// <summary>
        /// Gets or sets the free virtual memory.
        /// </summary>
        public long FreeVirtualMemory { get; set; }

        /// <summary>
        /// Gets or sets the total virtual memory size.
        /// </summary>
        public long TotalVirtualMemorySize { get; set; }
    }
}
