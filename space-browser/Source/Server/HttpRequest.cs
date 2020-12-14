using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace space_browser
{
    /// <summary>
    /// Http Request class handling
    /// </summary>
    public class HttpRequest
    {
        /// <summary>
        /// Request message
        /// </summary>
        public HttpRequestMessage Message { get; private set; }
        /// <summary>
        /// Request client
        /// </summary>
        public HttpClient HttpClient { get; private set; }
        /// <summary>
        /// Request cancelation token
        /// </summary>
        public CancellationToken CancellationToken { get => cts.Token; }
        /// <summary>
        /// Request http method
        /// </summary>
        public HttpMethod HttpMethod { get; private set; }

        private readonly CancellationTokenSource cts;
        private readonly string url;

        /// <summary>
        /// Constructs request
        /// </summary>
        /// <param name="requestMessage">Message of request</param>
        /// <param name="httpClient">HttpClient for each request</param>
        public HttpRequest(HttpRequestMessage requestMessage, HttpClient httpClient)
        {
            Message = requestMessage;
            HttpClient = httpClient;
            cts = new CancellationTokenSource();
        }

        /// <summary>
        /// Sends request
        /// </summary>
        /// <returns>Async task for request sent</returns>
        public async Task<HttpResponseMessage> Send()
        {
            var result = await HttpClient.SendAsync(Message, CancellationToken);
            return result;
        }
    }
}
