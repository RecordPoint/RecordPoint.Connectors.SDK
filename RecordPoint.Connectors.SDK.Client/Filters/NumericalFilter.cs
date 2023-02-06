using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Filters
{
    static class NumericalFilter
    {
        public static bool MatchesFilter(SubmissionMetaDataModel model, SearchTermModel filter)
        {
            var modelValue = GetNumericalValue(model?.Value);
            var filterValue = GetNumericalValue(filter?.FieldValue);

            if (filter == null) throw new ArgumentNullException(nameof(filter));
            return filter.OperatorProperty switch
            {
                FilterConstants.CommonFieldOperators.Equal => modelValue == filterValue,
                FilterConstants.CommonFieldOperators.NotEqual => modelValue != filterValue,
                FilterConstants.CommonFieldOperators.Empty => modelValue == null,
                FilterConstants.CommonFieldOperators.NotEmpty => modelValue != null,
                FilterConstants.NumericalFieldOperators.GreaterThan => modelValue > filterValue,
                FilterConstants.NumericalFieldOperators.GreaterThanOrEqualTo => modelValue >= filterValue,
                FilterConstants.NumericalFieldOperators.LessThan => modelValue < filterValue,
                FilterConstants.NumericalFieldOperators.LessThanOrEqualTo => modelValue <= filterValue,
                _ => throw new NotImplementedException($"Filter for FieldName [{filter.FieldName}] has invalid OperatorProperty [{filter.OperatorProperty}]"),
            };
        }

        private static double? GetNumericalValue(string value)
        {
            if (value == null) return null;

            try
            {
                // use Parse instead of TryParse because TryParse will return false instead of indicating an OverflowException
                return double.Parse(value);
            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is ArgumentException)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        return null;
                    }
                    throw new ValidationException($"{value} is not a valid Numerical value");
                }
                else if (ex is OverflowException)
                {
                    throw new ValidationException($"{value} is either too large or too small");
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
