namespace RecordPoint.Connectors.SDK.WebHost
{
    /// <summary>
    /// The web host builder extensions.
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Default value for the API Urls
        /// </summary>
        private static readonly string[] DEFAULT_API_URLS = new[] { "https://localhost:44342" };

        /// <summary>
        /// Configures and enables the SDK web host with configured or default URLs.
        /// </summary>
        /// <param name="hostBuilder">The host builder to configure.</param>
        /// <param name="configuration">The application configuration root.</param>
        /// <returns>The configured host builder.</returns>
        public static IHostBuilder UseWebHost(this IHostBuilder hostBuilder, IConfigurationRoot configuration)
        {
            var configuredUrls = configuration
                .GetSection("WebHost:Urls")
                .Get<string[]>();

            var urls = configuredUrls ?? DEFAULT_API_URLS;

            return hostBuilder
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(urls);
                });
        }
    }
}
