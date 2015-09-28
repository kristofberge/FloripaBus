using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using FloripaBusService.Exceptions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FloripaBusService.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace FloripaBusService.Service
{

    internal class ResponseHandler
    {
        internal static class Constants
        {
            public static readonly string FIND_ROUTES_BY_STREET = "findRoutesByStopName";
            public static readonly string FIND_STREETS_BY_ROUTE = "findStopsByRouteId";
            public static readonly string FIND_DEPARTURES_BY_ROUTE = "findDeparturesByRouteId";
        }

        private string _uri = "https://api.appglu.com/v1/queries/{0}/run";

        protected IJsonCreator jsonCreator;
        protected IServiceConnector serviceConnector;

        public ResponseHandler(IJsonCreator jsonCreator, IServiceConnector serviceConnector)
        {
            this.jsonCreator = jsonCreator;
            this.serviceConnector = serviceConnector;
        }

        #region Getting reponses
        public virtual async Task<string> GetRoutesByStreet(string street)
        {
            string uri = CreateUri(Constants.FIND_ROUTES_BY_STREET);
            string body = jsonCreator.GetRoutesBody(street);
            HttpResponseMessage response = await serviceConnector.GetPostResponse(uri, body);

            ValidateResponse(response);

            return GetContent(response);
        }

        public virtual async Task<string> GetStreetsByRoute(string routeId)
        {
            string uri = CreateUri(Constants.FIND_STREETS_BY_ROUTE);
            string body = jsonCreator.GetRouteDetailsBody(routeId);
            HttpResponseMessage response = await serviceConnector.GetPostResponse(uri, body);

            ValidateResponse(response);

            return GetContent(response);
        }

        public virtual async Task<string> GetDeparturesByRoute(string routeId)
        {
            string uri = CreateUri(Constants.FIND_DEPARTURES_BY_ROUTE);
            string body = jsonCreator.GetRouteDetailsBody(routeId);
            HttpResponseMessage response = await serviceConnector.GetPostResponse(uri, body);

            ValidateResponse(response);

            return GetContent(response);
        }
        #endregion

        #region Helper methods
        protected virtual string CreateUri(string method)
        {
            return string.Format(_uri, method);
        }

        protected virtual void ValidateResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.GatewayTimeout:
                    case System.Net.HttpStatusCode.RequestTimeout:
                        throw new ApiNotReachableException();
                    case System.Net.HttpStatusCode.NotFound:
                        throw new ItemNotFoundException();
                    default:
                        response.EnsureSuccessStatusCode(); // will throw HttpRequestException
                        break;
                }
            }
        }

        protected virtual string GetContent(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
        #endregion
    }
}
