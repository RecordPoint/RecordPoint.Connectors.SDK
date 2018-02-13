using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class SubmitContextTest
    {
        [Fact]
        public void DefaultSubmitContext_HasNonEmptyCorrelationId()
        {
            var submitContext = new SubmitContext();
            Assert.NotEqual(Guid.Empty, submitContext.CorrelationId);
        }

        [Fact]
        public void DefaultSubmitContext_HasNonNullSubmitResult()
        {
            var submitContext = new SubmitContext();
            Assert.NotNull(submitContext.SubmitResult);
        }
    }
}
