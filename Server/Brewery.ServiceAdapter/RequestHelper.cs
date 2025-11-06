using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Brewery.ServiceAdapter
{
    public class RequestHelper
    {
        private class EmptyBody { }

        public async Task SendRequest(string method, MethodTypes methodType, string body = null)
        {
            await SendRequest<EmptyBody>(method, methodType, body);
        }

        public async Task<T> SendRequest<T>(string method, MethodTypes methodType, string body = null)
        {
            var requestUri = $"http://minwinpc:8800/api/{method}";

            var webRequest = WebRequest.CreateHttp(requestUri);
            webRequest.Accept = "application/json";
            webRequest.Method = methodType.ToString();

            if (webRequest.Method != MethodTypes.GET.ToString() && !string.IsNullOrWhiteSpace(body))
            {
                webRequest.ContentType = "application/json";

                using (var requestStream = await webRequest.GetRequestStreamAsync())
                {
                    using (var streamWriter = new StreamWriter(requestStream))
                    {
                        await streamWriter.WriteAsync(body);
                    }
                }
            }

            using (var response = await webRequest.GetResponseAsync())
            {
                var responseStream = response.GetResponseStream();
                using (var streamReader = new StreamReader(responseStream))
                {
                    var readAll = await streamReader.ReadToEndAsync();

                    return JsonSerializer.Deserialize<T>(readAll);
                }
            }
        }
    }

    public enum MethodTypes
    {
        GET = 0,
        PUT = 1
    }
}