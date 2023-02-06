namespace ReferenceConnector.Common
{
    public static class Extensions
    {
        public static string Random(this List<string> values, IRandomHelper randomHelper)
        {
            var index = randomHelper.GenerateRandomInteger(0, values.Count - 1);
            return values[index];
        }
    }
}
