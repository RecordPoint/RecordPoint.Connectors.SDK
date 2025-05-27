namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// 
    /// </summary>
    public interface IManagedWorkManager : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        ManagedWorkStatusModel WorkStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkResult> CheckAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkResult> CompleteAsync(string reason, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkResult> AbandonedAsync(string reason, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateType"></param>
        /// <param name="state"></param>
        /// <param name="waitTill"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkResult> ContinueAsync(string stateType, string state, DateTimeOffset waitTill, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkResult> FailedAsync(string reason, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="faultedCount"></param>
        /// <returns></returns>
        Task<WorkResult> FaultyAsync(string reason, Exception exception, CancellationToken cancellationToken, int? faultedCount = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitTill"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="faultedCount"></param>
        /// <returns></returns>
        Task<WorkResult> RetryAsync(DateTimeOffset waitTill, CancellationToken cancellationToken, int? faultedCount = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StartAsync(CancellationToken cancellationToken);
    }
}