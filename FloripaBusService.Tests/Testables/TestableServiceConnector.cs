using FloripaBusService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace FloripaBusService.Tests.Testables
{
    internal class TestableServiceConnector : ServiceConnector
    {
        public HttpClient ClientToUSe { get; set; }

        public AuthenticationHeaderValue CallCreateAuthHeader(string username, string password)
        {
            return base.CreateAuthHeader(username, password);
        }

        protected override HttpClient GetHttpClient()
        {
            return ClientToUSe == null ? GetHttpClient() : ClientToUSe;
        }
    }
}
