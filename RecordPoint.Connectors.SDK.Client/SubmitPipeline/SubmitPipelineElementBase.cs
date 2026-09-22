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
        /// Logger used by pipeline elements.
        /// </summary>
        public ILog Log { get; set; } = null;

        private readonly ISubmission _next;

        /// <summary>
        /// Constructs a new submit pipeline element.
        /// </summary>
        /// <param name="next">The next pipeline element to invoke.</param>
        protected SubmitPipelineElementBase(ISubmission next)
        {
            _next = next;
        }

        /// <summary>
        /// Logs a verbose message using the current submission context.
        /// </summary>
        /// <param name="context">The current submission context.</param>
        /// <param name="methodName">The calling method name.</param>
        /// <param name="message">The message to write.</param>
        protected void LogVerbose(SubmitContext context, string methodName, string message)
        {
            Log?.LogVerbose(GetType(), methodName, $"{context.LogPrefix()} {message}");
        }

        /// <summary>
        /// Logs an informational message using the current submission context.
        /// </summary>
        /// <param name="context">The current submission context.</param>
        /// <param name="methodName">The calling method name.</param>
        /// <param name="message">The message to write.</param>
        protected void LogMessage(SubmitContext context, string methodName, string message)
        {
            Log?.LogMessage(GetType(), methodName, $"{context.LogPrefix()} {message}");
        }

        /// <summary>
        /// Logs a warning message using the current submission context.
        /// </summary>
        /// <param name="context">The current submission context.</param>
        /// <param name="methodName">The calling method name.</param>
        /// <param name="message">The message to write.</param>
        protected void LogWarning(SubmitContext context, string methodName, string message)
        {
            Log?.LogWarning(GetType(), methodName, $"{context.LogPrefix()} {message}");
        }

        private IPerformanceEvent CreatePerformanceEvent(SubmitContext submitContext, string methodName)
        {
            return new PerformanceEvent(_next.GetType(), methodName, submitContext.LogPrefix(), Log);
        }

        /// <summary>
        /// Marks the submission as skipped and records the skip reason.
        /// </summary>
        /// <param name="submitContext">The current submission context.</param>
        /// <param name="reason">The reason the submission pipeline is being skipped.</param>
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
        /// <param name="submitContext">The current submission context.</param>
        /// <returns>A task that completes when the next element finishes.</returns>
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
        /// Implements submit behavior for the pipeline element.
        /// </summary>
        /// <param name="submitContext">The current submission context.</param>
        /// <returns>A task that completes when submission processing finishes.</returns>
        public abstract Task Submit(SubmitContext submitContext);
    }
}
