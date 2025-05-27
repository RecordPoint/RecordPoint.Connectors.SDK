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
        /// 
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
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
