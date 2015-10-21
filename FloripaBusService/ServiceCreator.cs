using FloripaBusService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaBusService
{
    public static class ServiceCreator
    {
        public static IFloripaBusService GetFloripaBusService()
        {
            return new Service.FloripaBusService(new ResponseHandler(new JsonCreator(), new ServiceConnector()));
        }
    }
}