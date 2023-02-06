using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceConnector.Common
{
    public class SemaphoreLockScopedKeyAction : ISemaphoreLockScopedKeyAction
    {
        public Task<string> ExecuteAsync(ConnectorConfigModel connectorConfigModel, string workType, CancellationToken cancellationToken)
        {
            return Task.FromResult(workType);
        }
    }
}
