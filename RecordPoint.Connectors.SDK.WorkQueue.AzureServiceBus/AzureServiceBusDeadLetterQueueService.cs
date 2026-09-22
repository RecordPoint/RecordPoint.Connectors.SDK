using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Work.Models;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Extensions;

namespace RecordPoint.Connectors.SDK.WebHost.Services
{
    /// <summary>
    /// Deadletter queue service class
    /// </summary>
    public class AzureServiceBusDeadLetterQueueService : IDeadLetterQueueService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusAdministrationClient _serviceBusAdministrationClient;
        private readonly IManagedWorkStatusManager _managedWorkStatusManager;
        private readonly ILogger<AzureServiceBusDeadLetterQueueService> _logger;

        /// <summary>
        /// Maximum number of messages to receive at once from the queue
        /// </summary>
        public const int MaxMessages = 1000;

        /// <summary>
        /// Number of messages to receive per drain iteration. Kept well below
        /// MaxMessages so a batch of send+complete round-trips settles comfortably
        /// inside the queue lock window, limiting the lock-expiry duplicate risk.
        /// </summary>
        private const int DrainReceiveBatchSize = 100;

        /// <summary>
        /// Safety margin left before a message's lock expires. If a received
        /// message's lock is within this margin we stop the batch rather than send
        /// a copy we may not be able to complete.
        /// </summary>
        private static readonly TimeSpan LockSafetyMargin = TimeSpan.FromSeconds(15);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceBusClientFactory"></param>
        /// <param name="managedWorkStatusManager"></param>
        /// <param name="logger"></param>
        public AzureServiceBusDeadLetterQueueService(IServiceBusClientFactory serviceBusClientFactory, IManagedWorkStatusManager managedWorkStatusManager, ILogger<AzureServiceBusDeadLetterQueueService> logger)
        {
            _serviceBusClient = serviceBusClientFactory.CreateServiceBusClient();
            _serviceBusAdministrationClient = serviceBusClientFactory.CreateServiceBusAdministrationClient();
            _managedWorkStatusManager = managedWorkStatusManager;
            _logger = logger;
        }

        /// <summary>
        /// Get all messages based on the queue
        /// </summary>
        public async Task<List<DeadLetterModel>> GetMessagesAsync(string queueName, int maxCount = 0)
        {
            var serviceBusReceivedMessages = await GetAllPeekedServiceBusMessagesAsync(queueName, maxCount);

            var deadLetterList = new List<DeadLetterModel>();

            foreach (var receivedMessage in serviceBusReceivedMessages)
            {
                deadLetterList.Add(receivedMessage.ToDeadLetterModel());
            }
            return deadLetterList;
        }

        /// <summary>
        /// Get message based on the queue and sequenceNumber
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        public async Task<DeadLetterModel> GetMessageAsync(string queueName, long sequenceNumber)
        {
            var receivedMessage = await GetPeekedServiceBusMessageAsync(queueName, sequenceNumber);
            return receivedMessage.ToDeadLetterModel();
        }


