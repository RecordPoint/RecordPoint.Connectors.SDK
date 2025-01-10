namespace RecordPoint.Connectors.SDK.Observability
{

    /// <summary>
    /// Interface for submitting a variety of statistics and measures about the system in a consistent and structured manner.
    /// </summary>
    public interface ITelemetryTracker
    {
        /// <summary>
        /// Track a single occurance of an event with optional dimensions and associated measures.
        /// 
        /// Events should only be tracked upon completion (or failure) of a piece of work.
        /// Avoid creating seperate start and end events. Each event should be entirely self-contained.
        /// 
        /// Individual events cannot and should not be correlated with other events.
        /// They should not be exposing that level of detail (that's what logs and traces are for).
        /// 
        /// This function will never throw an exception. Any warnings or errors encountered will instead be logged.
        /// </summary>
        /// <param name="name">
        /// The event name uniquely identifies the type of event to track, not the specific instance of the event.
        /// 
        /// Provide in the form of "<system>.<component>.<action>" or "<system>.<component>.<action>.<subaction>" to make it standardised and user-readable.
        /// Use PascalCase for each identifier and limit to no more than 3 words. Keep it short but still understandable without context.
        /// e.g. "Eiger.AzureSearch.Sync", "Eiger.Item.Dispose", or "MachineLearning.Model.Train" or "Eiger.AzureSearch.Sync.Batch"
        /// </param>
        /// <param name="dimensions">
        /// These are the dimensions of the event which define high-level partitions or groupings of individual event instances.
        /// They are provided as key=value pairs. Dimensions are also known as properties.
        /// 
        /// Use dimensions to add additional context to an event which would be useful to filter on.
        /// e.g. "TenantId", "ConnectorId", "Success"
        /// 
        /// When considering which dimensions to add to an event:
        ///     + Do not add dimensions which always have the same value.
        ///       e.g. "BuildNumber" or "ServiceFabricVersion"
        ///       Such dimensions do not provide significant value while adding unnecessary costs and overhead.
        ///     + Do not add dimensions which contain logs, messages, or long strings.
        ///       Events are relatively lightweight and are not traces or exception logs.
        ///       As a rule of thumb, if it has a space in it then it shouldn't be added.
        ///     + In regard to the above, the more times the event occurs, the fewer and shorter the dimensions should be.
        ///     + If the property values are longer than a guid, then reconsider if the property is necessary.
        ///     + For numeric values, consider if a measure would be more appropriate.
        /// 
        /// Note that all events of the same type (i.e. with the same name) should have the same dimensions (but obviously can have different values for them).
        /// </param>
        /// <param name="measures">
        /// These are measurable numeric values associated with the event.
        /// Use measures to define values which can be graphed and have statistical operations performed upon them.
        /// These include counts, timings, and sizes. Metrics are calculated based on these values.
        /// 
        /// When considering which measures to add to an event:
        ///     + The unit should be clearly defined by the measure name.
        ///       e.g. "ItemsProcessed" is a count, "ProcessingTimeMs" is a timing in millieconds, "BinarySizeBytes" is a size in bytes.
        ///     + Timings should be specified in milliseconds.
        ///     + Sizes should be specified in bytes.
        ///     + Measures should relate to precisely this event.
        ///       - Do not add details relating to previous or future events (such as "NextSyncTime").
        ///         We can look at the previous events to determine that.
        ///     + Do not add cumulative progress information relating to parent events (such as "TotalProcessedSoFar").
        ///       - We can look at instances of the parent event type for a total.
        /// 
        /// Note that all events of the same type (i.e. with the same name) should have the same measure keys.
        /// </param>
        void TrackEvent(string name, Dimensions? dimensions = null, Measures? measures = null);

        /// <summary>
        /// Track an exception
        /// </summary>
        /// <param name="name">Name of the exception</param>
        /// <param name="exception">Exception to track</param>
        /// <param name="dimensions">Dimensions associated with the event</param>
        /// <param name="measures">Measures associated with the event</param>
        void TrackException(string name, Exception exception, Dimensions? dimensions = null, Measures? measures = null);

    }
}
