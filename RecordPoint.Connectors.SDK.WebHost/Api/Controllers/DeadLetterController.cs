using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.WebHost.Controllers
{
    /// <summary>
    /// DeadLetterController
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class DeadLetterController : ControllerBase
    {
        private readonly IDeadLetterQueueService _deadLetterQueueService;

        /// <summary>
        /// Constractor for DI
        /// </summary>
        /// <param name="deadLetterQueueService"></param>
        public DeadLetterController(IDeadLetterQueueService deadLetterQueueService)
        {
            _deadLetterQueueService = deadLetterQueueService;
        }

        /// <summary>
        /// Get all Dead Letter messages by Queue 
        /// </summary>
        /// <param name="queueName"></param>        
        /// <returns></returns>
        [HttpGet("GetAllMessages")]
        public async Task<IActionResult> Get([BindRequired] string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                return BadRequest("Queue Name is required");
            }

            var deadlLetterList = await _deadLetterQueueService.GetAllMessagesAsync(queueName);

            return Ok(deadlLetterList);
        }


        /// <summary>
        /// Get Dead Letter message by Sequence number 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([BindRequired] string queueName, [BindRequired] long sequenceNumber)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                return BadRequest("Queue Name is required");
            }

            if (sequenceNumber <= 0)
            {
                return BadRequest("Sequence Number is required");
            }

            var deadLetterList = await _deadLetterQueueService.GetMessageAsync(queueName, sequenceNumber);

            return Ok(deadLetterList);
        }

        /// <summary>
        /// Post the messages based on the sequence number to requeue
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumbers"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([BindRequired] string queueName, [BindRequired] long[] sequenceNumbers)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                return BadRequest("Queue Name is required");
            }

            if (!sequenceNumbers.Any())
            {
                return BadRequest("Sequence Number(s) is required");
            }

            await _deadLetterQueueService.ResubmitMessagesAsync(queueName, sequenceNumbers);
            return Ok();
        }

        /// <summary>
        /// Requeue all the dead letter messages for a given queue name and batch size
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        [HttpPost("PostAllMessages")]
        public async Task<IActionResult> Post([BindRequired] string queueName, [BindRequired] int batchSize = 1000)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                return BadRequest("Queue Name is required");
            }

            if (batchSize > 1000)
            {
                return BadRequest("Batch size is too big");
            }

            var deadLetterList = await _deadLetterQueueService.GetAllMessagesAsync(queueName);

            if (!deadLetterList.Any())
            {
                return Ok("No dead letters found");
            }

            var sequenceNumbers = deadLetterList.Select(dlm => long.Parse(dlm.SequenceNumber)).ToArray();

            // limit size of batch
            batchSize = batchSize > 0 ? batchSize : 1000;
            var count = sequenceNumbers.Length <= batchSize ? sequenceNumbers.Length : batchSize;
            var cappedSequenceNumbers = sequenceNumbers.Take(count).ToArray();

            await _deadLetterQueueService.ResubmitMessagesAsync(queueName, cappedSequenceNumbers);
            return Ok();
        }

        /// <summary>
        /// Delete the message from the queue based on the sequence number
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([BindRequired] string queueName, [BindRequired] long sequenceNumber)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                return BadRequest("Queue Name is required");
            }

            if (sequenceNumber <= 0)
            {
                return BadRequest("Sequence Number is required");
            }

            await _deadLetterQueueService.DeleteMessageAsync(queueName, sequenceNumber);

            return Ok();
        }

        /// <summary>
        /// Delete the all messages from the queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll([BindRequired] string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                return BadRequest("Queue Name is required");
            }
            else
            {
                await _deadLetterQueueService.DeleteAllMessagesAsync(queueName);
                return Ok();
            }
        }


    }
}
