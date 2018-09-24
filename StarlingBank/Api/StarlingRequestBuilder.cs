using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StarlingBank.Api
{
    public class StarlingRequestBuilder : IDisposable, IStarlingRequestBuilder, IStarlingRequestBuilderMethod, IStarlingRequestBuilderHeadersBodySend
    {
        public StarlingRequestBuilder()
        {
            this.Client = new HttpClient();
        }

        private StarlingRequestBuilder(HttpClient client, HttpRequestMessage request)
        {
            this.Client = client;
            this.Request = request;
        }

        protected HttpClient Client { get; }

        protected HttpRequestMessage Request { get; }

        private JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };


        public IStarlingRequestBuilderMethod WithUrl(string url)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url)
            };

            return new StarlingRequestBuilder(this.Client, request);
        }

        public IStarlingRequestBuilderHeadersBodySend WithMethod(HttpMethod method)
        {
            this.Request.Method = method;

            return this;
        }

        public IStarlingRequestBuilderHeadersBodySend WithHeader(string header, string value)
        {
            this.Request.Headers.Add(header, value);

            return this;
        }

        public IStarlingRequestBuilderHeadersBodySend WithHeaders(params KeyValuePair<string, string>[] headers)
        {
            foreach (var kvp in headers)
            {
                this.Request.Headers.Add(kvp.Key, kvp.Value);
            }

            return this;
        }

        public IStarlingRequestBuilderHeadersBodySend WithBody<T>(T body) where T : class
        {
            var json = JsonConvert.SerializeObject(body, this.SerializerSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            this.Request.Content = content;

            return this;
        }

        public async Task<(HttpStatusCode code, TResult result)> SendAsync<TResult>()
        {
            var response = await this.Client.SendAsync(this.Request);
            var result = await response.Content.ReadAsAsync<TResult>();

            return (response.StatusCode, result);
        }

        public Task<HttpResponseMessage> SendAsync()
        {
            return this.Client.SendAsync(this.Request);
        }

        public void Dispose()
        {
            this.Client?.Dispose();
        }
    }

    public interface IStarlingRequestBuilder
    {
        IStarlingRequestBuilderMethod WithUrl(string url);
    }

    public interface IStarlingRequestBuilderMethod
    {
        IStarlingRequestBuilderHeadersBodySend WithMethod(HttpMethod method);
    }

    public interface IStarlingRequestBuilderHeadersBodySend
    {
        IStarlingRequestBuilderHeadersBodySend WithHeader(string header, string value);

        IStarlingRequestBuilderHeadersBodySend WithHeaders(params KeyValuePair<string, string>[] headers);

        IStarlingRequestBuilderHeadersBodySend WithBody<T>(T body) where T : class;

        Task<(HttpStatusCode code, TResult result)> SendAsync<TResult>();

        Task<HttpResponseMessage> SendAsync();
    }
}