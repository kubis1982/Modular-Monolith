namespace ModularMonolith.Shared
{
    using Microsoft.AspNetCore.Mvc;
    using ModularMonolith.Shared.Extensions;
    using ModularMonolith.Shared.Modules.Endpoints.Responses;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;

    public class TestHttpClient : IDisposable
    {
        private readonly HttpClient client;
        private readonly string uriBase;

        public Action<string>? Log { get; set; }

        internal TestHttpClient(string routePrefix, HttpClient client)
        {
            this.client = client;
            this.uriBase = $"https://localhost:7778/{routePrefix.ToLower()}";
        }

        #region Stream

        public async Task<StreamContent> GetStreamAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Get, uri);
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage.Content as StreamContent ?? throw new ArgumentException("Nie można odczytać strumienia");
        }

        public async Task<StreamContent> PostStreamAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri);
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage.Content as StreamContent ?? throw new ArgumentException("Nie można odczytać strumienia");
        }

        public async Task<StreamContent> PostStreamAsync<TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri, JsonContent.Create(request));
            responseMessage.EnsureSuccessStatusCode();
            return responseMessage.Content as StreamContent ?? throw new ArgumentException("Nie można odczytać strumienia");
        }

        #endregion

        #region Get

        public async Task<TResponse> GetAsync<TResponse>(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Get, uri);
            responseMessage.EnsureSuccessStatusCode();
            return await ReadAsync<TResponse>(responseMessage);
        }

        public async Task<object?> GetAsync(string uri, Type type)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Get, uri);
            responseMessage.EnsureSuccessStatusCode();
            return await ReadAsync(responseMessage, type);
        }

        public async Task GetAndEnsureNotFoundAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Get, uri);
            await EnsureAsync(responseMessage, HttpStatusCode.NotFound);
        }

        #endregion

        #region Post

        public async Task PostAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri);
            await EnsureAsync(responseMessage);
        }

        public async Task<TResponse> PostAsync<TResponse, TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri, JsonContent.Create(request));
            await EnsureAsync(responseMessage);
            return await ReadAsync<TResponse>(responseMessage);
        }

        public async Task<IdentityResponse> PostAndReturnIdentityAsync<TRequest>(string uri, TRequest request)
        {
            return await PostAsync<IdentityResponse, TRequest>(uri, request);
        }

        /// <summary>
        /// Without body
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<IdentityResponse> PostAndReturnIdentityAsync(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri);
            await EnsureAsync(responseMessage);
            return await ReadAsync<IdentityResponse>(responseMessage);
        }

        public async Task PostAsync<TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Post, uri, JsonContent.Create(request));
            await EnsureAsync(responseMessage);
        }

        #endregion

        #region Put

        public async Task PutAsync<TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Put, uri, JsonContent.Create(request));
            await EnsureAsync(responseMessage);
        }

        #endregion

        #region Delete

        public async Task DeleteAndEnsureNoContentAsync(string uri)
        {
            var responseMessage = await SendAsync(HttpMethod.Delete, uri);
            await EnsureAsync(responseMessage, HttpStatusCode.NoContent);
        }

        public async Task DeleteAndEnsureNoContentAsync<TRequest>(string uri, TRequest request)
        {
            var responseMessage = await SendAsync(HttpMethod.Delete, uri, JsonContent.Create(request));
            await EnsureAsync(responseMessage, HttpStatusCode.NoContent);
        }

        #endregion

        #region Patch

        public async Task PatchAsync(string uri)
        {
            var responseMessage = await SendAsync(HttpMethod.Patch, uri);
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task PatchAsync<TRequest>(string uri, TRequest request)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Patch, uri, JsonContent.Create(request));
            responseMessage.EnsureSuccessStatusCode();
        }

        public async Task<TResponse> PatchAndResultAsync<TResponse>(string uri)
        {
            HttpResponseMessage responseMessage = await SendAsync(HttpMethod.Patch, uri);
            responseMessage.EnsureSuccessStatusCode();
            return await ReadAsync<TResponse>(responseMessage);
        }

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

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
