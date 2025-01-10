using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Client.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Test.Common.Mock.Caching.Semaphore
{
    public class MockSemaphoreLockScopedKeyAction : ISemaphoreLockScopedKeyAction
    {
        private const string DefaultKey = "KEY_123";

        public string Key { get; set; } = string.Empty;
        public int ExecutionCount { get; set; } = 0;

        public Task<string> ExecuteAsync(ConnectorConfigModel connectorConfigModel, string workType, object? context, CancellationToken cancellationToken)
        {
            ExecutionCount++;

            var key = string.IsNullOrEmpty(Key)
                ? $"{workType.Replace(" ", "")}_{DefaultKey}"
                : Key;

            return Task.FromResult(key);
        }
    }
}
