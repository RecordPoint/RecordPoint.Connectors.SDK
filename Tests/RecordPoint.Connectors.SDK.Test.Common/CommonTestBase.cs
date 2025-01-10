using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Test
{
    /// <summary>
    /// Common Test Base Class
    /// </summary>
    public abstract class CommonTestBase<TSut> : IDisposable
        where TSut : CommonSutBase, new()
    {
        private bool disposedValue;

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="sut">SUT to use for this test</param>
        protected CommonTestBase()
        {
            SUT = new TSut();
        }

        /// <summary>
        /// System under test
        /// </summary>
        protected TSut? SUT { get; private set; }

        /// <summary>
        /// SUT Services available to use in tests
        /// </summary>
        protected IServiceProvider? Services => SUT?.Services;

        /// <summary>
        /// Start the SUT for this test
        /// </summary>
        /// <returns>Task</returns>
        protected async Task StartSutAsync(IConfiguration? configuration = null)
        {
            if (SUT == null)
            {
                throw new InvalidOperationException("Cannot start a null SUT");
            }
            await SUT.StartSUTAsync(configuration);
        }

        /// <summary>
        /// Stop the SUT running for this test
        /// </summary>
        /// <returns>Task</returns>
        protected async Task StopSUTAsync()
        {
            if (SUT == null)
                return;
            await SUT.StopSUTAsync();
            SUT = null;
        }

        /// <summary>
        /// Creates a clone of the source object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        protected static T Clone<T>(T source)
            => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));

        #region Embedded Resources
        protected static string GetEmbeddedResourceText(string resourceName, Type assemblyType)
        {
            var assembly = assemblyType.Assembly;
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new InvalidOperationException($"Could not find resource {resourceName}");
            }
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        #endregion

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    StopSUTAsync().Wait();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
