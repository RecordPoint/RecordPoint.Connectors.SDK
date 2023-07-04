using RecordPoint.Connectors.SDK.Diagnostics;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// Base class for submit pipeline elements.
    /// </summary>
    public abstract class SubmitPipelineElementBase
        : ISubmission
    {
        /// <summary>
        /// A log.
        /// </summary>
        public ILog Log { get; set; } = null;

        private readonly ISubmission _next;

        /// <summary>
        /// Constructs a new SubmitPipelineElementBase with an optional next submit
        /// pipeline element.
        /// </summary>
        /// <param name="next"></param>
        protected SubmitPipelineElementBase(ISubmission next)
        {
            _next = next;
        }

        /// <summary>
        /// Logs a verbose message, providing information from the SubmitContext.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        protected void LogVerbose(SubmitContext context, string methodName, string message)
        {
            Log?.LogVerbose(GetType(), methodName, $"{context.LogPrefix()} {message}");
        }

        /// <summary>
        /// Logs a message, providing information from the SubmitContext.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        protected void LogMessage(SubmitContext context, string methodName, string message)
        {
            Log?.LogMessage(GetType(), methodName, $"{context.LogPrefix()} {message}");
        }

        /// <summary>
        /// Logs a warning message, providing information from the SubmitContext.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        protected void LogWarning(SubmitContext context, string methodName, string message)
        {
            Log?.LogWarning(GetType(), methodName, $"{context.LogPrefix()} {message}");
        }

        private IPerformanceEvent CreatePerformanceEvent(SubmitContext submitContext, string methodName)
        {
            return new PerformanceEvent(_next.GetType(), methodName, submitContext.LogPrefix(), Log);
        }

        /// <summary>
        /// Indicates that this pipeline element is terminating the pipeline.
        /// Note this method only performs the appropriate logging and sets the SubmitContext.SubmitResult to Skipped.
        /// The calling method still needs to be careful to return early or otherwise skip the rest of the pipeline.
        /// </summary>
        /// <param name="submitContext"></param>
        /// <param name="reason"></param>
        protected void SkipNext(SubmitContext submitContext, string reason)
        {
            var title = submitContext.CoreMetaData?.Where(x => x.Name == Fields.Title)?.FirstOrDefault()?.Value;
            submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Skipped;
            submitContext.SubmitResult.Reason = reason;
            LogVerbose(submitContext, nameof(SkipNext), $"not submitting item [{title}] because of reason [{reason}]");
        }

        /// <summary>
        /// Invokes the next element in the submission pipeline, if one exists.
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        protected async Task InvokeNext(SubmitContext submitContext)
        {
            if (_next != null)
            {
                // Note the timing here may not be the most intuitive.
                // The timing for any given element will be the sum of all processing in the remaining chain.
                // Be aware of this when analysing the metrics.
                using var perfEvent = CreatePerformanceEvent(submitContext, nameof(Submit));
                try
                {
                    submitContext.CancellationToken.ThrowIfCancellationRequested();
                    await _next.Submit(submitContext).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    perfEvent?.Exception(ex);
                    throw;
                }
            }
        }

        /// <summary>
        /// Implement in a derived class to provide custom submit pipeline functionality.
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        public abstract Task Submit(SubmitContext submitContext);
    }
}
