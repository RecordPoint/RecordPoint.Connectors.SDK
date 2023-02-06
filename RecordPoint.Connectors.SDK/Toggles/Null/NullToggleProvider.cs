namespace RecordPoint.Connectors.SDK.Toggles.Null
{
    /// <summary>
    /// Toggle provider that adds no funtionality and just passes through the default values
    /// </summary>
    public class NullToggleProvider : IToggleProvider
    {
        public bool GetToggleBool(string toggle, bool @default)
        {
            return @default;
        }
        public bool GetToggleBool(string toggle, string userKey, bool @default)
        {
            return @default;
        }

        public int GetToggleInt(string toggle, int @default)
        {
            return @default;
        }

        public int GetToggleInt(string toggle, string userKey, int @default)
        {
            return @default;
        }

    }
}
