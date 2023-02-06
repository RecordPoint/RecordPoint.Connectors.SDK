using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Filters
{
    public class NumericalFilterTest
    {
        [Theory]
        [InlineData("1.5", "1.50", FilterConstants.CommonFieldOperators.Equal, true)]
        [InlineData("1.5E+10", "15000000000", FilterConstants.CommonFieldOperators.Equal, true)]
        [InlineData("1,50", "150", FilterConstants.CommonFieldOperators.Equal, true)]
        [InlineData(null, null, FilterConstants.CommonFieldOperators.Equal, true)]
        [InlineData("0", "1", FilterConstants.CommonFieldOperators.NotEqual, true)]
        [InlineData("", null, FilterConstants.CommonFieldOperators.Empty, true)]
        [InlineData(null, "", FilterConstants.CommonFieldOperators.Empty, true)]
        [InlineData("1", "", FilterConstants.CommonFieldOperators.Empty, false)]
        [InlineData("1", null, FilterConstants.CommonFieldOperators.NotEmpty, true)]
        [InlineData(null, null, FilterConstants.CommonFieldOperators.NotEmpty, false)]
        [InlineData("1", "0", FilterConstants.NumericalFieldOperators.GreaterThan, true)]
        [InlineData("1", "1", FilterConstants.NumericalFieldOperators.GreaterThan, false)]
        [InlineData("1", "0", FilterConstants.NumericalFieldOperators.GreaterThanOrEqualTo, true)]
        [InlineData("0", "1", FilterConstants.NumericalFieldOperators.LessThan, true)]
        [InlineData("0", "0", FilterConstants.NumericalFieldOperators.LessThan, false)]
        [InlineData("0", "1", FilterConstants.NumericalFieldOperators.LessThanOrEqualTo, true)]
        [InlineData(null, null, FilterConstants.NumericalFieldOperators.LessThanOrEqualTo, false)]
        public void ValidNumericalFilterTest(string modelValue, string filterValue, string operatorType, bool expected)
        {
            var submission = GetSubmissionModel(modelValue);
            var filter = GetFilterModel(filterValue, operatorType);

            var result = NumericalFilter.MatchesFilter(submission, filter);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("a", "", FilterConstants.CommonFieldOperators.Equal, "a is not a valid Numerical value", typeof(ValidationException))]
        [InlineData("1", "1", FilterConstants.DateFieldOperators.After, "Filter for FieldName [FieldName] has invalid OperatorProperty [After]", typeof(NotImplementedException))]
        public void InvalidNumericalFilterThrowsException(string modelValue, string filterValue, string operatorType, string expectedMessage, Type exceptionType)
        {
            var submission = GetSubmissionModel(modelValue);
            var filter = GetFilterModel(filterValue, operatorType);

            var exception = Assert.Throws(exceptionType, () => NumericalFilter.MatchesFilter(submission, filter));
            Assert.Equal(expectedMessage, exception.Message);
        }

        private SubmissionMetaDataModel GetSubmissionModel(string value)
        {
            return new SubmissionMetaDataModel()
            {
                DisplayName = nameof(SubmissionMetaDataModel.DisplayName),
                Name = nameof(SubmissionMetaDataModel.Name),
                Type = nameof(Double),
                Value = value
            }; ;
        }

        private static SearchTermModel GetFilterModel(string filterValue, string operatorType)
        {
            return new SearchTermModel()
            {
                FieldName = nameof(SearchTermModel.FieldName),
                FieldType = nameof(FilterConstants.FilterFieldTypes.NumericalType),
                FieldValue = filterValue,
                OperatorProperty = operatorType
            };
        }
    }
}
