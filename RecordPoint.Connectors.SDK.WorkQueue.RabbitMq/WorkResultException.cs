namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    public class WorkResultException : Exception
    {
        public WorkResultException(string? message) : base(message)
        {

        }
    }
}
