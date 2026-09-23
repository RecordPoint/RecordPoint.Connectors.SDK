namespace RecordPoint.Connectors.SDK.Caching
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCacheItemType"></typeparam>
    public interface ICacheAction<TCacheItemType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CacheActionResult<TCacheItemType>> ExecuteAsync(CacheActionContext context, CancellationToken cancellationToken = (default));
    }
}
