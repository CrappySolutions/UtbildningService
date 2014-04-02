using GalaSoft.MvvmLight;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationLib.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationAware
    {
        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
