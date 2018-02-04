using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace RecordPoint.Connectors.SDK.SubmitPipeline.Test
{
    public class PipelineSelectorPipelineElementTest
    {
        Mock<ISubmission> _recordSubmitMock = new Mock<ISubmission>();
        Mock<ISubmission> _folderSubmitMock = new Mock<ISubmission>();

        private void VerifyRecordSubmit()
        {
            _recordSubmitMock.Verify(x =>
                x.Submit(It.IsAny<SubmitContext>()),
                Times.Once);

            _folderSubmitMock.Verify(x =>
                x.Submit(It.IsAny<SubmitContext>()),
                Times.Never);
        }

        private void VerifyFolderSubmit()
        {
            _recordSubmitMock.Verify(x =>
                x.Submit(It.IsAny<SubmitContext>()),
                Times.Never);

            _folderSubmitMock.Verify(x =>
                x.Submit(It.IsAny<SubmitContext>()),
                Times.Once);
        }

        private void VerifyNoSubmit()
        {
            _recordSubmitMock.Verify(x =>
                x.Submit(It.IsAny<SubmitContext>()),
                Times.Never);

            _folderSubmitMock.Verify(x =>
                x.Submit(It.IsAny<SubmitContext>()),
                Times.Never);
        }

        PipelineSelectorPipelineElement SUT()
        {
            return new PipelineSelectorPipelineElement(_recordSubmitMock.Object, _folderSubmitMock.Object);
        }

        [Fact]
        public async void PipelineSelectorPipelineElementTest_Record_Submits()
        {
            var coreMetadata = new List<SubmissionMetaDataModel>()
                {
                    new SubmissionMetaDataModel()
                    {
                        Name = Fields.ItemTypeId,
                        Type = nameof(String),
                        Value = "0"
                    }
                };
            var sourceMetadata = new List<SubmissionMetaDataModel>();

            var sut = SUT();
            await sut.Submit(new SubmitContext
            {
                ConnectorConfigId = Guid.NewGuid(),
                CoreMetaData = coreMetadata,
                SourceMetaData = sourceMetadata,
                CorrelationId = Guid.Empty,
                SubmitResult = new SubmitResult()
            });

            VerifyRecordSubmit();
        }

        [Fact]
        public async void PipelineSelectorPipelineElementTest_Folder_Submits()
        {
            var coreMetadata = new List<SubmissionMetaDataModel>()
                {
                    new SubmissionMetaDataModel()
                    {
                        Name = Fields.ItemTypeId,
                        Type = nameof(String),
                        Value = "1"
                    }
                };
            var sourceMetadata = new List<SubmissionMetaDataModel>();

            var sut = SUT();
            await sut.Submit(new SubmitContext
            {
                ConnectorConfigId = Guid.NewGuid(),
                CoreMetaData = coreMetadata,
                SourceMetaData = sourceMetadata,
                CorrelationId = Guid.Empty,
                SubmitResult = new SubmitResult()
            });

            VerifyFolderSubmit();
        }

        [Fact]
        public async void PipelineSelectorPipelineElementTest_NotRecord_Submits()
        {
            var coreMetadata = new List<SubmissionMetaDataModel>()
                {
                    new SubmissionMetaDataModel()
                    {
                        Name = Fields.ItemTypeId,
                        Type = nameof(String),
                        Value = "5"
                    }
                };
            var sourceMetadata = new List<SubmissionMetaDataModel>();

            var sut = SUT();
            await sut.Submit(new SubmitContext
            {
                ConnectorConfigId = Guid.NewGuid(),
                CoreMetaData = coreMetadata,
                SourceMetaData = sourceMetadata,
                CorrelationId = Guid.Empty,
                SubmitResult = new SubmitResult()
            });

            VerifyRecordSubmit();
        }

        [Fact]
        public async void PipelineSelectorPipelineElementTest_Nothing_Submits()
        {
            var coreMetadata = new List<SubmissionMetaDataModel>();
            var sourceMetadata = new List<SubmissionMetaDataModel>();

            var sut = SUT();
            await sut.Submit(new SubmitContext
            {
                ConnectorConfigId = Guid.NewGuid(),
                CoreMetaData = coreMetadata,
                SourceMetaData = sourceMetadata,
                CorrelationId = Guid.Empty,
                SubmitResult = new SubmitResult()
            });

            VerifyNoSubmit();
        }
    }
}
