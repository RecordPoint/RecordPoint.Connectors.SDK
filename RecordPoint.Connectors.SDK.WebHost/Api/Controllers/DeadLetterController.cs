using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.WebHost.Controllers
{
    /// <summary>
    /// API controller for viewing and replaying dead-letter queue messages.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class DeadLetterController : ControllerBase
    {
        private readonly IDeadLetterQueueService _deadLetterQueueService;
        private readonly IObservabilityScope _observabilityScope;
        private readonly ISystemContext _systemContext;
        private readonly ITelemetryTracker _telemetryTracker;
        private readonly DeadLetterControllerOptions _options;

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="deadLetterQueueService">The dead-letter queue service.</param>
        /// <param name="observabilityScope">The observability scope manager.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="options">The dead-letter controller options.</param>
        public DeadLetterController(
            IDeadLetterQueueService deadLetterQueueService,
            IObservabilityScope observabilityScope,
            ISystemContext systemContext,
            ITelemetryTracker telemetryTracker,
            IOptions<DeadLetterControllerOptions> options)
        {
            _deadLetterQueueService = deadLetterQueueService;
            _observabilityScope = observabilityScope;
            _systemContext = systemContext;
            _telemetryTracker = telemetryTracker;
            _options = options.Value;
        }

        /// <summary>
        /// Gets all dead-letter messages for a queue.
        /// </summary>
        /// <param name="queueName">The queue name to read dead-letter messages from.</param>
        /// <returns>A response containing the matching dead-letter messages.</returns>
        [HttpGet("GetAllMessages")]
        public async Task<IActionResult> Get([BindRequired] string queueName)
        {
            if (!TryValidateQueueName(queueName, out var badRequest))
            {
                return badRequest;
            }

            var deadLetterList = await _deadLetterQueueService.GetMessagesAsync(queueName);

            return Ok(deadLetterList);
        }


        /// <summary>
        /// Gets a dead-letter message by sequence number.
        /// </summary>
        /// <param name="queueName">The queue name to read from.</param>
        /// <param name="sequenceNumber">The sequence number of the dead-letter message.</param>
        /// <returns>A response containing the requested dead-letter message.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([BindRequired] string queueName, [BindRequired] long sequenceNumber)
        {
            if (!TryValidateQueueName(queueName, out var badRequest))
            {
                return badRequest;
            }

            if (sequenceNumber <= 0)
            {
                return BadRequest("Sequence Number is required");
            }

            var deadLetterList = await _deadLetterQueueService.GetMessageAsync(queueName, sequenceNumber);

            return Ok(deadLetterList);
        }

        /// <summary>
        /// Requeues selected dead-letter messages by sequence number.
        /// </summary>
        /// <param name="queueName">The queue name to requeue messages into.</param>
        /// <param name="sequenceNumbers">The sequence numbers to requeue.</param>
        /// <returns>An HTTP response indicating the replay result.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([BindRequired] string queueName, [BindRequired] long[] sequenceNumbers)
        {
            if (!TryValidateQueueName(queueName, out var badRequest))
            {
                return badRequest;
            }

            if (!sequenceNumbers.Any())
            {
                return BadRequest("Sequence Number(s) is required");
            }

            await _deadLetterQueueService.ResubmitMessagesAsync(queueName, sequenceNumbers);
            return Ok();
        }

        /// <summary>
        /// Requeues up to <paramref name="maxCount"/> dead-letter messages for a queue.
        /// </summary>
        /// <param name="queueName">The queue name to requeue messages into.</param>
        /// <param name="maxCount">The maximum number of messages to replay.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>An HTTP response indicating the replay result.</returns>
        [HttpPost("PostAllMessages")]
        public async Task<IActionResult> Post([BindRequired] string queueName, [BindRequired] int maxCount = 1000, CancellationToken cancellationToken = default)
        {
            using var systemScope = _observabilityScope.BeginSystemScope(_systemContext);

            if (!TryValidateQueueName(queueName, out var badRequest))
            {
                return badRequest;
            }

            if (maxCount > _options.MaxReplayBatchSize || maxCount <= 0)
            {
                return BadRequest($"Batch size [{maxCount}] is too big (>{_options.MaxReplayBatchSize}) or invalid");
            }

            var startedAtUtc = DateTime.UtcNow;
            string reason = "Completed";
            var resubmittedCount = 0;

            try
            {
                var result = await _deadLetterQueueService.ResubmitTopMessagesAsync(queueName, maxCount, cancellationToken);
                resubmittedCount = result.Resubmitted;

                if (result.Resubmitted == 0)
                {
                    if (result.QueueConfirmedEmpty)
                    {
                        reason = "NoMessages";
                        return Ok("No dead letters found");
                    }

                    // The DLQ is not confirmed empty - this pass simply made no progress
                    // (e.g. the head messages were momentarily locked, or every send failed).
                    // Return a non-terminal success so a looping caller keeps draining rather
                    // than false-stopping on an unverified "empty".
                    reason = "NoProgress";
                    return Ok();
                }

                return Ok();
            }
            catch (OperationCanceledException)
            {
                reason = "Cancelled";
                throw;
            }
            catch (Exception ex)
            {
                reason = "Exception";
                _telemetryTracker.TrackException(ex);
                throw;
            }
            finally
            {
                var durationMs = (DateTime.UtcNow - startedAtUtc).TotalMilliseconds;
                _telemetryTracker.TrackEvent(
                    "DLQ.ResubmitBatch",
                    new Dimensions
                    {
                        ["QueueName"] = queueName,
                        ["Reason"] = reason
                    },
                    new Measures
                    {
                        ["Requested"] = maxCount,
                        ["Resubmitted"] = resubmittedCount,
                        ["DurationMs"] = durationMs
                    });
            }
        }

        /// <summary>
        /// Deletes a dead-letter message by sequence number.
        /// </summary>
        /// <param name="queueName">The queue name that contains the message.</param>
        /// <param name="sequenceNumber">The sequence number of the message to delete.</param>
        /// <returns>An HTTP response indicating the delete result.</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([BindRequired] string queueName, [BindRequired] long sequenceNumber)
        {
            if (!_options.EnableDeleteOperations)
            {
                return NotFound();
            }

            if (!TryValidateQueueName(queueName, out var badRequest))
            {
                return badRequest;
            }

            if (sequenceNumber <= 0)
            {
                return BadRequest("Sequence Number is required");
            }

            await _deadLetterQueueService.DeleteMessageAsync(queueName, sequenceNumber);

            return Ok();
        }

        /// <summary>
        /// Deletes all dead-letter messages from a queue.
        /// </summary>
        /// <param name="queueName">The queue name to clear.</param>
        /// <returns>An HTTP response indicating the delete result.</returns>
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll([BindRequired] string queueName)
        {
            if (!_options.EnableDeleteOperations)
            {
                return NotFound();
            }

            if (!TryValidateQueueName(queueName, out var badRequest))
            {
                return badRequest;
            }

            await _deadLetterQueueService.DeleteAllMessagesAsync(queueName);
            return Ok();
        }

        private bool TryValidateQueueName(string? queueName, out BadRequestObjectResult badRequest)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                badRequest = BadRequest("Queue Name is required");
                return false;
            }

            if (_options.AllowedQueueNames.Count > 0 && !_options.AllowedQueueNames.Contains(queueName))
            {
                badRequest = BadRequest($"Queue Name is not allowed. Supported queues: {string.Join(", ", _options.AllowedQueueNames)}");
                return false;
            }

            badRequest = null!;
            return true;
        }
    }
}
