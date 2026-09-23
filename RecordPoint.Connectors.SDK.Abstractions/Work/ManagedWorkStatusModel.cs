using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// DTO model for the Managed Work Status
/// </summary>
public class ManagedWorkStatusModel
{
    /// <summary>
    /// Id that identifies this work context
    /// </summary>
    /// <remarks>
    /// This value will be the same for all continued invcations of the work item.
    /// </remarks>
    [Key]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Id that uniquely identifies the work
    /// </summary>
    /// <remarks>
    /// This value will be unique for all continued invocations of the work item except for retry attempts, where the value will be the same as the original work request.
    /// </remarks>
    public string WorkId { get; set; } = string.Empty;

    /// <summary>
    /// String that identifies the type of work
    /// </summary>
    public string WorkType { get; set; } = string.Empty;

    /// <summary>
    /// Unique string that identifies the type of the configuration, which may vary between different versions of the application
    /// </summary>
    public string ConfigurationType { get; set; } = string.Empty;

    /// <summary>
    /// Work configuation encoded as a string. The format depends on the work type.
    /// </summary>
    public string Configuration { get; set; } = string.Empty;

    /// <summary>
    /// Unique string that identifies the type of state, which may vary between different versions of the application
    /// </summary>
    public string StateType { get; set; } = string.Empty;

    /// <summary>
    /// Work state encoded as a string. The format depends on the work type.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Status of the work
    /// </summary>
    [JsonIgnore]
    [Obsolete("This property will be removed in a future version.")]
    public ManagedWorkStatuses Status { get; set; }

    /// <summary>
    /// The Time the work was first scheduled
    /// </summary>
    /// <remarks>
    /// This value is unchanged across every request of the same context of work
    /// </remarks>
    public DateTimeOffset? WorkInitiatedDate { get; set; }

    /// <summary>
    /// The time this work was requested to be processed
    /// </summary>
    /// <remarks>
    /// This value is set on each work request, however will be unchanged for retried work attempts
    /// </remarks>
    public DateTimeOffset? WorkRequestDate { get; set; }

    #region Transient State Properties
    // These properties are not persisted to the work messages, and are regenerated on each load of the work status

    /// <summary>
    /// Id of the connector the work belongs to
    /// </summary>
    //[NotMapped]
    //TODO: Convert this property to an unmapped property in a future version (when all connectors have been upgraded to the new SDK version that removes the managedworkstatus)
    public string ConnectorId { get; set; } = string.Empty;

    /// <summary>
    /// Id of the tenant the work belongs to
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public string TenantId { get; set; } = string.Empty;

    /// <summary>
    /// Tenant domain name
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public string TenantDomainName { get; set; } = string.Empty;

    /// <summary>
    /// Flag to support deadlettering of work
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public bool RetryOnFailure { get; set; } = true;

    /// <summary>
    /// The maximum number of times a failed work operation should be retried before being sent to the dead letter queue. This can be set to -1 for never being dead lettered
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public int MaxRetries { get; set; } = 5;

    /// <summary>
    /// The amount of time on seconds between retries of a failed work operation
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public int RetryDelay { get; set; } = 5;

    /// <summary>
    /// Enables an exponential backoff to retry attempts (default: true)
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public bool ExponentialRetryDelay { get; set; } = true;

    /// <summary>
    /// The maximum time a retry delay can be in seconds. This is to stop the exponential delay from getting too long. (default: 1 hour or 3600)
    /// </summary>
    [NotMapped]
    [JsonIgnore]
    public int MaxRetryDelay { get; set; } = 3600;
    #endregion

    /// <summary>
    /// Copy this work state to the target
    /// </summary>
    /// <param name="target">Object to copy to</param>
    public void CopyTo(ManagedWorkStatusModel target)
    {
        target.Id = Id;
        target.WorkType = WorkType;
        target.ConnectorId = ConnectorId;
        target.TenantId = TenantId;
        target.TenantDomainName = TenantDomainName;
        target.ConfigurationType = ConfigurationType;
        target.Configuration = Configuration;
        target.StateType = StateType;
        target.State = State;
        target.WorkId = WorkId;
        target.RetryOnFailure = RetryOnFailure;
        target.MaxRetries = MaxRetries;
        target.RetryDelay = RetryDelay;
        target.ExponentialRetryDelay = ExponentialRetryDelay;
        target.MaxRetryDelay = MaxRetryDelay;
        target.WorkInitiatedDate = WorkInitiatedDate;
        target.WorkRequestDate = WorkRequestDate;
    }

    /// <summary>
    /// Creates a clone of this ManagedWorkStatusModel
    /// </summary>
    /// <returns></returns>
    public ManagedWorkStatusModel Clone()
    {
        var clone = new ManagedWorkStatusModel();
        CopyTo(clone);
        return clone;
    }

    /// <summary>
    /// Serialize the work status
    /// </summary>
    /// <returns>Managed Work Status converted to Json</returns>
    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    /// <summary>
    /// Deserialize the work status 
    /// </summary>
    /// <returns>Work Status converted from Json</returns>
    public static ManagedWorkStatusModel? Deserialize(string json)
    {
        return JsonSerializer.Deserialize<ManagedWorkStatusModel>(json);
    }
}