        /// <summary>
        /// Resubmit to queue based on the queueName and sequenceNumbers
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumbers"></param>
        /// <returns></returns>
        public async Task ResubmitMessagesAsync(string queueName, long[] sequenceNumbers)
        {
            var receiverOptions = new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter };
            await using var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);
            await using var serviceBusSender = _serviceBusClient.CreateSender(queueName);
            var messageBatch = await serviceBusSender.CreateMessageBatchAsync();

            var deadLetterMessagesList = new List<ServiceBusReceivedMessage>();

            var (receivedMessages, nonTargetMessages) = await GetReceivedServiceBusMessagesAsync(serviceBusReceiver, queueName, sequenceNumbers.ToList());

            foreach (var receivedMessage in receivedMessages)
            {
                var outboundMessage = BuildResubmitMessage(receivedMessage, queueName);

                if (!messageBatch.TryAddMessage(outboundMessage))
                {
                    // Batch is full - send what we have so far
                    await serviceBusSender.SendMessagesAsync(messageBatch);
                    messageBatch = await serviceBusSender.CreateMessageBatchAsync();

                    if (!messageBatch.TryAddMessage(outboundMessage))
                    {
                        throw new InvalidOperationException(
                            $"Message with sequence number {receivedMessage.SequenceNumber} is too large for an empty batch.");
                    }
                }

                //Remove the message after we put it on the bus
                deadLetterMessagesList.Add(receivedMessage);
            }

            if (messageBatch.Count > 0)
            {
                await serviceBusSender.SendMessagesAsync(messageBatch);
            }

            foreach (var deadLetterMessages in deadLetterMessagesList)
            {
                await serviceBusReceiver.CompleteMessageAsync(deadLetterMessages);
            }

            // Targets are safely resubmitted and completed - now release the non-target
            // locks. Deferring the abandon until after the targets settle keeps a slow
            // abandon phase off the critical path (see GetReceivedServiceBusMessagesAsync).
            await AbandonNonTargetsAsync(serviceBusReceiver, nonTargetMessages, queueName);
        }

        /// <summary>
        /// Builds the message to resubmit for a dead-lettered message. When the body
        /// is a valid WorkRequest the fault count is reset; otherwise (the body is
        /// null-shaped, fails to parse, or is not an AMQP data body) the original
        /// message is forwarded verbatim so it is never lost. A single unparseable
        /// message must not abort the whole resubmit, so the deserialise is guarded.
        /// </summary>
        private ServiceBusMessage BuildResubmitMessage(ServiceBusReceivedMessage receivedMessage, string queueName)
        {
            WorkRequest? workRequest = null;
            try
            {
                workRequest = JsonConvert.DeserializeObject<WorkRequest>(receivedMessage.Body.ToString());
            }
            catch (Exception ex) when (ex is JsonException or NotSupportedException)
            {
                // JsonException: the body is not valid WorkRequest JSON.
                // NotSupportedException: the message has a non-data AMQP body, so even
                // reading .Body throws. Forward the original message verbatim via the
                // copy constructor (which safely carries any body kind and its metadata)
                // rather than touching .Body again, which would re-throw and abort the batch.
                _logger.LogWarning(
                    ex,
                    "Dead-letter message {SequenceNumber} on queue '{QueueName}' could not be parsed as a WorkRequest; forwarding it verbatim.",
                    receivedMessage.SequenceNumber,
                    queueName);
                return new ServiceBusMessage(receivedMessage);
            }

            if (workRequest == null)
            {
                _logger.LogWarning(
                    "Dead-letter message {SequenceNumber} on queue '{QueueName}' did not deserialise to a WorkRequest; forwarding it verbatim.",
                    receivedMessage.SequenceNumber,
                    queueName);
                return new ServiceBusMessage(receivedMessage);
            }

            // Reset the fault count so the message gets a fresh set of attempts.
            workRequest.FaultedCount = 0;
            return new ServiceBusMessage(JsonConvert.SerializeObject(workRequest));
        }

        /// <summary>
        /// Drain up to <paramref name="maxCount"/> messages from the dead-letter
        /// sub-queue and resubmit them to the main queue. Uses a single
        /// PeekLock receiver to stream messages directly off the head of the
        /// DLQ, avoiding the peek/receive mismatch and cross-call lock
        /// pollution that limited the previous implementation.
        /// </summary>
        /// <param name="queueName">The main queue whose DLQ should be drained.</param>
        /// <param name="maxCount">The maximum number of messages to resubmit in this call.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A <see cref="DeadLetterResubmitResult"/> with the number of messages resubmitted
        /// and, when that is 0, whether the dead-letter queue was confirmed empty by a
        /// lock-independent peek (as opposed to the pass merely making no progress).
        /// </returns>
        public async Task<DeadLetterResubmitResult> ResubmitTopMessagesAsync(string queueName, int maxCount, CancellationToken cancellationToken = default)
        {
            // A non-positive maxCount is a no-op request. Report it as terminal (confirmed
            // empty) rather than not-empty, so a caller looping "until confirmed empty" with
            // maxCount <= 0 stops instead of spinning forever. (The controller already
            // rejects maxCount <= 0 upstream; this only guards direct callers.)
            if (maxCount <= 0) return new DeadLetterResubmitResult(0, QueueConfirmedEmpty: true);

            var receiverOptions = new ServiceBusReceiverOptions
            {
                SubQueue = SubQueue.DeadLetter,
                ReceiveMode = ServiceBusReceiveMode.PeekLock
            };
            await using var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);
            await using var serviceBusSender = _serviceBusClient.CreateSender(queueName);

            // Prefer the queue's configured LockDuration for the safety margin. If the
            // admin read fails we leave this null and derive the margin from the
            // observed lock window of the first received message instead, rather than a
            // fixed fallback that could exceed a short lock and drain nothing.
            var lockMargin = await TryGetConfiguredLockMarginAsync(queueName);

            var resubmittedCount = 0;
            var attemptedSequenceNumbers = new HashSet<long>();

            // Bound the loop by attempts, not just successes. The resubmitted count rises
            // only for messages that both send and complete, so under a systemic send
            // failure (main queue disabled, at message quota, or throttled) it would never
            // advance while distinct messages keep arriving, walking the entire DLQ and
            // growing the attempted-sequence set without bound. The no-progress guard below
            // only stops the loop when the broker happens to redeliver already-attempted
            // messages ahead of unseen ones, which Service Bus does not guarantee. Stopping
            // once maxCount distinct messages have been attempted keeps the work proportional
            // to maxCount regardless of how many succeed. Anything not resubmitted this run
            // stays in the DLQ and drains on a later pass.
            while (resubmittedCount < maxCount
                   && attemptedSequenceNumbers.Count < maxCount
                   && !cancellationToken.IsCancellationRequested)
            {
                var remainingAttemptBudget = maxCount - attemptedSequenceNumbers.Count;
                var batchSize = Math.Min(DrainReceiveBatchSize, remainingAttemptBudget);
                var batch = await serviceBusReceiver.ReceiveMessagesAsync(
                    batchSize,
                    TimeSpan.FromSeconds(5),
                    cancellationToken);

                if (batch == null || batch.Count == 0)
                {
                    // No more messages available within the short wait - the
                    // DLQ either is empty or has only locked messages left.
                    break;
                }

                // If the queue metadata was unavailable, estimate the margin from the
                // remaining lock on a freshly received message (~ the lock duration).
                lockMargin ??= DeriveLockMarginFromObserved(batch[0]);

                var (successful, newlyAttempted) = await ResubmitBatchAsync(
                    serviceBusReceiver,
                    serviceBusSender,
                    batch,
                    attemptedSequenceNumbers,
                    lockMargin.Value,
                    queueName,
                    cancellationToken);

                resubmittedCount += successful;

                // Progress guard. If the batch produced no newly-attempted message
                // (it contained only messages we already tried this run - e.g. a
                // poison/throttled message that was abandoned and redelivered, or a
                // lock-at-risk stop on the first message) then no further progress is
                // possible, so stop. This bounds the loop by the number of distinct
                // DLQ messages and prevents an un-sendable message spinning forever.
                if (newlyAttempted == 0)
                {
                    _logger.LogWarning(
                        "Stopping DLQ drain on queue '{QueueName}': no further progress possible this run (remaining messages were already attempted or their locks are at risk). Resubmitted {ResubmittedCount}.",
                        queueName,
                        resubmittedCount);
                    break;
                }
            }

            // When nothing was resubmitted this pass, the caller cannot tell an empty DLQ
            // from a pass that merely made no progress (a transient empty receive because
            // the head messages were momentarily locked, or every send failed). Both return
            // 0. Confirm emptiness with a single lock-independent peek: peek ignores PeekLock,
            // so it sees messages even when the head is locked. Peek empty => genuinely empty;
            // peek non-empty => not empty, so the caller should drain again on a later pass.
            var queueConfirmedEmpty = false;
            if (resubmittedCount == 0)
            {
                try
                {
                    var peeked = await serviceBusReceiver.PeekMessagesAsync(1, cancellationToken: cancellationToken);
                    queueConfirmedEmpty = peeked.Count == 0;
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    // Safe direction: if we cannot confirm emptiness, report NOT empty so a
                    // looping caller tries again rather than stopping on an unverified empty.
                    _logger.LogWarning(
                        ex,
                        "Could not peek dead-letter queue for '{QueueName}' to confirm it is empty; treating it as not empty.",
                        queueName);
                    queueConfirmedEmpty = false;
                }
            }

            return new DeadLetterResubmitResult(resubmittedCount, queueConfirmedEmpty);
        }

        private async Task<(int successful, int newlyAttempted)> ResubmitBatchAsync(
            ServiceBusReceiver serviceBusReceiver,
            ServiceBusSender serviceBusSender,
            IReadOnlyList<ServiceBusReceivedMessage> batch,
            HashSet<long> attemptedSequenceNumbers,
            TimeSpan lockMargin,
            string queueName,
            CancellationToken cancellationToken)
        {
            var successful = 0;
            var newlyAttempted = 0;
            foreach (var receivedMessage in batch)
            {
                if (cancellationToken.IsCancellationRequested) break;

                // Skip anything already attempted this run. A message whose Send
                // succeeded but whose Complete failed (lock lost in the gap) becomes
                // available again immediately; re-sending it would duplicate it on the
                // main queue within the same call.
                if (attemptedSequenceNumbers.Contains(receivedMessage.SequenceNumber))
                {
                    continue;
                }

                // The batch was received together and shares one lock window. Once a
                // message's lock is within the safety margin we can no longer settle
                // (Complete) it after sending, and sending anyway would post a copy we
                // cannot complete - which redelivery then duplicates on the main queue.
                // Stop here; the remaining messages keep their lock, expire back to the
                // DLQ, and are drained cleanly on a later pass.
                if (IsLockAtRisk(receivedMessage, lockMargin))
                {
                    _logger.LogWarning(
                        "Stopping DLQ resubmit batch early on queue '{QueueName}': lock for message {SequenceNumber} expires at {LockedUntil:o}, within the safety margin. Remaining messages will be drained on a later pass.",
                        queueName,
                        receivedMessage.SequenceNumber,
                        receivedMessage.LockedUntil);
                    break;
                }

                // Record as attempted BEFORE the attempt, so a send-succeeded/
                // complete-failed message is not re-sent when it is redelivered.
                attemptedSequenceNumbers.Add(receivedMessage.SequenceNumber);
                newlyAttempted++;

                if (await TryResubmitMessageAsync(serviceBusReceiver, serviceBusSender, receivedMessage, queueName, cancellationToken))
                {
                    successful++;
                }
            }
            return (successful, newlyAttempted);
        }

        private static bool IsLockAtRisk(ServiceBusReceivedMessage receivedMessage, TimeSpan lockMargin)
        {
            // Leave enough time to complete a send + settle round-trip before the
            // lock expires. Messages received under PeekLock always carry a real
            // LockedUntil; a default (unset) value is treated as at-risk.
            return receivedMessage.LockedUntil <= DateTimeOffset.UtcNow + lockMargin;
        }

        private async Task<TimeSpan?> TryGetConfiguredLockMarginAsync(string queueName)
        {
            try
            {
                var properties = await _serviceBusAdministrationClient.GetQueueAsync(queueName);
                return ClampLockMargin(properties.Value.LockDuration);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "Could not read LockDuration for queue '{QueueName}'; the lock safety margin will be derived from the observed lock window of received messages.",
                    queueName);
                return null;
            }
        }

        private static TimeSpan DeriveLockMarginFromObserved(ServiceBusReceivedMessage message)
        {
            // A freshly received PeekLock message's remaining lock is approximately the
            // queue's LockDuration, so it is a sound estimate when the admin metadata
            // was unavailable - and, unlike a fixed fallback, it cannot exceed the lock.
            var observed = message.LockedUntil - DateTimeOffset.UtcNow;
            return ClampLockMargin(observed);
        }

        private static TimeSpan ClampLockMargin(TimeSpan lockWindow)
        {
            // Never use a margin larger than half the lock window, otherwise the guard
            // would trip on a message the instant it is received on a short-lock queue
            // and drain nothing. Never negative. Cap at the default for long locks.
            if (lockWindow <= TimeSpan.Zero) return TimeSpan.Zero;
            var half = TimeSpan.FromTicks(lockWindow.Ticks / 2);
            return half < LockSafetyMargin ? half : LockSafetyMargin;
        }

        private async Task<bool> TryResubmitMessageAsync(
            ServiceBusReceiver serviceBusReceiver,
            ServiceBusSender serviceBusSender,
            ServiceBusReceivedMessage receivedMessage,
            string queueName,
            CancellationToken cancellationToken)
        {
            try
            {
                var outboundMessage = BuildOutboundMessage(receivedMessage);

                // Send first, then complete. This is deliberately at-least-once:
                // if CompleteMessageAsync fails after a successful send the message
                // stays in the DLQ and may be resubmitted again on a later run, so
                // downstream processing must be idempotent. The alternative
                // (complete before send) would risk losing the message entirely,
                // which is worse.
                await serviceBusSender.SendMessageAsync(outboundMessage, cancellationToken);
                await serviceBusReceiver.CompleteMessageAsync(receivedMessage, cancellationToken);
                return true;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                // The caller (e.g. HttpContext.RequestAborted) cancelled. Propagate
                // so the drain surfaces as cancelled rather than being logged as a
                // resubmit failure and reported as a successful completion.
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "Failed to resubmit dead-letter message {SequenceNumber} from queue '{QueueName}'. Abandoning so it remains in the DLQ.",
                    receivedMessage.SequenceNumber,
                    queueName);

                await TryAbandonAsync(serviceBusReceiver, receivedMessage, cancellationToken);
                return false;
            }
        }

        private async Task TryAbandonAsync(
            ServiceBusReceiver serviceBusReceiver,
            ServiceBusReceivedMessage receivedMessage,
            CancellationToken cancellationToken)
        {
            try
            {
                await serviceBusReceiver.AbandonMessageAsync(receivedMessage, cancellationToken: cancellationToken);
            }
            catch (Exception abandonEx)
            {
                _logger.LogWarning(
                    abandonEx,
                    "Failed to abandon dead-letter message {SequenceNumber} after a resubmit failure. The PeekLock will expire and the message will return to the DLQ.",
                    receivedMessage.SequenceNumber);
            }
        }

        private static ServiceBusMessage BuildOutboundMessage(ServiceBusReceivedMessage receivedMessage)
        {
            var workRequest = JsonConvert.DeserializeObject<WorkRequest>(receivedMessage.Body.ToString());

            ServiceBusMessage outboundMessage;
            if (workRequest != null)
            {
                // Reset the fault count so the message gets a fresh set of attempts.
                workRequest.FaultedCount = 0;
                outboundMessage = new ServiceBusMessage(JsonConvert.SerializeObject(workRequest));
            }
            else
            {
                // Forward the body verbatim if it does not match the WorkRequest shape.
                outboundMessage = new ServiceBusMessage(receivedMessage.Body);
            }

            CopyMessageMetadata(receivedMessage, outboundMessage);
            return outboundMessage;
        }

        private static void CopyMessageMetadata(ServiceBusReceivedMessage source, ServiceBusMessage destination)
        {
            // Preserve routing/correlation metadata so the resubmitted message keeps
            // its identity for downstream tracing and correlation. MessageId is left
            // to be reassigned to avoid surprising duplicate-detection drops.
            destination.CorrelationId = source.CorrelationId;
            destination.Subject = source.Subject;
            destination.ContentType = source.ContentType;

            foreach (var property in source.ApplicationProperties)
            {
                destination.ApplicationProperties[property.Key] = property.Value;
            }
        }

        /// <summary>
        /// Delete the message from the deadletter queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        public async Task DeleteMessageAsync(string queueName, long sequenceNumber)
        {
            var receiverOptions = new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter };
            await using var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);

            var sequenceNumbers = new List<long> { sequenceNumber };
            var (receivedMessages, nonTargetMessages) = await GetReceivedServiceBusMessagesAsync(serviceBusReceiver, queueName, sequenceNumbers);

            // Guard against an empty result: the target may be missing, locked, or
            // beyond the scan bound. Indexing [0] unguarded would throw.
            if (receivedMessages.Count > 0 && receivedMessages[0].SequenceNumber == sequenceNumber)
            {
                await serviceBusReceiver.CompleteMessageAsync(receivedMessages[0]);
            }

            // Release any non-target locks after the target is completed.
            await AbandonNonTargetsAsync(serviceBusReceiver, nonTargetMessages, queueName);
        }

        /// <summary>
        /// Delete the all messages from the deadletter queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public async Task DeleteAllMessagesAsync(string queueName)
        {
            var receiverOptions = new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter };
            await using var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);

            QueueRuntimeProperties queue = await _serviceBusAdministrationClient.GetQueueRuntimePropertiesAsync(queueName);

            // we need to received the message as peekmessage doesn't allow to delete the message 
            for (var i = 0; i < queue.DeadLetterMessageCount; i++)
            {
                var deleteMessage = await serviceBusReceiver.ReceiveMessageAsync();
                if (deleteMessage != null)
                    await serviceBusReceiver.CompleteMessageAsync(deleteMessage);
                else
                    break;
            }
        }

        private async Task<(List<ServiceBusReceivedMessage> Targets, List<ServiceBusReceivedMessage> NonTargets)> GetReceivedServiceBusMessagesAsync(ServiceBusReceiver serviceBusReceiver, string queueName, IList<long> sequenceNumbers)
        {
            var messages = new List<ServiceBusReceivedMessage>();
            var nonTargetMessages = new List<ServiceBusReceivedMessage>();

            if (!sequenceNumbers.Any()) return (messages, nonTargetMessages);

            var sequenceNumberSet = new HashSet<long>(sequenceNumbers);
            var totalScanned = 0;
            // Scan up to 10x the target count + one full batch before giving up.
            // This tolerates targets being spread across ~10% of the DLQ while
            // staying bounded enough to avoid gateway timeouts on very large queues.
            var maxScanCount = sequenceNumberSet.Count * 10 + MaxMessages;

            while (sequenceNumberSet.Count > 0 && totalScanned < maxScanCount)
            {
                var batch = await serviceBusReceiver.ReceiveMessagesAsync(MaxMessages, TimeSpan.FromSeconds(5));

                if (batch == null || batch.Count == 0)
                    break;

                totalScanned += batch.Count;

                foreach (var message in batch)
                {
                    if (sequenceNumberSet.Remove(message.SequenceNumber))
                    {
                        messages.Add(message);
                    }
                    else
                    {
                        // Keep the lock for now. Abandoning mid-scan would make the
                        // message available again immediately and, since it sits at the
                        // head of the DLQ, the next receive would re-deliver it instead
                        // of advancing - stalling the scan on the first batch. We collect
                        // non-targets here and release their locks once the scan is done.
                        nonTargetMessages.Add(message);
                    }
                }
            }

            if (sequenceNumberSet.Count > 0)
            {
                _logger.LogWarning(
                    "Scan bound reached. {UnfoundCount} of {TotalCount} target messages not found within {ScannedCount} scanned messages for queue '{QueueName}'.",
                    sequenceNumberSet.Count, sequenceNumbers.Count, totalScanned, queueName);
            }

            // Return the non-targets so the caller can abandon them AFTER it has settled
            // the targets. Abandoning here would run on the critical path before the
            // targets are resubmitted and completed, and a slow abandon phase could push a
            // target's lock past expiry, causing CompleteMessageAsync to fail and the copy
            // already sent to be re-delivered (a duplicate).
            return (messages, nonTargetMessages);
        }

        private async Task AbandonNonTargetsAsync(
            ServiceBusReceiver serviceBusReceiver,
            IReadOnlyList<ServiceBusReceivedMessage> nonTargetMessages,
            string queueName)
        {
            if (nonTargetMessages.Count == 0) return;

            // Fan out with bounded concurrency: the SDK has no batch-settle API, but the
            // receiver is thread-safe, so a bounded number of concurrent abandons avoids
            // both a long sequence of round-trips and overwhelming the entity with ~1000
            // simultaneous settlements.
            const int maxConcurrentAbandons = 16;
            using var throttle = new SemaphoreSlim(maxConcurrentAbandons);

            var abandonTasks = nonTargetMessages.Select(async message =>
            {
                await throttle.WaitAsync();
                try
                {
                    await serviceBusReceiver.AbandonMessageAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(
                        ex,
                        "Failed to abandon non-target DLQ message {SequenceNumber} on queue '{QueueName}'. The lock will expire naturally.",
                        message.SequenceNumber,
                        queueName);
                }
                finally
                {
                    throttle.Release();
                }
            });

            await Task.WhenAll(abandonTasks);
        }

        private async Task<IReadOnlyList<ServiceBusReceivedMessage>> GetAllPeekedServiceBusMessagesAsync(string queueName, int maxCount = 0)
        {
            var receiverOptions = new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter };

            await using var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);
            var receivedMessages = new List<ServiceBusReceivedMessage>();

            var previousSequenceNumber = -1L;
            var sequenceNumber = 0L;

            maxCount = maxCount == 0 ? Int32.MaxValue : maxCount;
            var remainingCount = maxCount;

            do
            {
                var peekCount = Math.Min(MaxMessages, remainingCount);
                var messageBatch = await serviceBusReceiver.PeekMessagesAsync(peekCount, sequenceNumber);
                remainingCount -= messageBatch.Count;

                if (messageBatch.Count > 0)
                {
                    // Below line of code will give element position from the end of a list
                    var sequenceNumberFromMessage = messageBatch[^1].SequenceNumber;

                    // Increasing the SequenceNumber by 1 to avoid getting the message with the same SequenceNumber twice
                    sequenceNumber = sequenceNumberFromMessage + 1;

                    if (sequenceNumber == previousSequenceNumber)
                        break;

                    receivedMessages.AddRange(messageBatch);

                    previousSequenceNumber = sequenceNumber;
                }

                if (messageBatch.Count < peekCount)
                {
                    break;
                }
            } while (remainingCount > 0);

            return receivedMessages;
        }

        private async Task<ServiceBusReceivedMessage> GetPeekedServiceBusMessageAsync(string queueName, long sequenceNumber)
        {
            var receiverOptions = new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter, ReceiveMode = ServiceBusReceiveMode.PeekLock };

            await using var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);

            return await serviceBusReceiver.PeekMessageAsync(sequenceNumber);
        }

    }
}
