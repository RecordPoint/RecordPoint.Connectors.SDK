namespace RecordPoint.Connectors.SDK.Providers
{
    /// <summary>
    /// Singleton implementation of IDateTimeProvider.
    /// </summary>
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        private static readonly Lazy<DateTimeProvider> _dateTimeHelper = new(() => new DateTimeProvider());

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static DateTimeProvider Instance
        {
            get => _dateTimeHelper.Value;
        }

        //Singleton
        private DateTimeProvider()
        {
        }

        /// <summary>
        /// Gets a System.DateTime object that is set to the current date and time on this
        /// computer, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime UtcNow
        {
            get => DateTime.UtcNow;
        }
    }
}