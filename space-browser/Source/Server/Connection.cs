using System;
using System.Net.Http;

namespace space_browser
{
    /// <summary>
    /// Connection class
    /// </summary>
    class Connection
    {
        private HttpClient httpClient;

        /// <summary>
        /// Construct connection class
        /// </summary>
        /// <param name="timeout">Timeout time</param>
        public Connection(long timeout)
        {
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
        }

        /// <summary>
        /// Creates GET Request
        /// </summary>
        /// <param name="url">URL request</param>
        /// <returns>GET Request</returns>
        public HttpRequest CreateGet(string url)
        {
            return new HttpRequest(new HttpRequestMessage(HttpMethod.Get, url), httpClient);
        }
    }
}
