using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloripaBusService.Model;
using FloripaBus.ViewModel;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using FloripaBusService.Service;

namespace FloripaBus.View.Tabs
{
    public partial class StreetsTab : ContentPage
    {
        public StreetsTab(Route route, IFloripaBusService floripaBusService)
        {
            InitializeComponent();

            BindingContext = new StreetsViewModel(route, floripaBusService);
        }

        void StreetTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null; //Unselect the item
        }
    }
}