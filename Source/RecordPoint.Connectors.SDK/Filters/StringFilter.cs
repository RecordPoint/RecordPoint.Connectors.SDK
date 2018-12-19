using RecordPoint.Connectors.SDK.Client.Models;
using System;

namespace RecordPoint.Connectors.SDK.Filters
{
    public static class StringFilter
    {
        public static bool MatchesFilter(SubmissionMetaDataModel model, SearchTermModel filter)
        {
            switch(filter.OperatorProperty)
            {
                case FilterConstants.CommonFieldOperators.Equal:
                    {
                        return IsEqual(model, filter.FieldValue);
                    }
                case FilterConstants.CommonFieldOperators.NotEqual:
                    {
                        return !IsEqual(model, filter.FieldValue);
                    }
                case FilterConstants.CommonFieldOperators.Empty:
                    {
                        throw new NotImplementedException();
                    }
                case FilterConstants.CommonFieldOperators.NotEmpty:
                    {
                        throw new NotImplementedException();
                    }
                case FilterConstants.StringFieldOperators.Contains:
                    {
                        throw new NotImplementedException();
                    }
                case FilterConstants.StringFieldOperators.NotContains:
                    {
                        throw new NotImplementedException();
                    }
                case FilterConstants.StringFieldOperators.EndsWith:
                    {
                        throw new NotImplementedException();
                    }
                case FilterConstants.StringFieldOperators.NotEndsWith:
                    {
                        throw new NotImplementedException();
                    }
                case FilterConstants.StringFieldOperators.StartsWith:
                    {
                        throw new NotImplementedException();
                    }
                case FilterConstants.StringFieldOperators.NotStartsWith:
                    {
                        throw new NotImplementedException();
                    }
                default:
                    {
                        throw new NotImplementedException($"Filter for FieldName [{filter.FieldName}] has invalid OperatorProperty [{filter.OperatorProperty}]");
                    }
            }
        }

        private static bool IsEqual(SubmissionMetaDataModel model, string expectedValue)
        {
            // Validation should prevent an equals operation on an empty string - Users can
            // use an Empty operator to perform that filtering if required. Return false if the field
            // is not present or is empty
            if(!string.IsNullOrEmpty(model?.Value))
            {
                return string.Equals(model.Value, expectedValue, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }
    }
}
