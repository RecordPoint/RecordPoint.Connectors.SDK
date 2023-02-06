namespace RecordPoint.Connectors.SDK.Providers
{
    /// <summary>
    /// Provides an ability to inject and Moq UtcNow to test with a different time
    /// </summary>
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
