using System.Net;

namespace RecordPoint.Connectors.SDK.WebHost.MiddleWare
{
    /// <summary>
    /// The remote requests http filter extension.
    /// </summary>
    public static class RemoteRequestsHttpFilterExtension
    {

        /// <summary>
        /// Did the webrequest originate locally
        /// </summary>
        /// <param name="request">Target web request</param>
        /// <returns>True if the request is local</returns>
        public static bool IsLocal(this HttpRequest request)
        {
            var connection = request.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                if (connection.LocalIpAddress != null)
                {
                    return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);
                }
                else
                {
                    return IPAddress.IsLoopback(connection.RemoteIpAddress);
                }
            }

            // for in memory TestServer or when dealing with default connection info
            if (connection.RemoteIpAddress == null && connection.LocalIpAddress == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Use the remote http filter web request filter
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <returns>Updated application builder</returns>
        public static IApplicationBuilder UseRemoteRequestFilter(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (!context.Request.IsLocal())
                {
                    // Forbidden http status code
                    context.Response.StatusCode = 403;
                    return;
                }

                await next.Invoke();
            });
            return app;
        }


    }
}
