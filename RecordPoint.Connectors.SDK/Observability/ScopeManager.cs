using Microsoft.Extensions.Logging;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace RecordPoint.Connectors.SDK.Observability
{

    /// <summary>
    /// Standard Observability Scope Manager
    /// </summary>
    public class ScopeManager : IScopeManager
    {

        private class Scope : IDisposable
        {

            private readonly ScopeManager _scopeManager;
            private readonly IDisposable _loggerScope;
            private readonly ImmutableDictionary<string, string> _originalDimensions;
            private bool disposedValue;

            public Scope(ScopeManager scopeManager, IDisposable loggerScope, ImmutableDictionary<string, string> originalDimensions)
            {
                _originalDimensions = originalDimensions;
                _loggerScope = loggerScope;
                _scopeManager = scopeManager;
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        _scopeManager._dimensions.Value = _originalDimensions;
                        _loggerScope.Dispose();
                    }

                    disposedValue = true;
                }
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }

        private readonly ILogger _logger;
        private readonly AsyncLocal<ImmutableDictionary<string, string>> _dimensions = new AsyncLocal<ImmutableDictionary<string, string>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ScopeManager(ILogger<object> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Scope dimensions
        /// </summary>
        public ImmutableDictionary<string, string> Dimensions
        {
            get { return _dimensions.Value ?? ImmutableDictionary<string, string>.Empty; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger => _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimensions"></param>
        /// <returns></returns>
        public IDisposable BeginScope(Dimensions dimensions)
        {
            var loggerScope = _logger.BeginScope(dimensions.ToLogState());
            var scope = new Scope(this, loggerScope, Dimensions);
            _dimensions.Value = Dimensions.AddRange(dimensions.Where(kp => !Dimensions.ContainsKey(kp.Key)));
            return scope;
        }


    }

}
