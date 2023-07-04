namespace RecordPoint.Connectors.SDK.Providers
{
    public interface ISdkAzureBlobRetryProvider
    {
        Task ExecuteWithRetry(
            Func<Task> codeToExecute,
            Type type,
            string methodName);
    }
}
