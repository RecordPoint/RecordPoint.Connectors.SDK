using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server;
using Microsoft.Extensions.Options;
using Optional;
using Optional.Unsafe;
using System;

namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly
{
    public sealed class LaunchDarklyToggleProvider : IToggleProvider, IDisposable
    {
        private readonly Option<LdClient> _client;
        private readonly IOptions<LaunchDarklyOptions> _options;

        private User DefaultUser { get; set; }

        public LaunchDarklyToggleProvider(IOptions<LaunchDarklyOptions> options)
        {
            _options = options;
            _client = CreateClient();
            DefaultUser = User.WithKey(options.Value.DefaultUserKey);
        }

        private Option<LdClient> CreateClient()
        {
            try
            {
                var client = new LdClient(_options.Value.SdkKey);
                return Option.Some(client);
            }
            catch
            {
                // Return null. We rely on the health check to
                // report on the failure
                return Option.None<LdClient>();
            }
        }

        public bool GetToggleBool(string toggle, bool @default)
        {
            if (!_client.HasValue)
                return @default;

            var client = _client.ValueOrFailure();
            return client.BoolVariation(toggle, DefaultUser);
        }

        public bool GetToggleBool(string toggle, string userKey, bool @default)
        {
            if (!_client.HasValue)
                return @default;

            var client = _client.ValueOrFailure();
            var user = User.WithKey(userKey);
            return client.BoolVariation(toggle, user, @default);
        }

        public int GetToggleInt(string toggle, int @default)
        {
            if (!_client.HasValue)
                return @default;

            var client = _client.ValueOrFailure();
            return client.IntVariation(toggle, DefaultUser, @default);
        }

        public int GetToggleInt(string toggle, string userKey, int @default)
        {
            var user = User.WithKey(userKey);

            if (!_client.HasValue)
                return @default;

            var client = _client.ValueOrFailure();
            return client.IntVariation(toggle, user, @default);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            _client.MatchSome(client => client.Dispose());
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}