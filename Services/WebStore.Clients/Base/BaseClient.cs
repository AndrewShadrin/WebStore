using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly string serviceAddress;
        protected HttpClient client;

        protected BaseClient(IConfiguration configuration, string serviceAddress)
        {
            this.serviceAddress = serviceAddress;
            client = new HttpClient
            {
                BaseAddress = new Uri(configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }

        protected T Get<T>(string url) => GetAsync<T>(url).Result;

        protected async Task<T> GetAsync<T>(string url, CancellationToken cancell = default)
        {
            var response = await client.GetAsync(url, cancell);
            return await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsAsync<T>(cancell);
        }

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;
        
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken cancell = default)
        {
            var response = await client.PostAsJsonAsync(url, item, cancell);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken cancell = default)
        {
            var response = await client.PutAsJsonAsync(url, item, cancell);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancell = default)
        {
            return await client.DeleteAsync(url, cancell);
        }

        public void Dispose() => Dispose(true);
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            ((IDisposable)client).Dispose();
        }
    }
}
