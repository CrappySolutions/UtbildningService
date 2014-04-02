using MapControl;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFApplication.Map.ViewModels
{
    public class MapViewModel : NotificationObject, IMapViewModel, Microsoft.Practices.Prism.Regions.INavigationAware
    {
        private DataService.IGeoDataService _service;
        private IRegionManager _regionManager;

        public ObservableCollection<DataService.IssueItem> Issues { get; set; }

        private Views.MapView.MapMode _mode = Views.MapView.MapMode.Standard;        
        public Views.MapView.MapMode Mode
        {
            get { return _mode; }
            set 
            {
                _mode = value;
                RaisePropertyChanged("Mode");
            }
        }

        public ICommand PositionateCommand
        {
            get 
            {
                return new DelegateCommand<Location>((loc) => {
                    var geom = Newtonsoft.Json.JsonConvert.SerializeObject(new Converters.IssueGeom("Point", new double[] { loc.Latitude, loc.Longitude }));
                    var uriQuery = new UriQuery();
                    uriQuery.Add("Geom", geom);
                    _regionManager.RequestNavigate(RegionNames.MAIN, new Uri("AddView" + uriQuery.ToString(), UriKind.Relative));
                }); 
            }
        }


        public MapViewModel(IRegionManager regionManager, DataService.IGeoDataService service) 
        {
            _regionManager = regionManager;
            _service = service;
            Issues = new ObservableCollection<DataService.IssueItem>();
            InitData();
        }

        private async void InitData()
        {
            try
            {
                var items = await _service.GetAllIssuesAsync();
                items.ToList().ForEach(i =>
                    App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => {
                        try
                        {
                            Issues.Add(i);
                        }
                        catch (Exception)
                        {
                            
                        }
                    })));
            }
            catch (Exception)
            {
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
           var param =  navigationContext.Parameters["Positionate"];
           if (param != null)
           {
               Mode = Views.MapView.MapMode.Positionate;
           }
           else
           {
               Mode = Views.MapView.MapMode.Standard;
           }
        }
    }

    public interface IMapViewModel
    {

    }
}
