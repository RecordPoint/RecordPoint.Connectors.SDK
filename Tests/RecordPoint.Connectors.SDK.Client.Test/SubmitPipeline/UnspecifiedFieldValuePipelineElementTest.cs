using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class UnspecifiedFieldValuePipelineElementTest
    {
        private const string Unspecified = "Unspecified";

        private readonly Mock<ISubmission> _next = new();

        [Fact]
        public async Task ItemElement_AddsMissingRequiredFields()
        {
            var context = new SubmitContext { CoreMetaData = new List<SubmissionMetaDataModel>() };
            var element = new ItemUnspecifiedFieldValuePipelineElement(_next.Object);

            await element.Submit(context);

            Assert.Contains(context.CoreMetaData, x => x.Name == Fields.Title && x.Value == Unspecified);
            Assert.Contains(context.CoreMetaData, x => x.Name == Fields.Author && x.Value == Unspecified);
            Assert.Contains(context.CoreMetaData, x => x.Name == Fields.MediaType && x.Value == Unspecified);
            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public async Task ItemElement_ReplacesEmptyRequiredFieldValues()
        {
            var context = new SubmitContext
            {
                CoreMetaData = new List<SubmissionMetaDataModel>
                {
                    new() { Name = Fields.Title, Type = nameof(String), Value = "" }
                }
            };
            var element = new ItemUnspecifiedFieldValuePipelineElement(_next.Object);

            await element.Submit(context);

            Assert.Equal(Unspecified, context.CoreMetaData.Single(x => x.Name == Fields.Title).Value);
        }

        [Fact]
        public async Task ItemElement_ReplacesWhitespaceOnlyRequiredFieldValues()
        {
            var context = new SubmitContext
            {
                CoreMetaData = new List<SubmissionMetaDataModel>
                {
                    new() { Name = Fields.Title, Type = nameof(String), Value = " " }
                }
            };
            var element = new ItemUnspecifiedFieldValuePipelineElement(_next.Object);

            await element.Submit(context);

            Assert.Equal(Unspecified, context.CoreMetaData.Single(x => x.Name == Fields.Title).Value);
        }

        [Fact]
        public async Task ItemElement_PreservesExistingRequiredFieldValues()
        {
            var context = new SubmitContext
            {
                CoreMetaData = new List<SubmissionMetaDataModel>
                {
                    new() { Name = Fields.Title, Type = nameof(String), Value = "My Title" }
                }
            };
            var element = new ItemUnspecifiedFieldValuePipelineElement(_next.Object);

            await element.Submit(context);

            Assert.Equal("My Title", context.CoreMetaData.Single(x => x.Name == Fields.Title).Value);
        }

        [Fact]
        public async Task AggregationElement_AddsMissingRequiredFields()
        {
            var context = new SubmitContext { CoreMetaData = new List<SubmissionMetaDataModel>() };
            var element = new AggregationUnspecifiedFieldValuePipelineElement(_next.Object);

            await element.Submit(context);

            Assert.Contains(context.CoreMetaData, x => x.Name == Fields.Title && x.Value == Unspecified);
            Assert.Contains(context.CoreMetaData, x => x.Name == Fields.SourceLastModifiedBy && x.Value == Unspecified);
            Assert.Contains(context.CoreMetaData, x => x.Name == Fields.SourceCreatedBy && x.Value == Unspecified);
            _next.Verify(x => x.Submit(context), Times.Once);
        }
    }
}
