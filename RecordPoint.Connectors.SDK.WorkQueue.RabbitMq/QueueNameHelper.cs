using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    public static class QueueNameHelper
    {
        public static string GetQueueName(string workType, string queuePrefix) => string.IsNullOrEmpty(queuePrefix)
            ? $"{workType.Replace(" ", "-").ToLower()}"
            : $"{queuePrefix}-{workType.Replace(" ", "-").ToLower()}";

        public static string GetDLQueueName(string workType, string queuePrefix) => string.IsNullOrEmpty(queuePrefix)
            ? $"{workType.Replace(" ", "-").ToLower()}-{"DL"}"
            : $"{queuePrefix}-{workType.Replace(" ", "-").ToLower()}-{"DL"}";
    }
}
