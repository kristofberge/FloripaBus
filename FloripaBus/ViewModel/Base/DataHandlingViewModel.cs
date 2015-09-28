using FloripaBus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;

namespace FloripaBus.ViewModel
{
    [ImplementPropertyChanged]
    public abstract class DataHandlingViewModel : BaseViewModel
    {

        public virtual bool IsLoading { get; set; }
        public virtual bool ShowData { get; set; }

        protected virtual void SetDataLoadingMode(bool isLoading = true)
        {
            IsLoading = isLoading;
            ShowData = !isLoading;
        }
    }
}