using FloripaBusService;
using FloripaBusService.Service;
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
    public class DeparturesViewModel : DataHandlingViewModel
    {
        protected IFloripaBusService service;

        public ObservableCollection<Departure> Weekdays { get; private set; }
        public ObservableCollection<Departure> Saturdays { get; private set; }
        public ObservableCollection<Departure> Sundays { get; private set; }

        public DeparturesViewModel(Route route, IFloripaBusService floripaBusService)
        {
            base.Title = "Departures";
            service = floripaBusService;

            FillDepartures(route.Id);
        }

        protected async virtual void FillDepartures(string routeId)
        {
            SetDataLoadingMode();

            var searchResults = await service.GetDeparturesByRoute(routeId);
            var departures = new ObservableCollection<Departure>(searchResults);

            SplitDepartures(departures);

            SetDataLoadingMode(false);
        }

        protected virtual void SplitDepartures(ObservableCollection<Departure> departures)
        {
            Weekdays = new ObservableCollection<Departure>(departures.Where(d => d.Calendar.ToUpper() == "WEEKDAY").ToList<Departure>());
            Saturdays = new ObservableCollection<Departure>(departures.Where(d => d.Calendar.ToUpper() == "SATURDAY").ToList<Departure>());
            Sundays = new ObservableCollection<Departure>(departures.Where(d => d.Calendar.ToUpper() == "SUNDAY").ToList<Departure>());
        }
    }
}

