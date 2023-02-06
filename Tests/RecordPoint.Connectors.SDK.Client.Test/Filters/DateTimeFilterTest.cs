using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Filters
{
    public class DateTimeFilterTest
    {
        [Theory]
        [InlineData("2018-12-21T13:44:00.000Z", "2018-12-21T13:44:00.000Z", FilterConstants.CommonFieldOperators.Equal, true)]
        [InlineData("2019-12-21T13:44:00.000Z", "2018-12-21T13:44:00.000Z", FilterConstants.CommonFieldOperators.Equal, false)]
        [InlineData("", "2018-12-21T13:44:00.000Z", FilterConstants.CommonFieldOperators.Equal, false)]
        [InlineData(null, "2018-12-21T13:44:00.000Z", FilterConstants.CommonFieldOperators.Equal, false)]
        [InlineData("2018-12-21T13:44:00.000Z", null, FilterConstants.CommonFieldOperators.Equal, false)]
        [InlineData("", null, FilterConstants.CommonFieldOperators.Equal, true)]
        [InlineData(null, null, FilterConstants.CommonFieldOperators.Equal, true)]
        // Equal boundary conditions
        [InlineData("2019-01-01T08:00:00.000Z", "2019-01-01T00:00:00.000-8", FilterConstants.CommonFieldOperators.Equal, true)]
        [InlineData("2019-01-01T07:59:59.000Z", "2019-01-01T00:00:00.000-8", FilterConstants.CommonFieldOperators.Equal, false)]
        [InlineData("2019-01-02T07:59:59.000Z", "2019-01-01T00:00:00.000-8", FilterConstants.CommonFieldOperators.Equal, true)]
        [InlineData("2019-01-02T08:00:00.000Z", "2019-01-01T00:00:00.000-8", FilterConstants.CommonFieldOperators.Equal, false)]
        [InlineData("2019-12-21T13:44:00.000Z", "2018-12-21T13:44:00.000Z", FilterConstants.CommonFieldOperators.NotEqual, true)]
        [InlineData("2018-12-21T13:44:00.000Z", "2018-12-21T13:44:00.000Z", FilterConstants.CommonFieldOperators.NotEqual, false)]
        [InlineData("", "2018-12-21T13:44:00.000Z", FilterConstants.CommonFieldOperators.NotEqual, true)]
        [InlineData(null, "2018-12-21T13:44:00.000Z", FilterConstants.CommonFieldOperators.NotEqual, true)]
        // NotEqual boundary conditions
        [InlineData("2019-01-01T08:00:00.000Z", "2019-01-01T00:00:00.000-8", FilterConstants.CommonFieldOperators.NotEqual, false)]
        [InlineData("2019-01-01T07:59:59.000Z", "2019-01-01T00:00:00.000-8", FilterConstants.CommonFieldOperators.NotEqual, true)]
        [InlineData("2019-01-02T07:59:59.000Z", "2019-01-01T00:00:00.000-8", FilterConstants.CommonFieldOperators.NotEqual, false)]
        [InlineData("2019-01-02T08:00:00.000Z", "2019-01-01T00:00:00.000-8", FilterConstants.CommonFieldOperators.NotEqual, true)]
        [InlineData(null, null, FilterConstants.CommonFieldOperators.Empty, true)]
        [InlineData("", null, FilterConstants.CommonFieldOperators.Empty, true)]
        [InlineData("2018-12-21T13:44:00.000Z", null, FilterConstants.CommonFieldOperators.Empty, false)]
        [InlineData("", null, FilterConstants.CommonFieldOperators.NotEmpty, false)]
        [InlineData(null, null, FilterConstants.CommonFieldOperators.NotEmpty, false)]
        [InlineData("2018-12-21T13:44:00.000Z", null, FilterConstants.CommonFieldOperators.NotEmpty, true)]
        [InlineData("2019-12-21T13:44:00.000Z", "2018-12-21T13:44:00.000Z", FilterConstants.DateFieldOperators.After, true)]
        [InlineData("2017-12-21T13:44:00.000Z", "2018-12-21T13:44:00.000Z", FilterConstants.DateFieldOperators.After, false)]
        [InlineData("2019-12-21T13:44:00.000Z", "2018-12-21T13:44:00.000Z", FilterConstants.DateFieldOperators.Before, false)]
        [InlineData("2017-12-21T13:44:00.000Z", "2018-12-21T13:44:00.000Z", FilterConstants.DateFieldOperators.Before, true)]
        public void ValidDateTimeFilterTests(string submitValue, string filterFieldValue, string operatorType, bool expectedResult)
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
                FieldType = FilterConstants.FilterFieldTypes.DateType,
                FieldValue = filterFieldValue,
                OperatorProperty = operatorType
            };

            var result = DateTimeFilter.MatchesFilter(expectedValue, filterValue);

            Assert.Equal(expectedResult, result);
        }

        // Invalid filters

        [Theory]
        [InlineData(null, "2018-12-21T13:44:00.000Z", FilterConstants.StringFieldOperators.Contains)]
        [InlineData(null, "2018-12-21T13:44:00.000Z", FilterConstants.StringFieldOperators.StartsWith)]
        public void InvalidOperatorDateTimeFilterTests(string submitValue, string filterFieldValue, string operatorType)
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
                FieldType = FilterConstants.FilterFieldTypes.DateType,
                FieldValue = filterFieldValue,
                OperatorProperty = operatorType
            };

            Assert.Throws<NotImplementedException>(() => DateTimeFilter.MatchesFilter(expectedValue, filterValue));
        }

        [Theory]
        [InlineData("2018-12-21T13:44:00.000Z", "abcdefg", FilterConstants.CommonFieldOperators.Equal)]
        [InlineData("abcdefg", "2018-12-21T13:44:00.000Z", FilterConstants.CommonFieldOperators.Equal)]
        public void InvalidDateTimeFilterTests(string submitValue, string filterFieldValue, string operatorType)
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
                FieldType = FilterConstants.FilterFieldTypes.DateType,
                FieldValue = filterFieldValue,
                OperatorProperty = operatorType
            };

            Assert.Throws<ValidationException>(() => DateTimeFilter.MatchesFilter(expectedValue, filterValue));
        }
    }
}
