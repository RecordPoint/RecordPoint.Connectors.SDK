﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace RecordPoint.Connectors.SDK.Test
{
    public class TestLogger<T> : ILogger<T>, IDisposable
    {

        public List<string> Output = [];
        private bool disposedValue;

        public TestLogger()
        {
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var item = state?.ToString();
            if (!string.IsNullOrEmpty(item))
            {
                Output.Add(item);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
