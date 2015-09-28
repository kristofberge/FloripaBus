using FloripaBus.Services;
using FloripaBusService;
using FloripaBusService.Service;
using FloripaBusService.Exceptions;
using FloripaBusService.Model;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FloripaBus.ViewModel
{
    [ImplementPropertyChanged]
    public class ListViewModel : DataHandlingViewModel
    {

        public static class Messages
        {
            public static readonly string UNREACHABLE = "Web service unreachable";
            public static readonly string NOT_FOUND = "Street not found";
            public static readonly string UNKNOWN = "An unknown error occurred";
            public static readonly string DEFAULT = "Passing routes:";
        }

        public virtual ObservableCollection<Route> Routes { get; set; }
        public virtual string Message { get; set; }

        private readonly NavigationService _navigationService;
        private readonly IFloripaBusService _api;

        public ListViewModel(IFloripaBusService floripaBusService)
        {
            Title = "Floripa Bus";
            ShowRoutes = new Command<string>(ShowRoutesForStreet);
            RouteTapped = new Command<Route>(GoToRouteDetails);
            _navigationService = new NavigationService();
            _api = floripaBusService;
        }

        protected async void ShowRoutesForStreet(string street)
        {
            SetDataLoadingMode();

            Routes = new ObservableCollection<Route>();
            try
            {
                var searchResult = await _api.GetRoutesByStreet(street);
                Routes = new ObservableCollection<Route>(searchResult);
                Message = Messages.DEFAULT;
            }
            catch (ApiNotReachableException)
            {
                Message = Messages.UNREACHABLE;
            }
            catch (ItemNotFoundException)
            {
                Message = Messages.NOT_FOUND;
            }
            catch (Exception)
            {
                Message = Messages.UNKNOWN;
            }

            SetDataLoadingMode(false);
        }

        private async void GoToRouteDetails(Route route)
        {
            await _navigationService.NavigateToDetailsPage(route, _api);
        }

        public Command<string> ShowRoutes;
        public Command<Route> RouteTapped;
    }
}
