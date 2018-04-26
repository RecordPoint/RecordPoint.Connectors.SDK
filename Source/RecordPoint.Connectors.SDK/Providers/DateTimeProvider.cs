using RecordPoint.Connectors.SDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Providers
{
    /// <summary>
    /// Singleton implementation of IDateTimeProvider
    /// </summary>
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        private static Lazy<DateTimeProvider> _dateTimeHelper = new Lazy<DateTimeProvider>();
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