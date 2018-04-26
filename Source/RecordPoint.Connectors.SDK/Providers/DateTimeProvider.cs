using RecordPoint.Connectors.SDK.Interfaces;
using System;

namespace RecordPoint.Connectors.SDK.Providers
{
    /// <summary>
    /// Singleton implementation of IDateTimeProvider
    /// </summary>
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        private static Lazy<DateTimeProvider> _dateTimeHelper = new Lazy<DateTimeProvider>(() => new DateTimeProvider());
        /// <summary>
        /// Singleton instance
        /// </summary>
        public static DateTimeProvider Instance
        {
            get
            {
                return _dateTimeHelper.Value;
            }
            private set { }
        }

        //Singleton
        private DateTimeProvider()
        {
        }

        public DateTime UtcNow
        {
            private set { }
            get { return DateTime.UtcNow; }
        }
    }
}