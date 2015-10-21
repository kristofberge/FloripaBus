using FloripaBus.ViewModel;
using FloripaBusService.Model;
using FloripaBusService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaBus.Tests.Testables
{
    public class TestableDeparturesViewModel : DeparturesViewModel
    {
        public TestableDeparturesViewModel(Route route, IFloripaBusService service)
            : base(route, service)
        { }

        public void CallFillDepartures(string routeId)
        {
            FillDepartures(routeId);
        }
    }
}
