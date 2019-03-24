using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    public class ApiClientRetryPolicyTest
    {
        private Mock<ILog> _mockLog = new Mock<ILog>();

        [Fact]
        public async Task RetryPolicy_Logs()
        {
            var externalId = Guid.NewGuid().ToString();
            SubmitContext submitContext = new BinarySubmitContext
            {
                ExternalId = externalId
            };

            var policy = ApiClientRetryPolicy.GetPolicy(
                _mockLog.Object, 
                typeof(HttpSubmitBinaryPipelineElement),
                nameof(HttpSubmitBinaryPipelineElement.Submit), 
                2, 
                100, 
                CancellationToken.None, 
                submitContext.LogPrefix()
            );

            await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                await policy.ExecuteAsync(
                    async (ct) =>
                    {
                        throw new TaskCanceledException();
                    },
                    CancellationToken.None
                );
            });

            _mockLog.Verify(x => x.LogMessage(
                It.Is<Type>(t => t == typeof(HttpSubmitBinaryPipelineElement)),
                It.Is<string>(m => m == "Submit_Retry"),
                It.Is<string>(l => l.Contains(externalId)),
                It.IsAny<long?>()
            ), Times.Exactly(2));
        }
    }
}
