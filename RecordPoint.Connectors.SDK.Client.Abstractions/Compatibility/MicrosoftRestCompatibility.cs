using Azure;
using Azure.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http.Headers;

namespace Microsoft.Rest
{
    /// <summary>
    /// Represents a marker interface for generated service operation groups.
    /// </summary>
    /// <typeparam name="T">The service client type.</typeparam>
#pragma warning disable S2326
    public interface IServiceOperations<out T>
#pragma warning restore S2326
    {
    }

    /// <summary>
    /// Provides credentials behavior for initializing and authorizing service clients.
    /// </summary>
    public abstract class ServiceClientCredentials
    {
        /// <summary>
        /// Initializes a service client instance with credential-specific behavior.
        /// </summary>
        /// <typeparam name="T">The service client type.</typeparam>
        /// <param name="client">The service client to initialize.</param>
        public virtual void InitializeServiceClient<T>(ServiceClient<T> client) where T : ServiceClient<T>
        {
        }

        /// <summary>
        /// Applies authentication information to an outgoing HTTP request.
        /// </summary>
        /// <param name="request">The outgoing HTTP request.</param>
        /// <param name="cancellationToken">A token used to cancel the operation.</param>
        /// <returns>A task that completes when the request has been processed.</returns>
        public virtual Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Represents a base HTTP service client implementation.
    /// </summary>
    /// <typeparam name="T">The concrete service client type.</typeparam>
    public abstract class ServiceClient<T> : IDisposable where T : ServiceClient<T>
    {
        private readonly bool _disposeHttpClient;
        private bool disposedValue;

        /// <summary>
        /// Initializes a new service client using an existing <see cref="HttpClient"/> instance.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use for requests.</param>
        /// <param name="disposeHttpClient"><c>true</c> to dispose the HTTP client when this service client is disposed; otherwise <c>false</c>.</param>
        protected ServiceClient(HttpClient httpClient, bool disposeHttpClient)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _disposeHttpClient = disposeHttpClient;
        }

        /// <summary>
        /// Initializes a new service client with a default root handler and optional delegating handlers.
        /// </summary>
        /// <param name="handlers">The delegating handlers to include in the HTTP pipeline.</param>
        protected ServiceClient(params DelegatingHandler[] handlers)
        {
            var pipeline = CreateHandlerPipeline(new HttpClientHandler(), handlers);
            HttpClient = new HttpClient(pipeline, true);
            _disposeHttpClient = true;
        }

        /// <summary>
        /// Initializes a new service client with the specified root handler and optional delegating handlers.
        /// </summary>
        /// <param name="rootHandler">The root HTTP handler for the pipeline.</param>
        /// <param name="handlers">The delegating handlers to include in the HTTP pipeline.</param>
        protected ServiceClient(HttpClientHandler rootHandler, params DelegatingHandler[] handlers)
        {
            var pipeline = CreateHandlerPipeline(rootHandler ?? new HttpClientHandler(), handlers);
            HttpClient = new HttpClient(pipeline, true);
            _disposeHttpClient = true;
        }

        /// <summary>
        /// Gets the HTTP client used to send requests.
        /// </summary>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// Releases resources used by the current operation response.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (_disposeHttpClient && disposing)
                {
                    HttpClient.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Releases resources used by the current operation response.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private static HttpMessageHandler CreateHandlerPipeline(HttpMessageHandler rootHandler, DelegatingHandler[] handlers)
        {
            if (handlers == null || handlers.Length == 0)
            {
                return rootHandler;
            }

            HttpMessageHandler current = rootHandler;

            foreach (var handler in handlers.Reverse())
            {
                if (handler == null)
                {
                    continue;
                }

                handler.InnerHandler = current;
                current = handler;
            }

            return current;
        }
    }

    /// <summary>
    /// Represents the HTTP request and response for a service operation.
    /// </summary>
    public class HttpOperationResponse : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Gets or sets the HTTP request associated with the operation.
        /// </summary>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        /// Gets or sets the HTTP response associated with the operation.
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// Releases resources used by the current operation response.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Response?.Dispose();
                    Request?.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Releases resources used by the current operation response.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Represents the HTTP request and response for a service operation, including a typed body.
    /// </summary>
    /// <typeparam name="T">The type of response body.</typeparam>
    public class HttpOperationResponse<T> : HttpOperationResponse
    {
        /// <summary>
        /// Gets or sets the typed response body.
        /// </summary>
        public T Body { get; set; }
    }

    /// <summary>
    /// Wraps an HTTP request and its serialized content.
    /// </summary>
    public class HttpRequestMessageWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessageWrapper"/> class.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="content">The serialized request content.</param>
        public HttpRequestMessageWrapper(HttpRequestMessage request, string content)
        {
            Request = request;
            Content = content;
        }

        /// <summary>
        /// Gets the wrapped HTTP request message.
        /// </summary>
        public HttpRequestMessage Request { get; }

        /// <summary>
        /// Gets the serialized request content.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the HTTP method of the wrapped request.
        /// </summary>
        public HttpMethod Method => Request?.Method;

