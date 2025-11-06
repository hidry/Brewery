using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Brewery.ServiceAdapter
{
    public class RequestHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://minwinpc:8800/api/")
        };

        private class EmptyBody { }

        public async Task SendRequest(string method, MethodTypes methodType, string body = null)
        {
            await SendRequest<EmptyBody>(method, methodType, body);
        }

        public async Task<T> SendRequest<T>(string method, MethodTypes methodType, string body = null)
        {
            HttpResponseMessage response;

            if (methodType == MethodTypes.GET)
            {
                response = await _httpClient.GetAsync(method);
            }
            else if (methodType == MethodTypes.PUT)
            {
                HttpContent content = null;
                if (!string.IsNullOrWhiteSpace(body))
                {
                    content = new StringContent(body, Encoding.UTF8, "application/json");
                }
                response = await _httpClient.PutAsync(method, content);
            }
            else
            {
                throw new NotSupportedException($"Method type {methodType} is not supported");
            }

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(EmptyBody))
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(responseBody);
        }
    }

    public enum MethodTypes
    {
        GET = 0,
        PUT = 1
    }
}
