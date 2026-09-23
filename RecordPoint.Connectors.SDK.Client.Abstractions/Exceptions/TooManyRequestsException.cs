namespace RecordPoint.Connectors.SDK.Exceptions;

/// <summary>
/// 
/// </summary>
public class TooManyRequestsException : Exception
{
    /// <summary>
    /// 
    /// </summary>
    public DateTime WaitUntilTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="time"></param>
    public TooManyRequestsException(string message, DateTime time) : base(message)
    {
        WaitUntilTime = time;
    }
}
