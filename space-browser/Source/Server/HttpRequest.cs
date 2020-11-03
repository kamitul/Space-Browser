using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace space_browser
{
    public class HttpRequest
    {
        public HttpRequestMessage Message { get; private set; }
        public HttpClient HttpClient { get; private set; }
        public CancellationToken CancellationToken { get => cts.Token; }
        public HttpMethod HttpMethod { get; private set; }

        private readonly CancellationTokenSource cts;
        private readonly string url;

        public HttpRequest(HttpRequestMessage requestMessage, HttpClient httpClient)
        {
            Message = requestMessage;
            HttpClient = httpClient;
            cts = new CancellationTokenSource();
        }

        public async Task<HttpResponseMessage> Send()
        {
            var result = await HttpClient.SendAsync(Message, CancellationToken);
            return result;
        }
    }
}
