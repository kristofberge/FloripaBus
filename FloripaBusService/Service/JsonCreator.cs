using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("FloripaBusService.Tests")]

namespace FloripaBusService.Service
{
    internal class JsonCreator : IJsonCreator
    {
        //We cannot use anonymous objects to construct this json string,
        //because "params" is a keyword in C#.
        public string GetRoutesBody(string street)
        {
            return "{\"params\":{\"stopName\":\"%" + street + "%\"}}";
        }

        public string GetRouteDetailsBody(string routeId)
        {
            return "{\"params\":{\"routeId\":\"" + routeId + "\"}}";
        }
    }
}
