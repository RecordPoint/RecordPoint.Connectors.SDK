using System;
using System.Collections.Generic;

namespace RecordPoint.Connectors.SDK.Toggles.Development
{

    /// <summary>
    /// Toggle provider that returns values set in the Toggles dictionary, otherwise just returns the default value.
    /// </summary>
    /// <remarks>
    /// Intended for use in development
    /// </remarks>
    public class DictionaryToggleProvider : IToggleProvider
    {

        public Dictionary<string, object> Toggles { get; }

        public DictionaryToggleProvider()
        {
            Toggles = new Dictionary<string, object>();
        }

        public bool GetToggleBool(string toggle, bool @default)
        {
            return (bool)Toggles.GetValueOrDefault(toggle, @default);
        }

        public bool GetToggleBool(string toggle, string userKey, bool @default)
        {
            return (bool)Toggles.GetValueOrDefault(toggle, @default);
        }

        public int GetToggleInt(string toggle, int @default)
        {
            return (int)Toggles.GetValueOrDefault(toggle, @default);
        }

        public int GetToggleInt(string toggle, string userKey, int @default)
        {
            return (int)Toggles.GetValueOrDefault(toggle, @default);
        }

        public void GetToggleBool(string v)
        {
            throw new NotImplementedException();
        }
    }
}
