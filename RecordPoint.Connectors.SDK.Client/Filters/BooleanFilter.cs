using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Filters
{
    static class BooleanFilter
    {
        public static bool MatchesFilter(SubmissionMetaDataModel model, SearchTermModel filter)
        {
            switch (filter.OperatorProperty)
            {
                case FilterConstants.CommonFieldOperators.Equal:
                    {
                        return IsEqual(model, filter.FieldValue);
                    }
                case FilterConstants.CommonFieldOperators.NotEqual:
                    {
                        return !IsEqual(model, filter.FieldValue);
                    }
                default:
                    {
                        throw new NotImplementedException($"Filter for FieldName [{filter.FieldName}] has invalid OperatorProperty [{filter.OperatorProperty}]");
                    }
            }
        }

        private static bool IsEqual(SubmissionMetaDataModel model, string expectedValue)
        {
            if (!string.IsNullOrEmpty(model?.Value))
            {
                return string.Equals(model.Value, expectedValue, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }
    }
}
