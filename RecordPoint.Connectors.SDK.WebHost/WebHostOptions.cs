namespace RecordPoint.Connectors.SDK.WebHost
{
    public class WebHostOptions
    {
        public const string SECTION_NAME = "WebHost";

        public string SwaggerEndpointUrl { get; set; } = "v1/swagger.json";
        public string SwaggerEndpointName { get; set; } = "RecordPoint Connectors SDK WebHost API";
    }
}
