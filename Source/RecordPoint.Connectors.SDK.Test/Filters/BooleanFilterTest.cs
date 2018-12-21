using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace RecordPoint.Connectors.SDK.Test.Filters
{
    public class BooleanFilterTest
    {
        [Theory]
        [InlineData("", "true", false)]
        [InlineData("False", "true", false)]
        [InlineData(null, "true", false)]
        [InlineData("TRUE", "true", true)]
        [InlineData("true", "true", true)]
        public void MatchesBoolFilterTest(string submitValue, string fieldValue, bool expectedResult)
        {
            var expectedValue = new SubmissionMetaDataModel()
            {
                DisplayName = nameof(String),
                Name = nameof(String),
                Type = nameof(String),
                Value = submitValue
            };

            var filterValue = new SearchTermModel()
            {
                FieldName = FilterConstants.FilterFieldTypes.StringType,
                FieldType = FilterConstants.FilterFieldTypes.BooleanType,
                FieldValue = fieldValue,
                OperatorProperty = FilterConstants.CommonFieldOperators.Equal
            };

            var result = BooleanFilter.MatchesFilter(expectedValue, filterValue);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("", "true", true)]
        [InlineData("false", "true", true)]
        [InlineData(null, "true", true)]
        [InlineData("TruE", "true", false)]
        public void DoesNotMatchesBoolFilterTest(string submitValue, string fieldValue, bool expectedResult)
        {
            var expectedValue = new SubmissionMetaDataModel()
            {
                DisplayName = nameof(String),
                Name = nameof(String),
                Type = nameof(String),
                Value = submitValue
            };

            var filterValue = new SearchTermModel()
            {
                FieldName = FilterConstants.FilterFieldTypes.StringType,
                FieldType = FilterConstants.FilterFieldTypes.BooleanType,
                FieldValue = fieldValue,
                OperatorProperty = FilterConstants.CommonFieldOperators.NotEqual
            };

            var result = BooleanFilter.MatchesFilter(expectedValue, filterValue);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void OperatorPropertyEmpty_MatchesBoolFilterTest()
        {
            var expectedValue = new SubmissionMetaDataModel()
            {
                DisplayName = nameof(String),
                Name = nameof(String),
                Type = nameof(String),
                Value = nameof(String),
            };

            var filterValue = new SearchTermModel()
            {
                FieldName = FilterConstants.FilterFieldTypes.StringType,
                FieldType = FilterConstants.FilterFieldTypes.BooleanType,
                FieldValue = FilterConstants.FilterFieldTypes.StringType,
                OperatorProperty = FilterConstants.CommonFieldOperators.Empty
            };


            Assert.Throws<NotImplementedException>(() => BooleanFilter.MatchesFilter(expectedValue, filterValue));
        }

        [Fact]
        public void OperatorPropertyNull_MatchesBoolFilterTest()
        {
            var expectedValue = new SubmissionMetaDataModel()
            {
                DisplayName = nameof(String),
                Name = nameof(String),
                Type = nameof(String),
                Value = nameof(String),
            };

            var filterValue = new SearchTermModel()
            {
                FieldName = FilterConstants.FilterFieldTypes.StringType,
                FieldType = FilterConstants.FilterFieldTypes.BooleanType,
                FieldValue = FilterConstants.FilterFieldTypes.StringType,
                OperatorProperty = null
            };


            Assert.Throws<NotImplementedException>(() => BooleanFilter.MatchesFilter(expectedValue, filterValue));
        }
    }
}