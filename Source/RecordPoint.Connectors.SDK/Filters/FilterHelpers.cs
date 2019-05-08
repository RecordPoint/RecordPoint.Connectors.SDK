using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Linq;

namespace RecordPoint.Connectors.SDK.Filters
{
    internal static class FilterHelpers
    {
        private const char _separator = '|';
        private const string _sourceFieldPrefix = "S|";
        private const int _indexAfterSeparator = 39;

        public static MatchResult MatchesFilter(SubmitContext submitContext, SearchTermModel filter)
        {
            var fieldType = GetFieldType(filter);
            var field = GetField(submitContext, filter, fieldType);

            if (field != null && field.Type != fieldType)
            {
                throw new ValidationException($"Field [{field.Name}] has FieldType [{field.Type}] and does not match Filter's FieldType [{filter.FieldType}]");
            }

            var result = false;

            switch (filter.FieldType)
            {
                case FilterConstants.FilterFieldTypes.BooleanType:
                    {
                        result = BooleanFilter.MatchesFilter(field, filter);
                        break;
                    }
                case FilterConstants.FilterFieldTypes.DateType:
                    {
                        result = DateTimeFilter.MatchesFilter(field, filter);
                        break;
                    }
                case FilterConstants.FilterFieldTypes.NumericalType:
                    {
                        result = NumericalFilter.MatchesFilter(field, filter);
                        break;
                    }
                case FilterConstants.FilterFieldTypes.StringType:
                    {
                        result =  StringFilter.MatchesFilter(field, filter);
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException($"Filter for FieldName [{filter.FieldName}] has invalid FieldType [{filter.FieldType}]");
                    }
            }

            if(!result)
            {
                return new MatchResult() {
                    Result = false,
                    MatchReason = $"Field [{field?.Name ?? "<Null>"}] " +
                        $"Value [{field?.Value ?? "<Null>"}] " +
                        $"failed to match Filter: " +
                        $"Field Name {filter.FieldName} " +
                        $"Type [{filter.FieldType}] " +
                        $"Operator [{filter.OperatorProperty}] " +
                        $"Value [{filter?.FieldValue ?? "<Null>"}]"
                };
            }

            return new MatchResult()
            {
                Result = true,
                MatchReason = $"Field [{field?.Name ?? "<Null>"}] " +
                    $"Value [{field?.Value ?? "<Null>"}] " +
                    $"matched Filter: " +
                    $"Field Name {filter.FieldName} " +
                    $"Type [{filter.FieldType}] " +
                    $"Operator [{filter.OperatorProperty}] " +
                    $"Value [{filter?.FieldValue ?? "<Null>"}]"
            };
        }

        public static SubmissionMetaDataModel GetField(SubmitContext submitContext, SearchTermModel filter, string expectedFieldType)
        {
            // Format of a source field name in Records365: S|<ConnectorTypeId>|<DataType>|<FieldName>
            if (filter.FieldName.StartsWith(_sourceFieldPrefix))
            {
                // Avoiding String.Split due to performance concerns. Not using LastIndexOf in case a Field name contains a pipe.
                // We want to skip over S|<ConnectorTypeId>|, which has a known length (39 characters), then find the index of the first pipe following that. 
                // After that pipe, the rest should be the Source property's field name
                var lastPipeIndex = filter.FieldName.IndexOf(_separator, _indexAfterSeparator);
                var sourceFieldName = filter.FieldName.Substring(lastPipeIndex + 1);

                return submitContext?.SourceMetaData?.FirstOrDefault(x => x.Name == sourceFieldName && x.Type == expectedFieldType);
            }

            return submitContext?.CoreMetaData?.FirstOrDefault(x => x.Name == filter.FieldName);
        }

        public static string GetFieldType(SearchTermModel filter)
        {
            switch (filter.FieldType)
            {
                case FilterConstants.FilterFieldTypes.BooleanType:
                    {
                        return nameof(Boolean);
                    }
                case FilterConstants.FilterFieldTypes.DateType:
                    {
                        return nameof(DateTime);
                    }
                case FilterConstants.FilterFieldTypes.NumericalType:
                    {
                        return nameof(Double);
                    }
                case FilterConstants.FilterFieldTypes.StringType:
                    {
                        return nameof(String);
                    }
                default:
                    {
                        throw new NotImplementedException($"Filter for FieldName [{filter.FieldName}] has invalid FieldType [{filter.FieldType}]");
                    }
            }
        }

        public static bool IsInvalidFilterException(this Exception ex)
        {
            if(ex is ValidationException || ex is NotImplementedException)
            {
                return true;
            }

            return false;
        }
    }
}
