using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloripaBusService.Model;
using Xamarin.Forms;
using FloripaBus.ViewModel;
using FloripaBus.View.Tabs;
using FloripaBusService.Service;

namespace FloripaBus.View
{
    public partial class DetailsPage : TabbedPage
    {
        public DetailsPage(Route route, IFloripaBusService floripaBusService)
        {
            InitializeComponent();

            Children.Add(new StreetsTab(route, floripaBusService));
            Children.Add(new DeparturesTab(route, floripaBusService));

            BindingContext = new DetailsViewModel(route);
        }
    }
}