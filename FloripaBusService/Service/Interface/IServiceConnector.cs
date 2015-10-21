using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("FloripaBusService.Tests")]
namespace FloripaBusService.Service
{
    interface IServiceConnector
    {
        Task<HttpResponseMessage> GetPostResponse(string uri, string message);
    }
}
