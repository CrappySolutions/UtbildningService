using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Map.ViewModels
{
    public class MapViewModel : NotificationObject, IMapViewModel
    {
        private DataService.IGeoDataService _service;

        public ObservableCollection<DataService.IssueItem> Issues { get; set; }
        
        public MapViewModel(DataService.IGeoDataService service) 
        {
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


    }

    public interface IMapViewModel
    {

    }
}
