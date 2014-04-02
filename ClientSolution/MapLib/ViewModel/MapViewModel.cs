using CommonLib;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MapControl;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MapLib.ViewModel
{
    public class MapViewModel : ViewModelBase, INavigationAware, IMapViewModel
    {
        private readonly CommonLib.AwesomeService.IGeoDataService _service;

        private MapMode _mode = MapMode.Standard;
        public MapMode Mode
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

        public ObservableCollection<CommonLib.AwesomeService.IssueItem> Issues { get; set; }

        public ICommand PositionateCommand
        {
            get
            {
                return new RelayCommand<Location>((loc) => { MessengerInstance.Send<IssueGeom>(new IssueGeom { type = "point", coordinates = new List<double> { loc.Longitude, loc.Latitude } }); });
            }
        }

        public MapViewModel(CommonLib.AwesomeService.IGeoDataService service, IMessenger messenger)
            : base(messenger)
        {
            _service = service;
            Issues = new ObservableCollection<CommonLib.AwesomeService.IssueItem>();
            Init();
            CommonLib.IssueRepository.Current.IssueCreated += IssueCreated;
            MessengerInstance.Register<MapMode>(this, ChangeMapMode);
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

        private async void Init()
        {
            try
            {
                var issues = await _service.GetAllIssuesAsync();
                foreach (var issue in issues)
                {
                    await System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                    {
                        Issues.Add(issue);
                    }));
                }
            }
            catch (Exception)
            {

            }
        }

        private void IssueCreated(object sender, CommonLib.IssueRepository.IssueEventArgs args)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
            {
                Issues.Add(args.Issue);
            }));
        }

        private void ChangeMapMode(MapMode mode)
        {
            Mode = mode;
        }
    }

    public interface IMapViewModel
    {

    }
}
