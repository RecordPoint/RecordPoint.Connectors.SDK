namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Exception for when an unknown exception is thrown attempting to handle a Work Request
/// </summary>
public class UnknownWorkRequestException(Exception ex) : Exception(EXCEPTION_MESSAGE, ex)
{
    private const string EXCEPTION_MESSAGE = "Unknown exception thrown attempting to handle a Work Request";
}
