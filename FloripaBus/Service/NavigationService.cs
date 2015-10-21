using FloripaBus.View;
using FloripaBusService.Service;
using FloripaBusService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FloripaBus.Services
{
    public class NavigationService
    {
        public async Task NavigateToDetailsPage(Route route, IFloripaBusService floripaBusService)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetailsPage(route, floripaBusService));
        }
    }
}
