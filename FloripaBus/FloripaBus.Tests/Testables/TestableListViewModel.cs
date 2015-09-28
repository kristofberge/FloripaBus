using FloripaBus.ViewModel;
using FloripaBusService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaBus.Tests.Testables
{
    public class TestableListViewModel : ListViewModel
    {
        public TestableListViewModel(IFloripaBusService service)
            : base(service)
        { }

        public void CallShowRoutesForStreet(string street)
        {
            ShowRoutesForStreet(street);
        }
    }
}
