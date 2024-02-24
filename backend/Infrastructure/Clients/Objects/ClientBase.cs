using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Objects
{
    public abstract class ClientBase
    {
        protected HttpClient HttpClient { get; }

        protected string BaseUri { get; }

        protected ClientBase(string baseUri, HttpClient httpClient)
        {
            HttpClient = httpClient;
            BaseUri = baseUri;
        }

        protected HttpRequestMessage CreateRequestMessage(HttpMethod method, string path, HttpContent? content = default)
        {
            return new HttpRequestMessage(method, $"{BaseUri}/{path}") { Content = content };
        }
    }
}
