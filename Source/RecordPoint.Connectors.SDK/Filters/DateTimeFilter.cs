using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using System;
using System.Globalization;

namespace RecordPoint.Connectors.SDK.Filters
{
    static class DateTimeFilter
    {
        public static bool MatchesFilter(SubmissionMetaDataModel model, SearchTermModel filter)
        {
            var fieldValue = GetDateTimeValue(model?.Value);
            var filterValue = GetDateTimeValue(filter.FieldValue);

            switch (filter.OperatorProperty)
            {
                case FilterConstants.CommonFieldOperators.Equal:
                    {
                        if (fieldValue.HasValue && filterValue.HasValue)
                        {
                            return fieldValue.Value >= filterValue.Value && fieldValue < filterValue.Value.AddHours(24);
                        }
                        else
                        {
                            return fieldValue == filterValue;
                        }
                    }
                case FilterConstants.CommonFieldOperators.NotEqual:
                    {
                        if (fieldValue.HasValue && filterValue.HasValue)
                        {
                            return fieldValue.Value < filterValue.Value || fieldValue >= filterValue.Value.AddHours(24);
                        }
                        else
                        {
                            return fieldValue != filterValue;
                        }
                    }
                case FilterConstants.CommonFieldOperators.Empty:
                    {
                        return fieldValue == null;
                    }
                case FilterConstants.CommonFieldOperators.NotEmpty:
                    {
                        return fieldValue != null;
                    }
                case FilterConstants.DateFieldOperators.Before:
                    {
                        return fieldValue < filterValue;
                    }
                case FilterConstants.DateFieldOperators.After:
                    {
                        return fieldValue > filterValue;
                    }
                default:
                    {
                        throw new NotImplementedException($"Filter for FieldName [{filter.FieldName}] has invalid OperatorProperty [{filter.OperatorProperty}]");
                    }
            }
        }

        private static DateTime? GetDateTimeValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var dateTime))
                {
                    return dateTime;
                }
                else
                {
                    throw new ValidationException($"{value} is not a valid DateTime");
                }
            }
            else
            {
                return null;
            }
        }
    }
}
