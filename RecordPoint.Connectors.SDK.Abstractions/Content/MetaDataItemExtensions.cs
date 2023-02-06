namespace RecordPoint.Connectors.SDK.Content
{
    public static class MetaDataItemExtensions
    {
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
