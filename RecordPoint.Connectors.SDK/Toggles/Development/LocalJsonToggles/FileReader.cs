using System.IO;

namespace RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles
{
    /// <summary>
    /// Default implementation of the IFileReader interface.
    /// </summary>
    public class FileReader : IFileReader
    {
        /// <summary>
        /// Gets all the text from a file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadAllText(string path) => File.ReadAllText(path);
    }
}
