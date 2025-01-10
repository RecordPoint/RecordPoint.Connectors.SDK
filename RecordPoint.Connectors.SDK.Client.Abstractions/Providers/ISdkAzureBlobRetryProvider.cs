namespace RecordPoint.Connectors.SDK.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISdkAzureBlobRetryProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeToExecute"></param>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        Task ExecuteWithRetry(
            Func<Task> codeToExecute,
            Type type,
            string methodName);
    }
}
