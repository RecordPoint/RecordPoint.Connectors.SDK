using Xunit;

namespace RecordPoint.Connectors.SDK.Test
{
    /// <summary>
    /// xUnit collection definition used to isolate tests that redirect the process-global
    /// <see cref="System.Console.Out"/> writer to capture and assert on written output.
    ///
    /// <para>
    /// <see cref="System.Console.SetOut(System.IO.TextWriter)"/> mutates process-wide state, so
    /// <see cref="CollectionDefinitionAttribute.DisableParallelization"/> is enabled. This keeps
    /// these tests serial amongst themselves and prevents them running concurrently with the rest
    /// of the suite, so console writes from unrelated tests can never leak into a captured buffer
    /// (and vice versa), while the remainder of the suite continues to run in parallel.
    /// </para>
    /// </summary>
    [CollectionDefinition(Name, DisableParallelization = true)]
    public sealed class ConsoleOutputCollection
    {
        /// <summary>
        /// The collection name applied via <c>[Collection(ConsoleOutputCollection.Name)]</c>.
        /// </summary>
        public const string Name = "ConsoleOutput";
    }
}
