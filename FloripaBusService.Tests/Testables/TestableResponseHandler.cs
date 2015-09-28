using FloripaBusService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FloripaBusService.Tests.Testables
{
    internal class TestableResponseHandler : ResponseHandler
    {
        public TestableResponseHandler(IJsonCreator jsonCreator, IServiceConnector connector)
            : base(jsonCreator, connector)
        { }

        public void CallValidate(HttpResponseMessage response)
        {
            base.ValidateResponse(response);
        }

        public string CallGetContent(HttpResponseMessage response)
        {
            return base.GetContent(response);
        }

        public string CallCreateUri(string method)
        {
            return CreateUri(method);
        }
    }
}
