using FloripaBus.ViewModel;
using FloripaBusService.Service;
using FloripaBusService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FloripaBus.View
{
    public partial class ListPage : ContentPage
    {
        public ListPage(IFloripaBusService floripaBusService)
        {
            InitializeComponent();

            BindingContext = new ListViewModel(floripaBusService);
        }

        void SearchButtonPressed(object sender, EventArgs e)
        {
            ((ListViewModel)BindingContext).ShowRoutes.Execute(((SearchBar)sender).Text);
        }

        void RouteTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListViewModel)BindingContext).RouteTapped.Execute(e.Item);
        }
    }
}
