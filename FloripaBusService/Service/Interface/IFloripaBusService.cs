using FloripaBusService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaBusService.Service
{
    public interface IFloripaBusService
    {
        Task<List<Route>> GetRoutesByStreet(string street);
        Task<List<Street>> GetStreetsByRoute(string routeId);
        Task<List<Departure>> GetDeparturesByRoute(string routeId);
    }
}
