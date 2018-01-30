using RecordPoint.Connectors.Diagnostics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    public abstract class SubmitPipelineElementBase
        : ISubmission
    {
        public IPerformance PerformanceMonitor { get; set; } = null;

        public ILog Log { get; set; } = null;

        private readonly ISubmission _next;

        protected SubmitPipelineElementBase(ISubmission next)
        {
            _next = next;
        }
        
        protected void LogVerbose(SubmitContext context, string methodName, string message)
        {
            Log?.LogVerbose(GetType(), methodName, $"{context.LogPrefix()} {message}");
        }

        protected void LogMessage(SubmitContext context, string methodName, string message)
        {
            Log?.LogMessage(GetType(), methodName, $"{context.LogPrefix()} {message}");
        }

        protected void LogWarning(SubmitContext context, string methodName, string message)
        {
            Log?.LogWarning(GetType(), methodName, $"{context.LogPrefix()} {message}");
        }

        private IPerformanceEvent CreatePerformanceEvent(SubmitContext submitContext, string methodName)
        {
            return PerformanceMonitor?.CreateEvent(_next.GetType(), methodName, submitContext.LogPrefix());
        }

        /// <summary>
        /// Indicates that this pipeline element is terminating the pipeline.
        /// </summary>
        /// <param name="submitContext"></param>
        /// <param name="reason"></param>
        protected void SkipNext(SubmitContext submitContext, string reason)
        {
            var title = submitContext.CoreMetaData?.Where(x => x.Name == Fields.Title)?.FirstOrDefault()?.Value;
            submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Skipped;
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
                using (var perfEvent = CreatePerformanceEvent(submitContext, nameof(Submit)))
                {
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
        }

        public abstract Task Submit(SubmitContext submitContext);
    }
}
