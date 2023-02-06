namespace RecordPoint.Connectors.SDK.Client
{
    public static class Extensions
    {
        public static void TryAddQueryParameter(this List<string> queryParameters, string value, string format)
        {
            if (value != null)
            {
                queryParameters.Add(string.Format(format, Uri.EscapeDataString(value)));
            }
        }
    }
}
