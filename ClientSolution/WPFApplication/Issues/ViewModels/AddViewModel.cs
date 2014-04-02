using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFApplication.Issues.ViewModels
{
    [System.Runtime.Serialization.DataContractAttribute(Name = "IssueItem", Namespace = "http://schemas.datacontract.org/2004/07/Ut.Data")]
    public class AddViewModel
        : DataService.IssueItem, IAddViewModel, INavigationAware
    {
        private DataService.IGeoDataService _service;
        private IRegionManager _regionManager;
         
        public AddViewModel(IRegionManager regionManager, DataService.IGeoDataService service)
        {
            _regionManager = regionManager;
            _service = service;
        }

        public ICommand PositionCommand
        {
            get 
            {
                return new DelegateCommand(() => {
                    if (IsMapVisible)
                    {
                        var uriQuery = new UriQuery();
                        uriQuery.Add("Positionate", "True");
                        _regionManager.RequestNavigate(RegionNames.MAIN, new Uri("MapView" + uriQuery.ToString(), UriKind.Relative));
                    }
                    else 
                    {
                        _regionManager.RequestNavigate(RegionNames.MAIN, new Uri("AddView", UriKind.Relative));
                    }
                }); 
            }
        }

        private DelegateCommand _command;
        public ICommand AddCommand
        {
            get 
            {
                return _command ?? (_command = new DelegateCommand(async () => {
                    try
                    {
                        this.Created = DateTime.Now;
                        this.WKT = "{type: \"Point\",coordinates: [13.527184819038629,59.37560622212426]}";
                        if (await _service.AddIssueAsync(this))
                        {
                            NavigateAndClear();
                        }
                    }
                    catch {
                        System.Windows.MessageBox.Show("Misslyckades med att spara.", "Spara", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Asterisk);
                    }
                }, () => !string.IsNullOrEmpty(Title) && IsValidGeoJSON())); 
            }
        }


        public ICommand CancelCommand
        {
            get 
            {
                return new DelegateCommand(NavigateAndClear); 
            }
        }

        public new string Title
        {
            get { return base.Title; }
            set 
            {
                if (base.Title != value)
                {
                    base.Title = value;
                    _command.RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isMapVisible;
        public bool IsMapVisible
        {
            get { return _isMapVisible; }
            set 
            {
                _isMapVisible = value;
                RaisePropertyChanged("IsMapVisible");
            }
        }

        private bool IsValidGeoJSON() 
        {
            return true;
        }

        private void NavigateAndClear()
        {
            _regionManager.RequestNavigate(RegionNames.HEADER, new Uri("ToolsView", UriKind.Relative));
            _regionManager.RequestNavigate(RegionNames.MAIN, new Uri("MapView", UriKind.Relative));
            Title = string.Empty;
            Content = string.Empty;
            this.WKT = string.Empty;
            _command.RaiseCanExecuteChanged();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return !string.IsNullOrEmpty(this.WKT);
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var param = navigationContext.Parameters["Geom"];
            if (param != null)
            {
                this.WKT = param;
                _command.RaiseCanExecuteChanged();
            }
        }
    }

    public interface IAddViewModel
    {

    }
}