        /// <summary>
        /// Gets the request URI of the wrapped request.
        /// </summary>
        public Uri RequestUri => Request?.RequestUri;
    }

    /// <summary>
    /// Wraps an HTTP response and its serialized content.
    /// </summary>
    public class HttpResponseMessageWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessageWrapper"/> class.
        /// </summary>
        /// <param name="response">The HTTP response message.</param>
        /// <param name="content">The serialized response content.</param>
        public HttpResponseMessageWrapper(HttpResponseMessage response, string content)
        {
            Response = response;
            Content = content;
        }

        /// <summary>
        /// Gets the wrapped HTTP response message.
        /// </summary>
        public HttpResponseMessage Response { get; }

        /// <summary>
        /// Gets the serialized response content.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the HTTP status code of the wrapped response.
        /// </summary>
        public HttpStatusCode StatusCode => Response?.StatusCode ?? 0;

        /// <summary>
        /// Gets the wrapped response headers.
        /// </summary>
        public HttpResponseHeadersWrapper Headers => new HttpResponseHeadersWrapper(Response?.Headers);
    }

    /// <summary>
    /// Wraps HTTP response headers and provides convenience helpers.
    /// </summary>
    public class HttpResponseHeadersWrapper
    {
        private readonly HttpResponseHeaders _headers;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseHeadersWrapper"/> class.
        /// </summary>
        /// <param name="headers">The response headers to wrap.</param>
        public HttpResponseHeadersWrapper(System.Net.Http.Headers.HttpResponseHeaders headers)
        {
            _headers = headers;
        }

        /// <summary>
        /// Attempts to retrieve the values for a specified header name.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="values">The header values if found; otherwise an empty sequence.</param>
        /// <returns><c>true</c> if the header exists; otherwise <c>false</c>.</returns>
        public bool TryGetValues(string name, out IEnumerable<string> values)
        {
            if (_headers == null)
            {
                values = Array.Empty<string>();
                return false;
            }

            return _headers.TryGetValues(name, out values);
        }
    }

    /// <summary>
    /// Represents failures that occur during HTTP service operations.
    /// </summary>
    public class HttpOperationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpOperationException"/> class.
        /// </summary>
        public HttpOperationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpOperationException"/> class with an error message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public HttpOperationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpOperationException"/> class with an error message and inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The exception that caused the current exception.</param>
        public HttpOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Gets or sets the wrapped request details related to the failure.
        /// </summary>
        public HttpRequestMessageWrapper Request { get; set; }

        /// <summary>
        /// Gets or sets the wrapped response details related to the failure.
        /// </summary>
        public HttpResponseMessageWrapper Response { get; set; }
    }

    /// <summary>
    /// Defines validation rules used by service client validation exceptions.
    /// </summary>
    public enum ValidationRules
    {
        /// <summary>
        /// Indicates that a value must not be null.
        /// </summary>
        CannotBeNull,

        /// <summary>
        /// Indicates that a value exceeds a maximum length constraint.
        /// </summary>
        MaxLength,

        /// <summary>
        /// Indicates that a value is shorter than a minimum length constraint.
        /// </summary>
        MinLength
    }

    /// <summary>
    /// Represents validation failures encountered while preparing service requests.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with an error message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with an error message and target.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="target">The name of the invalid target.</param>
        public ValidationException(string message, string target)
            : base(message)
        {
            Target = target;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class for a specific validation rule.
        /// </summary>
        /// <param name="rule">The validation rule that failed.</param>
        /// <param name="target">The name of the invalid target.</param>
        /// <param name="details">Additional details about the validation failure.</param>
        public ValidationException(ValidationRules rule, string target, params object[] details)
            : base(BuildMessage(rule, target, details))
        {
            Rule = rule;
            Target = target;
            Details = details ?? Array.Empty<object>();
        }

        /// <summary>
        /// Gets the validation rule that failed.
        /// </summary>
        public ValidationRules Rule { get; }

        /// <summary>
        /// Gets the target associated with the validation failure.
        /// </summary>
        public string Target { get; }

        /// <summary>
        /// Gets additional details about the validation failure.
        /// </summary>
        public IReadOnlyList<object> Details { get; }

        private static string BuildMessage(ValidationRules rule, string target, object[] details)
        {
            return details == null || details.Length == 0
                ? $"Validation failed ({rule}) for '{target}'."
                : $"Validation failed ({rule}) for '{target}' with values: {string.Join(", ", details.Select(d => d?.ToString() ?? "<null>"))}.";
        }
    }

    /// <summary>
    /// Represents serialization and deserialization failures for service messages.
    /// </summary>
    public class SerializationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerializationException"/> class with an error message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public SerializationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializationException"/> class with message content and an inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="content">The payload content that failed serialization or deserialization.</param>
        /// <param name="innerException">The exception that caused the current exception.</param>
        public SerializationException(string message, string content, Exception innerException)
            : base(message, innerException)
        {
            Content = content;
        }

        /// <summary>
        /// Gets the payload content that failed serialization or deserialization.
        /// </summary>
        public string Content { get; }
    }

    /// <summary>
    /// Provides extension points for service client tracing hooks.
    /// </summary>
    public static class ServiceClientTracing
    {
        private static long _nextInvocationId;

        /// <summary>
        /// Gets or sets a value indicating whether service client tracing is enabled.
        /// </summary>
        public static bool IsEnabled { get; set; }

        /// <summary>
        /// Gets the next invocation identifier used for tracing.
        /// </summary>
        public static long NextInvocationId => Interlocked.Increment(ref _nextInvocationId);

        /// <summary>
        /// Called when execution enters a traced service operation.
        /// </summary>
        /// <param name="invocationId">The invocation identifier.</param>
        /// <param name="instance">The target instance.</param>
        /// <param name="method">The method name.</param>
        /// <param name="parameters">The method parameters.</param>
        public static void Enter(string invocationId, object instance, string method, IDictionary<string, object> parameters)
        {
            //We aren't doing anything with this yet, but we can add logging or telemetry here in the future if needed.
        }

        /// <summary>
        /// Called when a traced operation sends an HTTP request.
        /// </summary>
        /// <param name="invocationId">The invocation identifier.</param>
        /// <param name="request">The outgoing HTTP request.</param>
        public static void SendRequest(string invocationId, HttpRequestMessage request)
        {
            //We aren't doing anything with this yet, but we can add logging or telemetry here in the future if needed.
        }

        /// <summary>
        /// Called when a traced operation receives an HTTP response.
        /// </summary>
        /// <param name="invocationId">The invocation identifier.</param>
        /// <param name="response">The incoming HTTP response.</param>
        public static void ReceiveResponse(string invocationId, HttpResponseMessage response)
        {
            //We aren't doing anything with this yet, but we can add logging or telemetry here in the future if needed.
        }

        /// <summary>
        /// Called when a traced operation encounters an error.
        /// </summary>
        /// <param name="invocationId">The invocation identifier.</param>
        /// <param name="exception">The exception that was thrown.</param>
        public static void Error(string invocationId, Exception exception)
        {
            //We aren't doing anything with this yet, but we can add logging or telemetry here in the future if needed.
        }

        /// <summary>
        /// Called when execution exits a traced service operation.
        /// </summary>
        /// <param name="invocationId">The invocation identifier.</param>
        /// <param name="result">The operation result.</param>
        public static void Exit(string invocationId, object result)
        {
            //We aren't doing anything with this yet, but we can add logging or telemetry here in the future if needed.
        }
    }
}

