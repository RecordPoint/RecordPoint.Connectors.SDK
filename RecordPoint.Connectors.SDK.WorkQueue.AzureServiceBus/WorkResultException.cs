namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    public class WorkResultException : Exception
    {
        public WorkResultException(string? message) : base(message)
        {

        }
    }
}
