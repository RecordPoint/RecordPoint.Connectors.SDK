using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Collections.Generic;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class SubmitContextTest
    {
        private const string _expectedNoTitleMessage = "<no title found>";

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

        [Fact]
        public void DefaultSubmitContext_CanLogNonNullTitle()
        {
            var submitContext = new SubmitContext()
            {
                CoreMetaData = new List<SubmissionMetaDataModel>()
            };

            var title = Guid.NewGuid().ToString();

            submitContext.CoreMetaData.Add(new SubmissionMetaDataModel()
            {
                Name = Fields.Title,
                Type = nameof(String),
                Value = title
            });

            Assert.Contains(title, submitContext.LogPrefix());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DefaultSubmitContext_CanLogNullOrEmptyTitle(string title)
        {
            var submitContext = new SubmitContext()
            {
                CoreMetaData = new List<SubmissionMetaDataModel>()
            };

            submitContext.CoreMetaData.Add(new SubmissionMetaDataModel()
            {
                Name = Fields.Title,
                Type = nameof(String),
                Value = title
            });

            Assert.Contains(_expectedNoTitleMessage, submitContext.LogPrefix());
        }


        [Fact]
        public void DefaultSubmitContext_CanLogMissingTitle()
        {
            var submitContext = new SubmitContext();
            Assert.Contains(_expectedNoTitleMessage, submitContext.LogPrefix());

            submitContext.CoreMetaData = new List<SubmissionMetaDataModel>();
            Assert.Contains(_expectedNoTitleMessage, submitContext.LogPrefix());
        }

        [Fact]
        public void BinarySubmitContext_CanLogTitle()
        {
            var title = Guid.NewGuid().ToString();
            var submitContext = new BinarySubmitContext();
            submitContext.FileName = title;

            Assert.Contains(title, submitContext.LogPrefix());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void BinarySubmitContext_CanLogMissingOrEmptyTitle(string title)
        {
            var submitContext = new BinarySubmitContext();
            submitContext.FileName = title;

            Assert.Contains(_expectedNoTitleMessage, submitContext.LogPrefix());
        }
    }
}
