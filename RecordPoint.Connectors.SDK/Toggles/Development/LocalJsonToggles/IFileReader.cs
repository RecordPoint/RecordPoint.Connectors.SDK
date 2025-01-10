namespace RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles
{
    /// <summary>
    /// Interface for file reading, useful for dependency injection and testing.
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Reads all text from a file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string ReadAllText(string path);
    }
}
