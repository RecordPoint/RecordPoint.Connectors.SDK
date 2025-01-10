using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Filters
{
    static class StringFilter
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
                case FilterConstants.CommonFieldOperators.Empty:
                    {
                        return string.IsNullOrEmpty(model?.Value);
                    }
                case FilterConstants.CommonFieldOperators.NotEmpty:
                    {
                        return !string.IsNullOrEmpty(model?.Value);
                    }
                case FilterConstants.StringFieldOperators.Contains:
                    {
                        return IsContains(model, filter.FieldValue);
                    }
                case FilterConstants.StringFieldOperators.NotContains:
                    {
                        return !IsContains(model, filter.FieldValue);
                    }
                case FilterConstants.StringFieldOperators.EndsWith:
                    {
                        return IsEndsWith(model, filter.FieldValue);
                    }
                case FilterConstants.StringFieldOperators.NotEndsWith:
                    {
                        return !IsEndsWith(model, filter.FieldValue);
                    }
                case FilterConstants.StringFieldOperators.StartsWith:
                    {
                        return IsStartsWith(model, filter.FieldValue);
                    }
                case FilterConstants.StringFieldOperators.NotStartsWith:
                    {
                        return !IsStartsWith(model, filter.FieldValue);
                    }
                case FilterConstants.StringFieldOperators.EqualsToAnyOf:
                    {
                        return IsEqualsToAnyOf(model, filter.FieldValue);
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
            if (!string.IsNullOrEmpty(model?.Value))
            {
                return string.Equals(model.Value, expectedValue, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        private static bool IsContains(SubmissionMetaDataModel model, string expectedValue)
        {
            if (!string.IsNullOrEmpty(model?.Value))
            {
                //string.Contains doesn't have option to check with ignoreCase, that's why used IndexOf method
                //for reference https://stackoverflow.com/questions/444798/case-insensitive-containsstring/15464440#15464440
                return model.Value.IndexOf(expectedValue, StringComparison.OrdinalIgnoreCase) >= 0;
            }

            return false;
        }

        private static bool IsEndsWith(SubmissionMetaDataModel model, string expectedValue)
        {
            if (!string.IsNullOrEmpty(model?.Value))
            {
                return model.Value.EndsWith(expectedValue, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private static bool IsStartsWith(SubmissionMetaDataModel model, string expectedValue)
        {
            if (!string.IsNullOrEmpty(model?.Value))
            {
                return model.Value.StartsWith(expectedValue, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        private static bool IsEqualsToAnyOf(SubmissionMetaDataModel model, string expectedValue)
        {
            if (!string.IsNullOrEmpty(model?.Value))
            {
                return expectedValue.Split(',')
                    .Select(s => s.Trim().ToLower())
                    .Contains(model.Value.ToLower());
            }

            return false;
        }
    }
}
