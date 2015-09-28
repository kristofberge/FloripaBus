using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("FloripaBusService.Tests")]

namespace FloripaBusService.Service
{
    interface IJsonCreator
    {
        string GetRoutesBody(string street);
        string GetRouteDetailsBody(string routeId);
    }
}
