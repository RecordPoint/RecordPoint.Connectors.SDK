using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Describes an item's progress through a disposition action.
    /// This is a copy of a class from the Eiger codebase.
    /// </summary>
    [DataContract]
    public enum ItemDisposalStatus
    {
        /// <summary>
        /// No disposal action has been started.
        /// </summary>
        [EnumMember]
        None,

        /// <summary>
        /// A destroy action is pending.
        /// </summary>
        [EnumMember]
        DestroyPending,

        /// <summary>
        /// The item has been destroyed.
        /// </summary>
        [EnumMember]
        Destroyed,

        /// <summary>
        /// The destroy action failed.
        /// </summary>
        [EnumMember]
        DestroyFailed,

        /// <summary>
        /// The notification for a destroy action failed.
        /// </summary>
        [EnumMember]
        DestroyNotificationFailed,

        /// <summary>
        /// The notification for a destroy action was sent.
        /// </summary>
        [EnumMember]
        DestroyNotificationSent,

        /// <summary>
        /// The item has been transferred.
        /// </summary>
        [EnumMember]
        Transferred,

        /// <summary>
        /// The item has been reviewed.
        /// </summary>
        [EnumMember]
        Reviewed
    }
}
