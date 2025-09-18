namespace RecordPoint.Connectors.SDK.Content;

/// <summary>
/// Represents the status of a binary submission.
/// </summary>
public enum BinarySubmissionStatus
{
    /// <summary>
    /// The binary has not been successfully submitted yet.
    /// </summary>
    NotSubmitted,
    /// <summary>
    /// The binary has been successfully submitted.
    /// </summary>
    Submitted,
    /// <summary>
    /// The binary submission will be skipped and should not be retried.
    /// </summary>
    Skipped
}
