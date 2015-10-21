using FloripaBusService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("FloripaBusService.Tests")]
namespace FloripaBusService.Service
{
    internal class ServiceConnector : IServiceConnector
    {
        private string _contentType = "application/json";
        private string _customHeaderName = "X-AppGlu-Environment";
        private string _customHeaderValue = "staging";
        private string _username = "WKD4N7YMA1uiM8V";
        private string _password = "DtdTtzMLQlA0hk2C1Yi5pLyVIlAQ68";

        public virtual async Task<HttpResponseMessage> GetPostResponse(string uri, string message)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(message, Encoding.UTF8, _contentType);
            request.Headers.Add(_customHeaderName, _customHeaderValue);

            using (var client = GetHttpClient())
            {
                client.DefaultRequestHeaders.Authorization = CreateAuthHeader(_username, _password);
                try
                {
                    return await client.SendAsync(request);
                }
                catch (AggregateException)
                {
                    throw new ApiNotReachableException();
                }
            }
        }

        protected virtual HttpClient GetHttpClient()
        {
            return new HttpClient();
        }

        protected virtual AuthenticationHeaderValue CreateAuthHeader(string username, string password)
        {
            string concatenated = string.Format("{0}:{1}", username, password);
            byte[] byteArray = Encoding.UTF8.GetBytes(concatenated);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}