namespace Microsoft.Rest.Serialization
{
    /// <summary>
    /// Provides JSON serialization helpers for compatibility with Microsoft.Rest generated clients.
    /// </summary>
    public static class SafeJsonConvert
    {
        /// <summary>
        /// Serializes an object to JSON using the specified serializer settings.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="settings">The serializer settings.</param>
        /// <returns>The serialized JSON string.</returns>
        public static string SerializeObject(object value, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(value, settings);
        }

        /// <summary>
        /// Deserializes a JSON string into a typed object using the specified serializer settings.
        /// </summary>
        /// <typeparam name="T">The target type to deserialize to.</typeparam>
        /// <param name="value">The JSON payload.</param>
        /// <param name="settings">The serializer settings.</param>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeObject<T>(string value, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
    }

    /// <summary>
    /// Allows setting values on read-only properties during JSON deserialization.
    /// </summary>
    public sealed class ReadOnlyJsonContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Creates a JSON property definition and marks it writable for deserialization.
        /// </summary>
        /// <param name="member">The reflected member.</param>
        /// <param name="memberSerialization">The member serialization mode.</param>
        /// <returns>A JSON property configured as writable.</returns>
        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.Writable = true;
            return property;
        }
    }

    /// <summary>
    /// Serializes and deserializes <see cref="TimeSpan"/> values using ISO 8601-compatible formatting.
    /// </summary>
    public sealed class Iso8601TimeSpanConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether the converter can convert the specified type.
        /// </summary>
        /// <param name="objectType">The type to check.</param>
        /// <returns><c>true</c> if the type is <see cref="TimeSpan"/> or nullable <see cref="TimeSpan"/>; otherwise <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
        }

        /// <summary>
        /// Writes a <see cref="TimeSpan"/> value to JSON using constant format.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="serializer">The active serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(((TimeSpan)value).ToString("c"));
        }

        /// <summary>
        /// Reads a <see cref="TimeSpan"/> value from JSON.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="objectType">The destination type.</param>
        /// <param name="existingValue">The existing value of the destination object.</param>
        /// <param name="serializer">The active serializer.</param>
        /// <returns>The parsed <see cref="TimeSpan"/> value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return objectType == typeof(TimeSpan?) ? null : TimeSpan.Zero;
            }

#pragma warning disable S6580
            if (reader.Value is string raw && TimeSpan.TryParse(raw, out var parsed))
            {
                return parsed;
            }
#pragma warning restore S6580

            throw new JsonSerializationException($"Invalid TimeSpan value '{reader.Value}'.");
        }
    }
}
