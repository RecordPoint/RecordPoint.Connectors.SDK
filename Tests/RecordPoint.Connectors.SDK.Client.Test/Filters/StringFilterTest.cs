using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Filters;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Filters
{
    public class StringFilterTest : FilterTestBase
    {
        [Theory]
        [InlineData("Test", "Test", FilterConstants.CommonFieldOperators.Equal)]
        [InlineData("Test", "TEST", FilterConstants.CommonFieldOperators.Equal)]
        [InlineData("Test", "abcd", FilterConstants.CommonFieldOperators.NotEqual)]
        [InlineData("Test", "ABCD", FilterConstants.CommonFieldOperators.NotEqual)]
        [InlineData("", "", FilterConstants.CommonFieldOperators.Empty)]
        [InlineData("", "abcd", FilterConstants.CommonFieldOperators.NotEmpty)]
        [InlineData("", "ABCD", FilterConstants.CommonFieldOperators.NotEmpty)]
        [InlineData("contains", "string contains", FilterConstants.StringFieldOperators.Contains)]
        [InlineData("contains", "string CONTAINS", FilterConstants.StringFieldOperators.Contains)]
        [InlineData("contains", "string", FilterConstants.StringFieldOperators.NotContains)]
        [InlineData("contains", "STRING", FilterConstants.StringFieldOperators.NotContains)]
        [InlineData("xyz", "uvwxyz", FilterConstants.StringFieldOperators.EndsWith)]
        [InlineData("xyz", "UVWXYZ", FilterConstants.StringFieldOperators.EndsWith)]
        [InlineData("xyz", "abcdef", FilterConstants.StringFieldOperators.NotEndsWith)]
        [InlineData("xyz", "ABCDEF", FilterConstants.StringFieldOperators.NotEndsWith)]
        [InlineData("abc", "abcefgh", FilterConstants.StringFieldOperators.StartsWith)]
        [InlineData("abc", "ABCEFGH", FilterConstants.StringFieldOperators.StartsWith)]
        [InlineData("abc", "zxyerf", FilterConstants.StringFieldOperators.NotStartsWith)]
        [InlineData("abc", "ZXYERF", FilterConstants.StringFieldOperators.NotStartsWith)]
        public void MatchesFilters_StringType_DifferentOperators_ResultShouldBe_True(string filterValue, string modelValue, string operatorType)
        {
            Assert.True(MatchesFilterTest(filterValue, modelValue, operatorType));
        }

        [Theory]
        [InlineData("Test", "Test1", FilterConstants.CommonFieldOperators.Equal)]
        [InlineData("Test", "TEST1", FilterConstants.CommonFieldOperators.Equal)]
        [InlineData("Test", "Test", FilterConstants.CommonFieldOperators.NotEqual)]
        [InlineData("TEST", "Test", FilterConstants.CommonFieldOperators.NotEqual)]
        [InlineData("", "abcd", FilterConstants.CommonFieldOperators.Empty)]
        [InlineData("", "ABCD", FilterConstants.CommonFieldOperators.Empty)]
        [InlineData("", "", FilterConstants.CommonFieldOperators.NotEmpty)]
        [InlineData("contains", "string", FilterConstants.StringFieldOperators.Contains)]
        [InlineData("contains", "STRING", FilterConstants.StringFieldOperators.Contains)]
        [InlineData("CONTAINS", "contains", FilterConstants.StringFieldOperators.NotContains)]
        [InlineData("contains", "CONTAINS", FilterConstants.StringFieldOperators.NotContains)]
        [InlineData("xyz", "uvabc", FilterConstants.StringFieldOperators.EndsWith)]
        [InlineData("xyz", "uvABC", FilterConstants.StringFieldOperators.EndsWith)]
        [InlineData("xyz", "abcxyz", FilterConstants.StringFieldOperators.NotEndsWith)]
        [InlineData("xyz", "abcXYZ", FilterConstants.StringFieldOperators.NotEndsWith)]
        [InlineData("abc", "efghij", FilterConstants.StringFieldOperators.StartsWith)]
        [InlineData("abc", "EFGHIJ", FilterConstants.StringFieldOperators.StartsWith)]
        [InlineData("abc", "abcerf", FilterConstants.StringFieldOperators.NotStartsWith)]
        [InlineData("abc", "ABCerf", FilterConstants.StringFieldOperators.NotStartsWith)]
        public void MatchesFilters_StringType_DifferentOperators_ResultShouldBe_False(string filterValue, string modelValue, string operatorType)
        {
            Assert.False(MatchesFilterTest(filterValue, modelValue, operatorType));
        }


        private static bool MatchesFilterTest(string filterfieldValue, string modelValue, string operatorProperty)
        {
            var fieldName = "Test";

            var filter = new SearchTermModel()
            {
                FieldName = fieldName,
                FieldType = FilterConstants.FilterFieldTypes.StringType,
                FieldValue = filterfieldValue,
                OperatorProperty = operatorProperty
            };

            var model = new SubmissionMetaDataModel()
            {
                Name = fieldName,
                Type = FilterHelpers.GetFieldType(filter),
                Value = modelValue
            };

            return StringFilter.MatchesFilter(model, filter);
        }
    }
}
