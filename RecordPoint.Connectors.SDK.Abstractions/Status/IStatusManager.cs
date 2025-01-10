namespace RecordPoint.Connectors.SDK.Status
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStatusManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<StatusModel>> GetStatusModelAsync(CancellationToken cancellationToken);

    }
}
