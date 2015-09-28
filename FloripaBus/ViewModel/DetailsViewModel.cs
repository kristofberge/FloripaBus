using FloripaBusService.Model;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaBus.ViewModel
{
    [ImplementPropertyChanged]
    public class DetailsViewModel : BaseViewModel
    {
        public DetailsViewModel(Route route)
        {
            Title = string.Format("Details for {0}-{1}", route.ShortName, route.LongName);
        }
    }
}