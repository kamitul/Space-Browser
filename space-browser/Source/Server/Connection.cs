using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace space_browser
{
    class Connection
    {
        private HttpClient httpClient;

        public Connection(long timeout)
        {
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
        }

        public HttpRequest CreateGet(string url)
        {
            return new HttpRequest(new HttpRequestMessage(HttpMethod.Get, url), httpClient);
        }
    }
}
