using FloripaBus.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaBus.Tests.Testables
{
    public class TestableDataHandlingViewModel : DataHandlingViewModel
    {
        public void CallSetDataLoadingMode(bool isLoading)
        {
            SetDataLoadingMode(isLoading);
        }

        public bool GetIsLoading()
        {
            return IsLoading;
        }

        public bool GetShowData()
        {
            return ShowData;
        }
    }
}
