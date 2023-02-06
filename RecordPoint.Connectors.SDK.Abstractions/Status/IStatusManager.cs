namespace RecordPoint.Connectors.SDK.Status
{
    public interface IStatusManager
    {
        public Task<List<StatusModel>> GetStatusModelAsync(CancellationToken cancellationToken);

    }
}
