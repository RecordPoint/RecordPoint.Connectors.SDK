namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// The extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Try add query parameter.
        /// </summary>
        /// <param name="queryParameters">The query parameters.</param>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        public static void TryAddQueryParameter(this List<string> queryParameters, string value, string format)
        {
            if (value != null)
            {
                queryParameters.Add(string.Format(format, Uri.EscapeDataString(value)));
            }
        }
    }
}
