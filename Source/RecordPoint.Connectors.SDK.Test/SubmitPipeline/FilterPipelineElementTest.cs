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
            var fieldName = GetTestString();

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
                Value = GetTestString()
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
            var fieldName = GetTestString();

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
                Value = GetTestString()
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
        public async Task InvalidBooleanOperatorDoesNotThrowAndIngestsContent()
        {
            await AssertNotFiltered(GetSubmitContext(new FiltersModel()
            {
                Excluded = null,
                Included = new SearchTreeNodeModel()
                {
                    Children = new List<SearchTreeNodeModel>() { new SearchTreeNodeModel() },
                    SearchTerm = null,
                    BoolOperator = "NotImplementedOperator"
                }
            }));

            await AssertNotFiltered(GetSubmitContext(new FiltersModel()
            {
                Excluded = new SearchTreeNodeModel()
                {
                    Children = new List<SearchTreeNodeModel>() { new SearchTreeNodeModel() },
                    SearchTerm = null,
                    BoolOperator = "NotImplementedOperator"
                },
                Included = null
            }));

            _mockLog.Verify(x => x.LogWarning(It.IsAny<Type>(), It.Is<string>(y => y == nameof(FilterPipelineElement.Submit)), It.Is<string>(y => y.Contains(nameof(NotImplementedException))), It.IsAny<long?>()), Times.Exactly(2));
        }

        [Theory]
        [MemberData(nameof(CanMatchExcludedOnAndTestData))]
        public async Task CanMatchExcludedOnAnd(SubmitContext submitContext, bool expectFilter)
        {
            await AssertFiltered(submitContext, expectFilter);
        }

        [Theory]
        [MemberData(nameof(CanMatchExcludedOnAndTestData))]
        public async Task CanMatchIncludedAndTestData(SubmitContext submitContext, bool expectFilter)
        {
            // Test data is designed for an excluded test
            // Move the filter from the test data to the included, reverse the expected
            submitContext.Filters.Included = submitContext.Filters.Excluded;
            submitContext.Filters.Excluded = null;

            await AssertFiltered(submitContext, !expectFilter);
        }

        public static IEnumerable<object[]> CanMatchExcludedOnAndTestData()
        {
            // Fields not present - Expect item to not be filtered
            yield return new object[] { GetSubmitContext(3, 0, FilterConstants.FilterBooleanOperators.And), false };
            // One of the three Fields present - Expect item to not be filtered
            yield return new object[] { GetSubmitContext(3, 1, FilterConstants.FilterBooleanOperators.And), false };
            // Only two of three Fields present - Expect item to not be filtered
            yield return new object[] { GetSubmitContext(3, 2, FilterConstants.FilterBooleanOperators.And), false };
            // All three Fields present - Expect item to be filtered
            yield return new object[] { GetSubmitContext(3, 3, FilterConstants.FilterBooleanOperators.And), true };
            // All three Fields present, but one field has a value which does not match - Expect item to not be filtered
            var submitContext = GetSubmitContext(3, 3, FilterConstants.FilterBooleanOperators.And);
            yield return new object[] { AddOrUpdateCoreMetadata(submitContext, submitContext.CoreMetaData.Last().Name, "Test"), false };
        }

        [Theory]
        [MemberData(nameof(CanMatchExcludedOnOrTestData))]
        public async Task CanMatchExcludedOnOr(SubmitContext submitContext, bool expectFilter)
        { 
            await AssertFiltered(submitContext, expectFilter);
        }

        [Theory]
        [MemberData(nameof(CanMatchExcludedOnOrTestData))]
        public async Task CanMatchIncludedOnOr(SubmitContext submitContext, bool expectFilter)
        {
            // Test data is designed for an excluded test
            // Move the filter from the test data to the included, reverse the expected
            submitContext.Filters.Included = submitContext.Filters.Excluded;
            submitContext.Filters.Excluded = null;

            await AssertFiltered(submitContext, !expectFilter);
        }

        public static IEnumerable<object[]> CanMatchExcludedOnOrTestData()
        {
            // Fields not present - Expect item to not be filtered
            yield return new object[] { GetSubmitContext(3, 0, FilterConstants.FilterBooleanOperators.Or), false };
            // One of the fields present, but does not match - Expect item to not be filtered 
            var onePresentDoesNotMatch = GetSubmitContext(3, 1, FilterConstants.FilterBooleanOperators.Or);
            yield return new object[] { AddOrUpdateCoreMetadata(onePresentDoesNotMatch, onePresentDoesNotMatch.CoreMetaData.Last().Name, "Test"), false };
            // One of the three Fields present - Expect item to be filtered
            yield return new object[] { GetSubmitContext(3, 1, FilterConstants.FilterBooleanOperators.Or), true };
            // Two of the fields present, but one does not match - Expect item to be filtered 
            var twoPresentDoesNotMatch = GetSubmitContext(3, 2, FilterConstants.FilterBooleanOperators.Or);
            yield return new object[] { AddOrUpdateCoreMetadata(twoPresentDoesNotMatch, twoPresentDoesNotMatch.CoreMetaData.Last().Name, "Test"), true };
            // Two of the three Fields present - Expect item to be filtered 
            yield return new object[] { GetSubmitContext(3, 2, FilterConstants.FilterBooleanOperators.Or), true };
            // All of the three Fields present - Expect item to be filtered 
            yield return new object[] { GetSubmitContext(3, 2, FilterConstants.FilterBooleanOperators.Or), true };
        }

        [Fact]
        public async Task CanMatchOnNestedTermsSearchTermOrOr()
        {
            // Proves our ability to match on a OR (b OR c)
            var a = GetTestKeyValuePair();
            var b = GetTestKeyValuePair();
            var c = GetTestKeyValuePair();

            var filter = new SearchTreeNodeModel()
            {
                BoolOperator = FilterConstants.FilterBooleanOperators.Or,
                Children = new List<SearchTreeNodeModel>()
                {
                    GetSearchTreeNodeModel(a.Key, a.Value),
                    GetSearchTreeNodeModel(FilterConstants.FilterBooleanOperators.Or, new List<KeyValuePair<string, string>>(){ b, c })
                }
            };

            // Should be excluded / excluded if we add any one of these to the metadata
            var filteredContext = GetSubmitContext(new FiltersModel() { Excluded = filter });
            await AssertFiltered(filteredContext, false);
            await AssertFiltered(AddOrUpdateCoreMetadata(filteredContext, a.Key, a.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(filteredContext, b.Key, b.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(filteredContext, c.Key, c.Value), true);

            var unfilteredContext = GetSubmitContext(new FiltersModel() { Included = filter });
            await AssertFiltered(unfilteredContext, true);
            await AssertFiltered(AddOrUpdateCoreMetadata(unfilteredContext, a.Key, a.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(unfilteredContext, b.Key, b.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(unfilteredContext, c.Key, c.Value), false);
        }


        [Fact]
        public async Task CanMatchOnNestedTermsSearchTermOrAnd()
        {
            // Proves our ability to match on a OR (b AND c)
            var a = GetTestKeyValuePair();
            var b = GetTestKeyValuePair();
            var c = GetTestKeyValuePair();

            var filter = new SearchTreeNodeModel()
            {
                BoolOperator = FilterConstants.FilterBooleanOperators.Or,
                Children = new List<SearchTreeNodeModel>()
                {
                    GetSearchTreeNodeModel(a.Key, a.Value),
                    GetSearchTreeNodeModel(FilterConstants.FilterBooleanOperators.And, new List<KeyValuePair<string, string>>(){ b, c })
                }
            };

            // Should be excluded / excluded if we add either a or both b and c
            var submitContext = GetSubmitContext(new FiltersModel() { Excluded = filter });
            await AssertFiltered(submitContext, false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, a.Key, a.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), true);

            submitContext = GetSubmitContext(new FiltersModel() { Excluded = filter });
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), true);

            submitContext = GetSubmitContext(new FiltersModel() { Included = filter });
            await AssertFiltered(submitContext, true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, a.Key, a.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), false);

            submitContext = GetSubmitContext(new FiltersModel() { Included = filter });
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), false);
        }

        [Fact]
        public async Task CanMatchOnNestedTermsSearchTermAndOr()
        {
            // Proves our ability to match on a AND (b OR c)
            var a = GetTestKeyValuePair();
            var b = GetTestKeyValuePair();
            var c = GetTestKeyValuePair();

            var filter = new SearchTreeNodeModel()
            {
                BoolOperator = FilterConstants.FilterBooleanOperators.And,
                Children = new List<SearchTreeNodeModel>()
                {
                    GetSearchTreeNodeModel(a.Key, a.Value),
                    GetSearchTreeNodeModel(FilterConstants.FilterBooleanOperators.Or, new List<KeyValuePair<string, string>>(){ b, c })
                }
            };

            // Should be included / excluded if we add a and one of b or c
            var submitContext = GetSubmitContext(new FiltersModel() { Excluded = filter });
            await AssertFiltered(submitContext, false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, a.Key, a.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), true);

            submitContext = GetSubmitContext(new FiltersModel() { Excluded = filter });
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), false);

            submitContext = GetSubmitContext(new FiltersModel() { Included = filter });
            await AssertFiltered(submitContext, true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, a.Key, a.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), false);

            submitContext = GetSubmitContext(new FiltersModel() { Included = filter });
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), true);
        }

        [Fact]
        public async Task CanMatchOnNestedTermsSearchTermAndAnd()
        {
            // Proves our ability to match on a AND (b AND c)
            var a = GetTestKeyValuePair();
            var b = GetTestKeyValuePair();
            var c = GetTestKeyValuePair();

            var filter = new SearchTreeNodeModel()
            {
                BoolOperator = FilterConstants.FilterBooleanOperators.And,
                Children = new List<SearchTreeNodeModel>()
                {
                    GetSearchTreeNodeModel(a.Key, a.Value),
                    GetSearchTreeNodeModel(FilterConstants.FilterBooleanOperators.And, new List<KeyValuePair<string, string>>(){ b, c })
                }
            };

            // Shouldn't exclude / include until all are present
            var submitContext = GetSubmitContext(new FiltersModel() { Excluded = filter });
            await AssertFiltered(submitContext, false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, a.Key, a.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), false);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), true);

            submitContext = GetSubmitContext(new FiltersModel() { Included = filter });
            await AssertFiltered(submitContext, true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, a.Key, a.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, b.Key, b.Value), true);
            await AssertFiltered(AddOrUpdateCoreMetadata(submitContext, c.Key, c.Value), false);
        }

        private async Task AssertFiltered(SubmitContext submitContext, bool expectFiltered)
        {
            // We might reuse the same submit context id in the same run - Let's just reset
            // the invocations before we do a verify
            _mockNext.ResetCalls();

            if (expectFiltered)
            {
                await AssertFiltered(submitContext);
            }
            else
            {
                await AssertNotFiltered(submitContext);
            }
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

        private static SearchTermModel GetSearchTermModel(string fieldName, string expectedValue = _expectedValue)
        {
            return new SearchTermModel()
            {
                FieldName = fieldName,
                FieldType = FilterConstants.FilterFieldTypes.StringType,
                FieldValue = expectedValue,
                OperatorProperty = FilterConstants.CommonFieldOperators.Equal
            };
        }

        private static SearchTreeNodeModel GetSearchTreeNodeModel(string fieldName, string expectedValue = _expectedValue)
        {
            return new SearchTreeNodeModel()
            {
                SearchTerm = GetSearchTermModel(fieldName, expectedValue)
            };
        }

        private static SearchTreeNodeModel GetSearchTreeNodeModel(string boolOperator, IEnumerable<KeyValuePair<string, string>> fieldNamesAndExpectedValues)
        {
            return new SearchTreeNodeModel()
            {
                BoolOperator = boolOperator,
                Children = fieldNamesAndExpectedValues.Select(x => GetSearchTreeNodeModel(x.Key, x.Value)).ToList()
            };
        }

        private static SubmitContext GetSubmitContext(int numberOfFields, int numberOfMatchingFields, string boolOperator)
        {
            var fieldNamesAndValues = new Dictionary<string, string>();
            for (int i = 0; i < numberOfFields; i++)
            {
                fieldNamesAndValues.Add(GetTestString(), GetTestString());
            }

            var filter = new FiltersModel()
            {
                Excluded = GetSearchTreeNodeModel(boolOperator, fieldNamesAndValues)
            };

            var submitContext = GetSubmitContext(filter);
            return AddOrUpdateCoreMetadata(submitContext, fieldNamesAndValues.Take(numberOfMatchingFields));
        }

        private static SubmitContext AddOrUpdateCoreMetadata(SubmitContext submitContext, string fieldName, string fieldValue, string fieldType = nameof(String))
        {
            submitContext.CoreMetaData.AddOrUpdate(new SubmissionMetaDataModel()
            {
                Name = fieldName,
                Type = fieldType,
                Value = fieldValue
            });

            return submitContext;
        }

        private static SubmitContext AddOrUpdateCoreMetadata(SubmitContext submitContext, IEnumerable<KeyValuePair<string, string>> fieldKeysAndValues, string fieldType = nameof(String))
        {
            foreach(var kvp in fieldKeysAndValues)
            {
                submitContext = AddOrUpdateCoreMetadata(submitContext, kvp.Key, kvp.Value, fieldType);
            }

            return submitContext;
        }
    }
}
