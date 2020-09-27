using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
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
    }
}
