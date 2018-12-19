using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Filters;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using RecordPoint.Connectors.SDK.Test.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class FilterPipelineElementTest : FilterTestBase
    {
        private FilterPipelineElement _pipelineElement;

        private Mock<ILog> _mockLog = new Mock<ILog>();
        private Mock<ISubmission> _mockNext = new Mock<ISubmission>();

        private const string _expectedValue = "expected-value-89dfae60-6c66-4885-9ef1-b70f418192f2";

        public FilterPipelineElementTest()
        {
            _pipelineElement = new FilterPipelineElement(_mockNext.Object)
            {
                Log = _mockLog.Object
            };
        }

        [Fact]
        public async Task NullFiltersIngestsAllContent()
        {
            var submitContext = GetSubmitContext();
            submitContext.Filters = null;

            await AssertNotFiltered(submitContext);
        }

        [Fact]
        public async Task NullIncludedAndExcludedIngestsAllContent()
        {
            var submitContext = GetSubmitContext();
            submitContext.Filters = new FiltersModel()
            {
                Excluded = null,
                Included = null
            };

            await AssertNotFiltered(submitContext);
        }

        [Fact]
        public async Task NullExcludedIngestsOnlyIncludedContent()
        {
            var fieldName = Guid.NewGuid().ToString();

            var filter = new FiltersModel()
            {
                Excluded = null,
                Included = new SearchTreeNodeModel()
                {
                    SearchTerm = GetSearchTermModel(fieldName)
                }
            };

            // Field not present
            await AssertFiltered(GetSubmitContext(filter));
            // Field present but does not match
            var submitContext = GetSubmitContext(filter);
            submitContext.CoreMetaData.Add(new SubmissionMetaDataModel()
            {
                Name = fieldName,
                Type = nameof(String),
                Value = Guid.NewGuid().ToString()
            });
            await AssertFiltered(submitContext);

            // Field present and matches
            submitContext = GetSubmitContext(filter);
            submitContext.CoreMetaData.Add(new SubmissionMetaDataModel()
            {
                Name = fieldName,
                Type = nameof(String),
                Value = _expectedValue
            });
            await AssertNotFiltered(submitContext);
        }

        [Fact]
        public async Task NullIncludedIngestsOnlyNonExcludedContent()
        {
            var fieldName = Guid.NewGuid().ToString();

            var filter = new FiltersModel()
            {
                Excluded = new SearchTreeNodeModel()
                {
                    SearchTerm = GetSearchTermModel(fieldName)
                },
                Included = null
            };

            // Field not present
            await AssertNotFiltered(GetSubmitContext(filter));

            // Field present but does not match
            var submitContext = GetSubmitContext(filter);
            submitContext.CoreMetaData.Add(new SubmissionMetaDataModel()
            {
                Name = fieldName,
                Type = nameof(String),
                Value = Guid.NewGuid().ToString()
            });
            await AssertNotFiltered(submitContext);

            // Field present and matches
            submitContext = GetSubmitContext(filter);
            submitContext.CoreMetaData.Add(new SubmissionMetaDataModel()
            {
                Name = fieldName,
                Type = nameof(String),
                Value = _expectedValue
            });
            await AssertFiltered(submitContext);
        }

        [Fact]
        public async Task NullFilterDoesNotThrowAndIngestsContent()
        {
            await AssertNotFiltered(GetSubmitContext(new FiltersModel()
            {
                Excluded = new SearchTreeNodeModel()
                {
                    Children = null,
                    SearchTerm = null
                },
                Included = null
            }));

            await AssertNotFiltered(GetSubmitContext(new FiltersModel()
            {
                Excluded = new SearchTreeNodeModel()
                {
                    Children = new List<SearchTreeNodeModel>(),
                    SearchTerm = null
                },
                Included = null
            }));

            await AssertNotFiltered(GetSubmitContext(new FiltersModel()
            {
                Excluded = null,
                Included = new SearchTreeNodeModel()
                {
                    Children = null,
                    SearchTerm = null
                }
            }));

            await AssertNotFiltered(GetSubmitContext(new FiltersModel()
            {
                Excluded = null,
                Included = new SearchTreeNodeModel()
                {
                    Children = new List<SearchTreeNodeModel>(),
                    SearchTerm = null
                }
            }));

            _mockLog.Verify(x => x.LogWarning(It.IsAny<Type>(), It.Is<string>(y => y == nameof(FilterPipelineElement.Submit)), It.Is<string>(y => y.Contains(nameof(ValidationException))), It.IsAny<long?>()), Times.Exactly(4));
        }

        [Fact]
        public void InvalidFilterDoesNotThrowAndIngestsContent()
        {

        }

        [Fact]
        public void InvalidBooleanOperatorDoesNotThrowAndIngestsContent()
        {

        }

        [Fact]
        public void CanMatchOnAnd()
        {

        }

        [Fact]
        public void CanMatchOnOr()
        {

        }

        [Fact]
        public void CanMatchOnNestedTerms()
        {

        }

        private async Task AssertNotFiltered(SubmitContext submitContext)
        {
            await _pipelineElement.Submit(submitContext);
            _mockNext.Verify(x => x.Submit(It.Is<SubmitContext>((sc) => sc.CorrelationId == submitContext.CorrelationId)), Times.Once);
        }

        private async Task AssertFiltered(SubmitContext submitContext)
        {
            await _pipelineElement.Submit(submitContext);
            _mockNext.Verify(x => x.Submit(It.Is<SubmitContext>((sc) => sc.CorrelationId == submitContext.CorrelationId)), Times.Never);
        }

        private SearchTermModel GetSearchTermModel(string fieldName, string expectedValue = _expectedValue)
        {
            return new SearchTermModel()
            {
                FieldName = fieldName,
                FieldType = FilterConstants.FilterFieldTypes.StringType,
                FieldValue = expectedValue,
                OperatorProperty = FilterConstants.CommonFieldOperators.Equal
            };
        }
    }
}
