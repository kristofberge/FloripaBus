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
    public class TestableStreetsViewModel : StreetsViewModel
    {
        public TestableStreetsViewModel(Route route, IFloripaBusService service)
            : base(route, service)
        { }

        public void CallFillStreets(string routeId)
        {
            FillStreets(routeId);
        }
    }
}
