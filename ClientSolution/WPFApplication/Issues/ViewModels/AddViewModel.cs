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
        : DataService.IssueItem, IAddViewModel
    {
        private DataService.IGeoDataService _service;
        private IRegionManager _regionManager;
         
        public AddViewModel(IRegionManager regionManager, DataService.IGeoDataService service)
        {
            _regionManager = regionManager;
            _service = service;
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
                        var status = await _service.AddIssueAsync(this);
                        _regionManager.RequestNavigate("Main", new Uri("MapView", UriKind.Relative));
                    }
                    catch { }
                }, () => !string.IsNullOrEmpty(Title) && IsValidGeoJSON())); 
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

        private bool IsValidGeoJSON() 
        {
            return true;
        }
    }

    public interface IAddViewModel
    {

    }
}
