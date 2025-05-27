namespace RecordPoint.Connectors.SDK.WebHost
{
    /// <summary>
    /// The web host options.
    /// </summary>
    public class WebHostOptions
    {
        /// <summary>
        /// The SECTION NAME.
        /// </summary>
        public const string SECTION_NAME = "WebHost";

        /// <summary>
        /// Gets or sets the swagger endpoint url.
        /// </summary>
        public string SwaggerEndpointUrl { get; set; } = "v1/swagger.json";
        /// <summary>
        /// Gets or sets the swagger endpoint name.
        /// </summary>
        public string SwaggerEndpointName { get; set; } = "RecordPoint Connectors SDK WebHost API";
    }
}
