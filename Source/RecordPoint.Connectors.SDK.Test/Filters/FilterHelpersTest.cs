using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;
using System;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Filters
{
    public class FilterHelpersTest : FilterTestBase
    {
        [Fact]
        public void MatchesFilter_InvalidFieldTypeThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                FilterHelpers.MatchesFilter(
                    null, 
                    new SearchTermModel()
                    {
                        FieldName = "TestField",
                        FieldType = "TestType",
                        FieldValue = "TestValue",
                        OperatorProperty = "TestOperator"
                    }
                );
            });
        }

        [Fact]
        public void GetFieldType_InvalidFieldTypeThrowsNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() =>
            {
                FilterHelpers.GetFieldType(
                    new SearchTermModel()
                    {
                        FieldName = "TestField",
                        FieldType = "TestType",
                        FieldValue = "TestValue",
                        OperatorProperty = "TestOperator"
                    }
                );
            });
        }

        [Theory]
        [InlineData(FilterConstants.FilterFieldTypes.BooleanType, nameof(Boolean))]
        [InlineData(FilterConstants.FilterFieldTypes.DateType, nameof(DateTime))]
        [InlineData(FilterConstants.FilterFieldTypes.NumericalType, nameof(Double))]
        [InlineData(FilterConstants.FilterFieldTypes.StringType, nameof(String))]
        public void GetFieldType_RetrievesExpectedFieldType(string filterType, string expectedFieldType)
        {
            var result = FilterHelpers.GetFieldType(
                new SearchTermModel()
                {
                    FieldName = "TestField",
                    FieldType = filterType,
                    FieldValue = "TestValue",
                    OperatorProperty = "TestOperator"
                }
            );

            Assert.Equal(expectedFieldType, result);
        }

        [Fact]
        public void MatchesFilter_FieldTypeMissmatchThrowsValidationException()
        {
            var fieldName = "Test";
            var submitContext = GetSubmitContext();

            submitContext.CoreMetaData.Add(
                new SubmissionMetaDataModel()
                {
                    Name = fieldName,
                    Type = nameof(String),
                    Value = "Test"
                }
            );

            Assert.Throws<ValidationException>(() =>
            {
                FilterHelpers.MatchesFilter(
                    submitContext,
                    new SearchTermModel()
                    {
                        FieldName = fieldName,
                        FieldType = FilterConstants.FilterFieldTypes.BooleanType,
                        FieldValue = "true",
                        OperatorProperty = FilterConstants.CommonFieldOperators.Equal
                    }
                );
            });
        }

        [Fact]
        public void MatchesFilter_CanMatchOnString()
        {
            Assert.True(MatchesFilterTest(FilterConstants.FilterFieldTypes.StringType, "Test"));
        }

        [Fact(Skip = "Not Implemented yet")]
        public void MatchesFilter_CanMatchOnBoolean()
        {
            Assert.True(MatchesFilterTest(FilterConstants.FilterFieldTypes.BooleanType, "true"));
        }

        [Fact(Skip = "Not Implemented yet")]
        public void MatchesFilter_CanMatchOnDateTime()
        {
            Assert.True(MatchesFilterTest(FilterConstants.FilterFieldTypes.DateType, DateTime.UtcNow.ToString("O")));
        }

        [Fact(Skip = "Not Implemented yet")]
        public void MatchesFilter_CanMatchOnNumerical()
        {
            Assert.True(MatchesFilterTest(FilterConstants.FilterFieldTypes.NumericalType, "103"));
        }

        [Fact]
        public void GetField_CanGetCoreField()
        {
            var fieldName = Guid.NewGuid().ToString();
            var submitContext = GetSubmitContext();

            submitContext.CoreMetaData.Add(
                new SubmissionMetaDataModel()
                {
                    Name = fieldName,
                    Type = nameof(String),
                    Value = "Test"
                }
            );

            var filter = new SearchTermModel()
            {
                FieldName = fieldName,
                FieldType = nameof(String),
                FieldValue = "Test",
                OperatorProperty = FilterConstants.CommonFieldOperators.Equal
            };

            var field = FilterHelpers.GetField(submitContext, filter, nameof(String));
            Assert.Equal(fieldName, field.Name);
        }

        [Fact]
        public void GetField_CanGetSourceField()
        {
            var fieldName = Guid.NewGuid().ToString();
            var submitContext = GetSubmitContext();

            submitContext.SourceMetaData.Add(
                new SubmissionMetaDataModel()
                {
                    Name = fieldName,
                    Type = nameof(String),
                    Value = "Test"
                }
            );

            var filter = new SearchTermModel()
            {
                FieldName = GetSourceFieldName(Guid.NewGuid(), fieldName, nameof(String)),
                FieldType = nameof(String),
                FieldValue = "Test",
                OperatorProperty = FilterConstants.CommonFieldOperators.Equal
            };

            var field = FilterHelpers.GetField(submitContext, filter, nameof(String));
            Assert.Equal(fieldName, field.Name);
        }

        [Fact]
        public void GetField_CanGetFieldOfCorrectType()
        {
            var fieldName = Guid.NewGuid().ToString();
            var submitContext = GetSubmitContext();

            foreach(var fieldType in FieldTypes)
            {
                submitContext.SourceMetaData.Add(
                    new SubmissionMetaDataModel()
                    {
                        Name = fieldName,
                        Type = fieldType,
                        Value = "Test"
                    }
                );
            }

            foreach (var filterType in FilterTypes)
            {
                var fieldType = FilterHelpers.GetFieldType(new SearchTermModel() { FieldType = filterType });

                var filter = new SearchTermModel()
                {
                    FieldName = GetSourceFieldName(Guid.NewGuid(), fieldName, fieldType),
                    FieldType = filterType,
                    FieldValue = "Test",
                    OperatorProperty = FilterConstants.CommonFieldOperators.Equal
                };

                var field = FilterHelpers.GetField(submitContext, filter, fieldType);
                Assert.Equal(fieldName, field.Name);
                Assert.Equal(fieldType, field.Type);
            }
        }

        private bool MatchesFilterTest(string filterType, string fieldValue)
        {
            var fieldName = "Test";

            var filter = new SearchTermModel()
            {
                FieldName = fieldName,
                FieldType = filterType,
                FieldValue = fieldValue,
                OperatorProperty = FilterConstants.CommonFieldOperators.Equal
            };

            var submitContext = GetSubmitContext();
            submitContext.CoreMetaData.Add(
                new SubmissionMetaDataModel()
                {
                    Name = fieldName,
                    Type = FilterHelpers.GetFieldType(filter),
                    Value = fieldValue
                }
            );

            return FilterHelpers.MatchesFilter(submitContext, filter).Result;
        }
    }
}
