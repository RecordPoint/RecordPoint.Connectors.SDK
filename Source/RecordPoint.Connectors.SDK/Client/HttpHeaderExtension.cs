using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordPoint.Connectors.SDK.Client
{
    public static class HttpHeaderExtension
    {
        public const string AuthorizationHeaderName = "Authorization";

        public static void AddAuthorizationHeader(this Dictionary<string, List<string>> headers, string tokenType, string token)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            headers[AuthorizationHeaderName] = new List<string>() { $"{tokenType} {token}" };
        }
    }
}
