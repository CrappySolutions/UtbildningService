using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NavigationLib.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationAware, INavigationViewModel
    {
        private IRegionManager _regionManager;
        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        private ICommand _toolbarCommand;
        public ICommand ToolbarCommand
        {
            get
            {
                if (_toolbarCommand == null)
                {
                    _toolbarCommand = new RelayCommand<string>((viewName) =>
                    {
                        _regionManager.Regions[CommonLib.RegionNames.MAIN].RequestNavigate(viewName);
                    });
                    
                }
                return _toolbarCommand;
            }
        }
    }

    public interface INavigationViewModel
    {

    }
}
