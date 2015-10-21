using System;
using FloripaBus.View;
using Xamarin.Forms;
using FloripaBusService;

namespace FloripaBus
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new ListPage(ServiceCreator.GetFloripaBusService()));
        }
    }
}