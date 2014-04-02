using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MapControl;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLib.ViewModel
{
    public class MapViewModel : ViewModelBase, INavigationAware, IMapViewModel
    {
        private View.MapView.MapMode _mode = View.MapView.MapMode.Standard;
        public View.MapView.MapMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                RaisePropertyChanged("Mode");
            }
        }

        private Location _mouseLocation;
        public Location MouseLocation
        {
            get { return _mouseLocation; }
            set 
            {
                _mouseLocation = value;
                RaisePropertyChanged(() => MouseLocation);
            }
        }
        

        bool Microsoft.Practices.Prism.Regions.INavigationAware.IsNavigationTarget(Microsoft.Practices.Prism.Regions.NavigationContext navigationContext)
        {
            return true;
        }

        void Microsoft.Practices.Prism.Regions.INavigationAware.OnNavigatedFrom(Microsoft.Practices.Prism.Regions.NavigationContext navigationContext)
        {

        }

        void Microsoft.Practices.Prism.Regions.INavigationAware.OnNavigatedTo(Microsoft.Practices.Prism.Regions.NavigationContext navigationContext)
        {

        }

        public MapViewModel(IMessenger messenger)
            : base(messenger)
        {

        }
    }

    public interface IMapViewModel
    {

    }
}
