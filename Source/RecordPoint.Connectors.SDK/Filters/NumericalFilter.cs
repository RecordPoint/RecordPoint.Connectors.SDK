using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using System;

namespace RecordPoint.Connectors.SDK.Filters
{
    static class NumericalFilter
    {
        public static bool MatchesFilter(SubmissionMetaDataModel model, SearchTermModel filter)
        {
            var modelValue = GetNumericalValue(model?.Value);
            var filterValue = GetNumericalValue(filter?.FieldValue);

            switch (filter.OperatorProperty)
            {
                case FilterConstants.CommonFieldOperators.Equal:
                    return modelValue == filterValue;
                case FilterConstants.CommonFieldOperators.NotEqual:
                    return modelValue != filterValue;
                case FilterConstants.CommonFieldOperators.Empty:
                    return modelValue == null;
                case FilterConstants.CommonFieldOperators.NotEmpty:
                    return modelValue != null;
                case FilterConstants.NumericalFieldOperators.GreaterThan:
                    return modelValue > filterValue;
                case FilterConstants.NumericalFieldOperators.GreaterThanOrEqualTo:
                    return modelValue >= filterValue;
                case FilterConstants.NumericalFieldOperators.LessThan:
                    return modelValue < filterValue;
                case FilterConstants.NumericalFieldOperators.LessThanOrEqualTo:
                    return modelValue <= filterValue;
                default: throw new NotImplementedException($"Filter for FieldName [{filter.FieldName}] has invalid OperatorProperty [{filter.OperatorProperty}]");
            }
        }

        private static double? GetNumericalValue(string value)
        {
            try
            {
                // use Parse instead of TryParse because TryParse will return false instead of indicating an OverflowException
                var result = double.Parse(value);
                return result;
            }
            catch(Exception ex)
            {
                if (ex is FormatException || ex is ArgumentException)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        return null;
                    }
                    throw new ValidationException($"{value} is not a valid Numerical value");
                }
                else if(ex is OverflowException)
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
