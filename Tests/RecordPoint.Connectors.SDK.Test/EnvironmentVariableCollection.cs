using Xunit;

namespace RecordPoint.Connectors.SDK.Test
{
    /// <summary>
    /// xUnit collection definition used to isolate tests that either mutate
    /// process-global environment state (DOTNET_ENVIRONMENT / ASPNETCORE_ENVIRONMENT)
    /// or build a default generic <c>Host</c> whose configuration and DI validation
    /// depend on that environment state.
    ///
    /// <para>
    /// These tests share process-wide state, so <see cref="CollectionDefinitionAttribute.DisableParallelization"/>
    /// is enabled. This keeps them serial amongst themselves (and prevents them running
    /// concurrently with the rest of the suite) so a host build never observes an
    /// environment variable that another test is temporarily mutating, while the
    /// remainder of the test suite continues to run in parallel.
    /// </para>
    /// </summary>
    [CollectionDefinition(Name, DisableParallelization = true)]
    public sealed class EnvironmentVariableCollection
    {
        /// <summary>
        /// The collection name applied via <c>[Collection(EnvironmentVariableCollection.Name)]</c>.
        /// </summary>
        public const string Name = "EnvironmentVariable";
    }
}
