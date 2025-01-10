namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkResultException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public WorkResultException(string? message) : base(message)
        {

        }
    }
}
