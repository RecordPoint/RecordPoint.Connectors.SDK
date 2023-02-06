﻿using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Client.Models;
using System.Net;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Why is this here?
    /// 
    /// The OpenAPI/Swagger 2.0 tooling for providing binary/file uploads to HTTP methods is not great -
    /// it only supports multipart/form-data which is not suitable for our needs. 
    /// 
    /// The autogenerated client in ApiClient therefore doesn't provide any way to provide a Stream
    /// to the binary submission endpoint. Hence, we provide a copy+paste of that method here,
    /// with an additional parameter for the Stream.
    /// 
    /// This should be revisited as/when we upgrade to OpenAPI/Swagger 3.0.
    /// </summary>
    public partial class ApiClient
    {
        /// <summary>
        /// Submits a binary to be archived and protected by Records365 vNext.
        /// </summary>
        /// <param name='connectorId'>
        /// The ID of the connector submitting the binary
        /// </param>
        /// <param name='itemExternalId'>
        /// The ExternalID of the item that the binary belongs to
        /// </param>
        /// <param name='binaryExternalId'>
        /// The ExternalID of the binary
        /// </param>
        /// <param name='fileName'>
        /// An optional file name to associate with the binary
        /// </param>
        /// <param name="correlationId">
        /// An optional ID used to track the binary as it moves through the pipeline
        /// </param>
        /// <param name='acceptLanguage'>
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='inputStream'>
        /// A stream for binary data to upload.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<HttpOperationResponse<ErrorResponseModel>> ApiBinariesPostWithHttpMessagesAndStreamAsync(
            string connectorId = default,
            string itemExternalId = default,
            string binaryExternalId = default,
            string fileName = default,
            string correlationId = default,
            string acceptLanguage = default,
            Dictionary<string, List<string>> customHeaders = null,
            Stream inputStream = null,
            CancellationToken cancellationToken = default)
        {

            //Setup Request
            var (httpRequest, invocationId) = await SetupRequestAsync(connectorId, itemExternalId, binaryExternalId, fileName, correlationId, acceptLanguage, customHeaders, inputStream, cancellationToken);

            // Send Request
            if (ServiceClientTracing.IsEnabled)
            {
                ServiceClientTracing.SendRequest(invocationId, httpRequest);
            }


            cancellationToken.ThrowIfCancellationRequested();
            var httpResponse = await HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);

            if (ServiceClientTracing.IsEnabled)
            {
                ServiceClientTracing.ReceiveResponse(invocationId, httpResponse);
            }

            cancellationToken.ThrowIfCancellationRequested();
            await HandleInitialResponseAsync(httpRequest, httpResponse, invocationId, cancellationToken).ConfigureAwait(false);

            // Create Result
            var result = new HttpOperationResponse<ErrorResponseModel>
            {
                Request = httpRequest,
                Response = httpResponse
            };

            await HandleBadRequestAsync(httpRequest, httpResponse, result, cancellationToken).ConfigureAwait(false);

            if (ServiceClientTracing.IsEnabled)
            {
                ServiceClientTracing.Exit(invocationId, result);
            }

            return result;
        }

        private async Task<(HttpRequestMessage HttpRequest, string InvocationId)> SetupRequestAsync(
            string connectorId,
            string itemExternalId,
            string binaryExternalId,
            string fileName,
            string correlationId,
            string acceptLanguage,
            Dictionary<string, List<string>> customHeaders,
            Stream inputStream,
            CancellationToken cancellationToken)
        {
            // Tracing
            string invocationId = null;
            if (ServiceClientTracing.IsEnabled)
            {
                invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new()
                {
                    { "connectorId", connectorId },
                    { "itemExternalId", itemExternalId },
                    { "binaryExternalId", binaryExternalId },
                    { "fileName", fileName },
                    { "acceptLanguage", acceptLanguage },
                    { "cancellationToken", cancellationToken }
                };
                ServiceClientTracing.Enter(invocationId, this, "ApiBinariesPost", tracingParameters);
            }

            // Construct URL
            var url = GetUrl(connectorId, itemExternalId, binaryExternalId, fileName, correlationId);

            // Create HTTP transport objects
            var httpRequest = new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(url)
            };

            // Set Headers
            AddRequestHeaders(httpRequest, acceptLanguage, customHeaders);

            // BEGIN MANUALLY MODIFIED CODE
            httpRequest.Content = new StreamContent(inputStream);
            // ensure that Expect Continue behaviour is always used for binary submissions
            httpRequest.Headers.ExpectContinue = true;
            // END MANUALLY MODIFIED CODE 

            // Set Credentials
            if (Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Credentials.ProcessHttpRequestAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }

            return (httpRequest, invocationId);
        }

        private static async Task HandleInitialResponseAsync(HttpRequestMessage httpRequest, HttpResponseMessage httpResponse, string invocationId, CancellationToken cancellationToken)
        {
            var requestContent = string.Empty;
            if (httpResponse.StatusCode != HttpStatusCode.OK && httpResponse.StatusCode != HttpStatusCode.BadRequest && httpResponse.StatusCode != HttpStatusCode.PreconditionFailed)
            {
                var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'", httpResponse.StatusCode));
                var responseContent = httpResponse.Content != null
                    ? await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)
                    : string.Empty;

                ex.Request = new HttpRequestMessageWrapper(httpRequest, requestContent);
                ex.Response = new HttpResponseMessageWrapper(httpResponse, responseContent);
                if (ServiceClientTracing.IsEnabled)
                {
                    ServiceClientTracing.Error(invocationId, ex);
                }
                httpRequest.Dispose();
                if (httpResponse != null)
                {
                    httpResponse.Dispose();
                }
                throw ex;
            }
        }

        private async Task HandleBadRequestAsync(HttpRequestMessage httpRequest, HttpResponseMessage httpResponse, HttpOperationResponse<ErrorResponseModel> result, CancellationToken cancellationToken)
        {
            // Deserialize Response
            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                try
                {
                    result.Body = SafeJsonConvert.DeserializeObject<ErrorResponseModel>(responseContent, DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    httpRequest?.Dispose();
                    httpResponse?.Dispose();
                    throw new SerializationException("Unable to deserialize the response.", responseContent, ex);
                }
            }
        }

        private string GetUrl(string connectorId, string itemExternalId, string binaryExternalId, string fileName, string correlationId)
        {
            var baseUrl = BaseUri.AbsoluteUri + (BaseUri.AbsoluteUri.EndsWith("/") ? "" : "/");
            var url = new Uri(new Uri(baseUrl), "api/Binaries").ToString();

            var queryParameters = new List<string>();
            queryParameters.TryAddQueryParameter(connectorId, "ConnectorId={0}");
            queryParameters.TryAddQueryParameter(itemExternalId, "ItemExternalId={0}");
            queryParameters.TryAddQueryParameter(binaryExternalId, "BinaryExternalId={0}");
            queryParameters.TryAddQueryParameter(fileName, "FileName={0}");
            queryParameters.TryAddQueryParameter(correlationId, "CorrelationId={0}");
            if (queryParameters.Count > 0)
            {
                url += "?" + string.Join("&", queryParameters);
            }

            return url;
        }

        private static void AddRequestHeaders(HttpRequestMessage httpRequest, string acceptLanguage, Dictionary<string, List<string>> customHeaders)
        {
            if (acceptLanguage != null)
            {
                if (httpRequest.Headers.Contains("Accept-Language"))
                {
                    httpRequest.Headers.Remove("Accept-Language");
                }
                httpRequest.Headers.TryAddWithoutValidation("Accept-Language", acceptLanguage);
            }

            if (customHeaders != null)
            {
                foreach (var header in customHeaders)
                {
                    if (httpRequest.Headers.Contains(header.Key))
                    {
                        httpRequest.Headers.Remove(header.Key);
                    }
                    httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

    }
}
