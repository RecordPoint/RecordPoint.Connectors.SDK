namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// The meta data item extensions.
    /// </summary>
    public static class MetaDataItemExtensions
    {
        /// <summary>
        /// Checks if is equal.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The Y.</param>
        /// <returns>A bool</returns>
        public static bool IsEqual(this List<MetaDataItem> x, List<MetaDataItem> y)
        {
            if (x == null && y == null) return true;
            if (x == null) return false;
            if (y == null) return false;
            if (x.Count != y.Count) return false;

            for (var i = 0; i < x.Count; i++)
            {
                if (!x[i].Equals(y[i])) return false;
            }

            return true;
        }
    }
}
