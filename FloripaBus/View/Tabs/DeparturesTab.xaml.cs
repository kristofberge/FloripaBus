using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloripaBusService.Model;
using FloripaBus.ViewModel;
//using FloripaBus.View.CustomViews;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using FloripaBusService.Service;

namespace FloripaBus.View.Tabs
{
    public partial class DeparturesTab : ContentPage
    {

        public DeparturesTab(Route route, IFloripaBusService floripaBusService)
        {
            InitializeComponent();

            BindingContext = new DeparturesViewModel(route, floripaBusService);
        }
    }
}