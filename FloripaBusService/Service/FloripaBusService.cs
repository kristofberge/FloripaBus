using FloripaBusService.Exceptions;
using FloripaBusService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

[assembly: InternalsVisibleTo("FloripaBusService.Tests")]

namespace FloripaBusService.Service
{
    public class FloripaBusService : IFloripaBusService
    {
        internal ResponseHandler client;

        internal FloripaBusService(ResponseHandler client)
        {
            this.client = client;
        }

        public virtual async Task<List<Route>> GetRoutesByStreet(string street)
        {
            string json = await client.GetRoutesByStreet(street);

            var objDefinition = new RowsObject<Route>();
            var routes = JsonConvert.DeserializeAnonymousType(json, objDefinition).rows;

            if (routes.Count == 0)
                throw new ItemNotFoundException(ItemNotFoundException.ItemType.Street);

            return routes;
        }

        public virtual async Task<List<Street>> GetStreetsByRoute(string routeId)
        {
            string json = await client.GetStreetsByRoute(routeId);

            var objDefinition = new RowsObject<Street>();
            var streets = JsonConvert.DeserializeAnonymousType(json, objDefinition).rows;

            if (streets.Count == 0)
                throw new ItemNotFoundException(ItemNotFoundException.ItemType.Route);

            return streets;
        }

        public virtual async Task<List<Departure>> GetDeparturesByRoute(string routeId)
        {
            string json = await client.GetDeparturesByRoute(routeId);

            var objDefinition = new RowsObject<Departure>();
            var departures = JsonConvert.DeserializeAnonymousType(json, objDefinition).rows;

            if (departures.Count == 0)
                throw new ItemNotFoundException(ItemNotFoundException.ItemType.Route);

            return departures;
        }

        private class RowsObject<T>
        {
            public List<T> rows { get; set; }
        }
    }
}
