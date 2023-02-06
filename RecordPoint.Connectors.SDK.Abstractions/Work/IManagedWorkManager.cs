namespace RecordPoint.Connectors.SDK.Work
{
    public interface IManagedWorkManager
    {
        ManagedWorkStatusModel WorkStatus { get; set; }

        Task<WorkResult> CheckAsync(CancellationToken cancellationToken);
        Task<WorkResult> CompleteAsync(string reason, CancellationToken cancellationToken);
        Task<WorkResult> AbandonedAsync(string reason, CancellationToken cancellationToken);
        Task<WorkResult> ContinueAsync(string stateType, string state, DateTimeOffset waitTill, CancellationToken cancellationToken);
        Task<WorkResult> FailedAsync(string reason, CancellationToken cancellationToken);
        Task<WorkResult> FaultyAsync(string reason, CancellationToken cancellationToken, int? faultedCount = 0);
        Task<WorkResult> RetryAsync(DateTimeOffset waitTill, CancellationToken cancellationToken, int? faultedCount = 0);
        Task StartAsync(CancellationToken cancellationToken);
    }
}