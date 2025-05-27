using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles
{
    /// <summary>
    /// This is a toggle provider that reads the value of toggles from a local json file. 
    /// </summary>
    public class LocalFileToggleProvider : IToggleProvider
    {
        private readonly Dictionary<string, FeatureToggleModel> _toggles;
        private readonly IOptions<LocalFeatureToggleOptions> _options;
        private readonly IFileReader _fileReader;
        private readonly object _lock = new();

        /// <summary>
        ///Initialises the LocalFileToggleProvider, reads the json file and stores the values in a dictionary.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="fileReader"></param>
        public LocalFileToggleProvider(IOptions<LocalFeatureToggleOptions> options, IFileReader fileReader)
        {
            _options = options;
            _fileReader = fileReader;
            _toggles = InnerGetTogglesDictionaryFromJson();
        }

        /// <summary>
        /// Gets the toggle value from the loaded dictionary, if it doesn't exist it returns the default value.
        /// </summary>
        /// <param name="toggle"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public bool GetToggleBool(string toggle, bool @default)
        {
            var toggles = GetTogglesDictionaryFromJson();
            return toggles.TryGetValue(toggle, out var featureToggle) ? featureToggle.Value : @default;
        }

        /// <summary>
        /// Gets the toggle value from the loaded dictionary, also checks for user overrides, if it doesn't exist it returns the default value.
        /// </summary>
        /// <param name="toggle"></param>
        /// <param name="userKey"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public bool GetToggleBool(string toggle, string userKey, bool @default)
        {
            var toggles = GetTogglesDictionaryFromJson();
            if (!toggles.TryGetValue(toggle, out var featureToggleModel))
            {
                return @default;
            }

            if (featureToggleModel.UserKeyOverrides.TryGetValue(userKey, out var toggleBool))
            {
                return toggleBool;
            }

            return featureToggleModel.Value;
        }

        /// <inheritdoc/>
        public int GetToggleNumber(string toggle, string userKey, int @default)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public int GetToggleNumber(string toggle, int @default)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetToggleString(string toggle, string userKey, string @default = null)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public string GetToggleString(string toggle, string @default = null)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Tries to load the json file if it fails it gets the loaded dictionary, this allows for the file to be updated without restarting the service.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, FeatureToggleModel> GetTogglesDictionaryFromJson()
        {
            Dictionary<string, FeatureToggleModel> toggleDictionary;
            lock (_lock)
            {
                try
                {
                    toggleDictionary = InnerGetTogglesDictionaryFromJson();
                }
                catch
                {
                    toggleDictionary = _toggles;
                }
            }

            return toggleDictionary;
        }
        private Dictionary<string, FeatureToggleModel> InnerGetTogglesDictionaryFromJson()
        {
            Dictionary<string, FeatureToggleModel> toggleDictionary;
            if (string.IsNullOrWhiteSpace(_options.Value.JsonFilePath))
            {
                toggleDictionary = new Dictionary<string, FeatureToggleModel>();
            }
            else
            {
                var json = _fileReader.ReadAllText(_options.Value.JsonFilePath);
                toggleDictionary = JsonConvert.DeserializeObject<Dictionary<string, FeatureToggleModel>>(json) ?? new Dictionary<string, FeatureToggleModel>();
            }

            return toggleDictionary;
        }
    }
}