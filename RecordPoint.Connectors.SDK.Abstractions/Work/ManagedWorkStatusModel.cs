using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// DTO model for the Managed Work Status
    /// </summary>
    public class ManagedWorkStatusModel
    {
        /// <summary>
        /// Id that uniquely identifies the work
        /// </summary>
        [Key]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// String that identifies the type of work
        /// </summary>
        public string WorkType { get; set; } = string.Empty;

        /// <summary>
        /// Id of the connector the work belongs to
        /// </summary>
        public string ConnectorId { get; set; } = string.Empty;

        /// <summary>
        /// Unique string that identifies the type of the configuration, which may vary between different versions of the application
        /// </summary>
        public string ConfigurationType { get; set; } = string.Empty;

        /// <summary>
        /// Work configuation encoded as a string. The format depends on the work type.
        /// </summary>
        public string Configuration { get; set; } = string.Empty;

        /// <summary>
        /// Unique string that identifies the type of state
        /// </summary>
        /// <remarks>
        /// The primary use case of this field if providing backwards compatibility for work
        /// created in earlier versions of the application.
        /// 
        /// By setting it to a version specific string new versions of an application can
        /// detect messages created by earlier versions and transparently upgrade them.
        /// </remarks>
        public string StateType { get; set; } = string.Empty;

        /// <summary>
        /// Work state encoded as a string. The format depends on the work type.
        /// </summary>
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// Status of the work
        /// </summary>
        public ManagedWorkStatuses Status { get; set; }

        /// <summary>
        /// Current Work Id we are expecting to execute
        /// </summary>
        public string WorkId { get; set; } = string.Empty;

        /// <summary>
        /// The Time the Works status was last updated.  
        /// This provides a watchdog type function, so work that is "lost" can be cancelled and restarted after it times out.
        /// </summary>
        public DateTimeOffset? LastStatusUpdate { get; set; }

        /// <summary>
        /// Flag to support deadlettering of work
        /// </summary>
        [NotMapped]
        public bool RetryOnFailure { get; set; } = true;

        /// <summary>
        /// The maximum number of times a failed work operation should be retried before being sent to the dead letter queue. This can be set to -1 for never being dead lettered
        /// </summary>
        [NotMapped]
        public int MaxRetries { get; set; } = 5;

        /// <summary>
        /// The amount of time on seconds between retries of a failed work operation
        /// </summary>
        [NotMapped]
        public int RetryDelay { get; set; } = 5;

        /// <summary>
        /// Enables an exponential backoff to retry attempts (default: true)
        /// </summary>
        [NotMapped]
        public bool ExponentialRetryDelay { get; set; } = true;

        /// <summary>
        /// The maximum time a retry delay can be in seconds. This is to stop the exponential delay from getting too long. (default: 1 hour or 3600)
        /// </summary>
        [NotMapped]
        public int MaxRetryDelay { get; set; } = 3600;

        /// <summary>
        /// Copy this work state to the target
        /// </summary>
        /// <param name="target">Object to copy to</param>
        public void CopyTo(ManagedWorkStatusModel target)
        {
            target.Id = Id;
            target.WorkType = WorkType;
            target.ConnectorId = ConnectorId;
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
}
