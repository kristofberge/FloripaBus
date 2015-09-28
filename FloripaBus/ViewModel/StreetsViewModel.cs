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
    public class StreetsViewModel : DataHandlingViewModel
    {
        public ObservableCollection<Street> Streets { get; set; }

        private IFloripaBusService _api;

        public StreetsViewModel(Route route, IFloripaBusService floripaBusService)
        {
            Title = "Streets";
            _api = floripaBusService;

            FillStreets(route.Id);
        }

        protected async virtual void FillStreets(string routeId)
        {
            SetDataLoadingMode();

            var searchResults = await _api.GetStreetsByRoute(routeId);
            Streets = new ObservableCollection<Street>(searchResults);
            Streets.OrderBy(s => s.Sequence);

            SetDataLoadingMode(false);
        }

    }
}

