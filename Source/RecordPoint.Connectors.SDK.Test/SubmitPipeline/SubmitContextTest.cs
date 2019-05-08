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
        private const string _expectedNoExternalIdMessage = "<no external id found>";

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

        [Fact]
        public void DefaultSubmitContext_CanLogNonNullExternalId()
        {
            var submitContext = new SubmitContext()
            {
                CoreMetaData = new List<SubmissionMetaDataModel>()
            };

            var externalId = Guid.NewGuid().ToString();

            submitContext.CoreMetaData.Add(new SubmissionMetaDataModel()
            {
                Name = Fields.ExternalId,
                Type = nameof(String),
                Value = externalId
            });

            Assert.Contains(externalId, submitContext.LogPrefix());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DefaultSubmitContext_CanLogNullOrEmptyExternalId(string title)
        {
            var submitContext = new SubmitContext()
            {
                CoreMetaData = new List<SubmissionMetaDataModel>()
            };

            submitContext.CoreMetaData.Add(new SubmissionMetaDataModel()
            {
                Name = Fields.ExternalId,
                Type = nameof(String),
                Value = title
            });
            Assert.Contains(_expectedNoExternalIdMessage, submitContext.LogPrefix());
        }

        [Fact]
        public void DefaultSubmitContext_CanLogMissingExternalId()
        {
            var submitContext = new SubmitContext();
            Assert.Contains(_expectedNoExternalIdMessage, submitContext.LogPrefix());

            submitContext.CoreMetaData = new List<SubmissionMetaDataModel>();
            Assert.Contains(_expectedNoExternalIdMessage, submitContext.LogPrefix());
        }

        [Fact]
        public void BinarySubmitContext_CanLogExternalId()
        {
            var externalId = Guid.NewGuid().ToString();
            var submitContext = new BinarySubmitContext();
            submitContext.ExternalId = externalId;

            Assert.Contains(externalId, submitContext.LogPrefix());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void BinarySubmitContext_CanLogMissingOrEmptyExternalId(string title)
        {
            var submitContext = new BinarySubmitContext();
            submitContext.ExternalId = title;

            Assert.Contains(_expectedNoExternalIdMessage, submitContext.LogPrefix());
        }
    }
}
