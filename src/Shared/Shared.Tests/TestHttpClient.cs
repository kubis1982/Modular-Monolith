namespace ModularMonolith.Shared
{
    using Microsoft.AspNetCore.Mvc;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Modules.Endpoints.Responses;
    using System;
    using System.Net;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an HTTP test client for making HTTP requests.
    /// </summary>
    public class TestHttpClient : IDisposable
    {
        private readonly HttpClient client;
        private readonly string uriBase;

        /// <summary>
        /// Gets or sets the log action for logging request and response information.
        /// </summary>
        public Action<string>? Log { get; internal set; }

        /// <summary>
        /// Gets or sets the action to be executed when the client is disposed.
        /// </summary>
        public Action? Disposed { get; set; }

        internal TestHttpClient(string routePrefix, HttpClient client)
        {
            this.client = client;
            this.uriBase = $"https://localhost:8081/{routePrefix.ToLower()}";
        }

        #region Stream

        /// <summary>
        /// Sends a GET request to the specified URI and returns the response as a stream content.
        /// </summary>
        /// <param name="uri">The URI to send the request to.</param>
        /// <returns>The response as a stream content.</returns>
        public async Task<StreamContent> GetStreamAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Get, uri);
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage.Content as StreamContent ?? throw new ArgumentException("Nie można odczytać strumienia");
        }

        /// <summary>
        /// Sends a POST request to the specified URI and returns the response as a stream content.
        /// </summary>
        /// <param name="uri">The URI to send the request to.</param>
        /// <returns>The response as a stream content.</returns>
        public async Task<StreamContent> PostStreamAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri);
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage.Content as StreamContent ?? throw new ArgumentException("Nie można odczytać strumienia");
        }

        /// <summary>
        /// Sends a POST request to the specified URI with the specified request object and returns the response as a stream content.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="request">The request object.</param>
        /// <returns>The response as a stream content.</returns>
        public async Task<StreamContent> PostStreamAsync<TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri, JsonContent.Create(request));
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage.Content as StreamContent ?? throw new ArgumentException("Nie można odczytać strumienia");
        }

        #endregion

        #region Get

        /// <summary>
        /// Sends a GET request to the specified URI and returns the response deserialized as the specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize the response as.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <returns>The deserialized response.</returns>
        public async Task<TResponse> GetAsync<TResponse>(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Get, uri);
            responseMessage.EnsureSuccessStatusCode();
            return await ReadAsync<TResponse>(responseMessage);
        }

        /// <summary>
        /// Sends a GET request to the specified URI and returns the response deserialized as the specified type.
        /// </summary>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="type">The type to deserialize the response as.</param>
        /// <returns>The deserialized response.</returns>
        public async Task<object?> GetAsync(string uri, Type type)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Get, uri);
            responseMessage.EnsureSuccessStatusCode();
            return await ReadAsync(responseMessage, type);
        }

        /// <summary>
        /// Sends a GET request to the specified URI and ensures that the response has a status code of NotFound (404).
        /// </summary>
        /// <param name="uri">The URI to send the request to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task GetAndEnsureNotFoundAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Get, uri);
            await EnsureAsync(responseMessage, HttpStatusCode.NotFound);
        }

        #endregion

        #region Post

        /// <summary>
        /// Sends a POST request to the specified URI.
        /// </summary>
        /// <param name="uri">The URI to send the request to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PostAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri);
            await EnsureAsync(responseMessage);
        }

        /// <summary>
        /// Sends a POST request to the specified URI with the specified request object and returns the response deserialized as the specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize the response as.</typeparam>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="request">The request object.</param>
        /// <returns>The deserialized response.</returns>
        public async Task<TResponse> PostAsync<TResponse, TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri, JsonContent.Create(request));
            await EnsureAsync(responseMessage);
            return await ReadAsync<TResponse>(responseMessage);
        }

        /// <summary>
        /// Sends a POST request to the specified URI with the specified request object and returns the response deserialized as an IdentityResponse.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="request">The request object.</param>
        /// <returns>The deserialized response as an IdentityResponse.</returns>
        public async Task<IdentityResponse> PostAndReturnIdentityAsync<TRequest>(string uri, TRequest request)
        {
            return await PostAsync<IdentityResponse, TRequest>(uri, request);
        }

        /// <summary>
        /// Sends a POST request to the specified URI and returns the response deserialized as an IdentityResponse.
        /// </summary>
        /// <param name="uri">The URI to send the request to.</param>
        /// <returns>The deserialized response as an IdentityResponse.</returns>
        public async Task<IdentityResponse> PostAndReturnIdentityAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri);
            await EnsureAsync(responseMessage);
            return await ReadAsync<IdentityResponse>(responseMessage);
        }

        /// <summary>
        /// Sends a POST request to the specified URI with the specified request object.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="request">The request object.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PostAsync<TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri, JsonContent.Create(request));
            await EnsureAsync(responseMessage);
        }

        #endregion

        #region Put

        /// <summary>
        /// Sends a PUT request to the specified URI with the specified request object.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="request">The request object.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PutAsync<TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Put, uri, JsonContent.Create(request));
            await EnsureAsync(responseMessage);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Sends a DELETE request to the specified URI and ensures that the response has a status code of NoContent (204).
        /// </summary>
        /// <param name="uri">The URI to send the request to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAndEnsureNoContentAsync(string uri)
        {
            var responseMessage = await SendAsync(HttpMethod.Delete, uri);
            await EnsureAsync(responseMessage, HttpStatusCode.NoContent);
        }

        #endregion

        #region Patch

        /// <summary>
        /// Sends a PATCH request to the specified URI.
        /// </summary>
        /// <param name="uri">The URI to send the request to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PatchAsync(string uri)
        {
            var responseMessage = await SendAsync(HttpMethod.Patch, uri);
            responseMessage.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Sends a PATCH request to the specified URI with the specified request object.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="request">The request object.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task PatchAsync<TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Patch, uri, JsonContent.Create(request));
            responseMessage.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Sends a PATCH request to the specified URI and returns the response deserialized as the specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize the response as.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <returns>The deserialized response.</returns>
        public async Task<TResponse> PatchAndResultAsync<TResponse>(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Patch, uri);
            responseMessage.EnsureSuccessStatusCode();
            return await ReadAsync<TResponse>(responseMessage);
        }

        /// <summary>
        /// Sends a PATCH request to the specified URI with the specified request object and returns the response deserialized as the specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type to deserialize the response as.</typeparam>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <param name="uri">The URI to send the request to.</param>
        /// <param name="request">The request object.</param>
        /// <returns>The deserialized response.</returns>
        public async Task<TResponse> PatchAsync<TResponse, TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Patch, uri, JsonContent.Create(request));
            responseMessage.EnsureSuccessStatusCode();
            return await ReadAsync<TResponse>(responseMessage);
        }

        #endregion

        #region Logs

        private void WriteLog(string text)
        {
            Log?.Invoke(text);
        }

        private async Task WriteLogAsync(string title, HttpContent? httpContent)
        {
            if (httpContent != null)
            {
                string text = await httpContent.ReadAsStringAsync();
                WriteLog($"{title}: {text}");
            }
        }

        #endregion

        private async Task<T> ReadAsync<T>(HttpResponseMessage responseMessage)
        {
            await WriteLogAsync("Response", responseMessage.Content);
            return await responseMessage.Content.ReadFromJsonAsync<T>() ?? throw new ArgumentException($"Nie można deserializować {typeof(T).Name}");
        }

        private async Task<object?> ReadAsync(HttpResponseMessage responseMessage, Type type)
        {
            await WriteLogAsync("Response", responseMessage.Content);
            return await responseMessage.Content.ReadFromJsonAsync(type) ?? throw new ArgumentException($"Nie można deserializować {type.Name}");
        }

        private async Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string url, HttpContent? content = null)
        {
            url = $"{uriBase.WithLeadingSlash()}{url.WithLeadingSlash()}";
            WriteLog($"Requesting with {httpMethod.Method} {url}");
            var requestMessage = new HttpRequestMessage(httpMethod, url)
            {
                Content = content
            };
            await WriteLogAsync("Body", content);
            var responseMessage = await client.SendAsync(requestMessage);
            if (responseMessage.IsSuccessStatusCode == false)
            {
                await WriteLogAsync("Error", responseMessage.Content);
            }
            return responseMessage;
        }

        private static async Task EnsureAsync(HttpResponseMessage response, HttpStatusCode expected)
        {
            if (response.StatusCode != expected)
            {
                throw new HttpRequestException(await response.Content.ReadAsStringAsync());
            }
        }

        private static async Task EnsureAsync(HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                ProblemDetails? problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new HttpRequestException(problemDetails?.Title ?? await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Disposes the underlying HttpClient and invokes the Disposed action.
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
            Disposed?.Invoke();
        }

        internal static TestHttpClient CreateHttpApi<TEntryPoint>(string routePrefix, TestWebApplicationFactory<TEntryPoint> factory, Action<string>? log) where TEntryPoint : class
        {
            var client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
            return new TestHttpClient(routePrefix, client)
            {
                Log = log
            };
        }
    }
}
