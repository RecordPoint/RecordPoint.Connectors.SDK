using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Filters
{
    public class FilterEdgeCaseTests
    {
        private static SearchTermModel Filter(string fieldType, string op, string value = "1") => new()
        {
            FieldName = "field",
            FieldType = fieldType,
            FieldValue = value,
            OperatorProperty = op
        };

        private static SubmissionMetaDataModel Model(string type, string value) => new()
        {
            Name = "field",
            Type = type,
            Value = value
        };

        [Fact]
        public void StringFilter_InvalidOperator_Throws()
        {
            Assert.Throws<NotImplementedException>(() =>
                StringFilter.MatchesFilter(Model(nameof(String), "x"), Filter(FilterConstants.FilterFieldTypes.StringType, "BadOperator", "x")));
        }

        [Fact]
        public void NumericalFilter_InvalidOperator_Throws()
        {
            Assert.Throws<NotImplementedException>(() =>
                NumericalFilter.MatchesFilter(Model(nameof(Double), "1"), Filter(FilterConstants.FilterFieldTypes.NumericalType, "BadOperator")));
        }

        [Fact]
        public void NumericalFilter_InvalidNumber_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() =>
                NumericalFilter.MatchesFilter(Model(nameof(Double), "not-a-number"), Filter(FilterConstants.FilterFieldTypes.NumericalType, FilterConstants.CommonFieldOperators.Equal)));
        }

        [Theory]
        [InlineData(FilterConstants.NumericalFieldOperators.GreaterThan, "5", "3", true)]
        [InlineData(FilterConstants.NumericalFieldOperators.GreaterThanOrEqualTo, "3", "3", true)]
        [InlineData(FilterConstants.NumericalFieldOperators.LessThan, "1", "3", true)]
        [InlineData(FilterConstants.NumericalFieldOperators.LessThanOrEqualTo, "3", "3", true)]
        [InlineData(FilterConstants.CommonFieldOperators.NotEqual, "5", "3", true)]
        [InlineData(FilterConstants.CommonFieldOperators.Empty, "", "3", true)]
        [InlineData(FilterConstants.CommonFieldOperators.NotEmpty, "5", "3", true)]
        public void NumericalFilter_Operators(string op, string modelValue, string filterValue, bool expected)
        {
            var result = NumericalFilter.MatchesFilter(
                Model(nameof(Double), modelValue),
                Filter(FilterConstants.FilterFieldTypes.NumericalType, op, filterValue));

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(FilterConstants.DateFieldOperators.Before, "2020-01-01", "2021-01-01", true)]
        [InlineData(FilterConstants.DateFieldOperators.After, "2022-01-01", "2021-01-01", true)]
        [InlineData(FilterConstants.CommonFieldOperators.NotEqual, "2020-01-01", "2021-01-01", true)]
        [InlineData(FilterConstants.CommonFieldOperators.Empty, "", "2021-01-01", true)]
        [InlineData(FilterConstants.CommonFieldOperators.NotEmpty, "2020-01-01", "2021-01-01", true)]
        public void DateTimeFilter_Operators(string op, string modelValue, string filterValue, bool expected)
        {
            var result = DateTimeFilter.MatchesFilter(
                Model(nameof(DateTime), modelValue),
                Filter(FilterConstants.FilterFieldTypes.DateType, op, filterValue));

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DateTimeFilter_InvalidOperator_Throws()
        {
            Assert.Throws<NotImplementedException>(() =>
                DateTimeFilter.MatchesFilter(
                    Model(nameof(DateTime), "2020-01-01"),
                    Filter(FilterConstants.FilterFieldTypes.DateType, "BadOperator", "2021-01-01")));
        }

        [Fact]
        public void DateTimeFilter_InvalidDate_ThrowsValidationException()
        {
            Assert.Throws<ValidationException>(() =>
                DateTimeFilter.MatchesFilter(
                    Model(nameof(DateTime), "not-a-date"),
                    Filter(FilterConstants.FilterFieldTypes.DateType, FilterConstants.CommonFieldOperators.Equal, "2021-01-01")));
        }

        [Fact]
        public void BooleanFilter_InvalidOperator_Throws()
        {
            Assert.Throws<NotImplementedException>(() =>
                BooleanFilter.MatchesFilter(
                    Model(nameof(Boolean), "true"),
                    Filter(FilterConstants.FilterFieldTypes.BooleanType, "BadOperator", "true")));
        }

        [Fact]
        public void IsInvalidFilterException_TrueForValidationAndNotImplemented()
        {
            Assert.True(new ValidationException("x").IsInvalidFilterException());
            Assert.True(new NotImplementedException().IsInvalidFilterException());
        }

        [Fact]
        public void IsInvalidFilterException_FalseForOtherExceptions()
        {
            Assert.False(new InvalidOperationException().IsInvalidFilterException());
        }
    }
}
