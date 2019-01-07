namespace RecordPoint.Connectors.SDK.Filters
{
    internal static class FilterConstants
    {
        public static class FilterBooleanOperators
        {
            public const string Or = "OR";
            public const string And = "And";
        }

        public static class FilterFieldTypes
        {
            public const string BooleanType = nameof(BooleanType);
            public const string DateType = nameof(DateType);
            public const string NumericalType = nameof(NumericalType);
            public const string StringType = nameof(StringType);
        }

        public static class CommonFieldOperators
        {
            public const string Equal = nameof(Equal);
            public const string NotEqual = nameof(NotEqual);
            public const string Empty = nameof(Empty);
            public const string NotEmpty = nameof(NotEmpty);
        }

        public static class StringFieldOperators
        {
            public const string Contains = nameof(Contains);
            public const string NotContains = nameof(NotContains);
            public const string EndsWith = nameof(EndsWith);
            public const string NotEndsWith = nameof(NotEndsWith);
            public const string StartsWith = nameof(StartsWith);
            public const string NotStartsWith = nameof(NotStartsWith);
        }

        public static class DateFieldOperators
        {
            public const string After = nameof(After);
            public const string Before = nameof(Before);
        }
    }
}
