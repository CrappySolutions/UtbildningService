using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFApplication.Map.ViewModels
{
    public class ToolsViewModel : NotificationObject, IToolsVIewModel
    {
        private IRegionManager _regionManager;

        public ToolsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public ICommand ShowAddCommand
        {
            get 
            {
                return new DelegateCommand(() => { System.Windows.MessageBox.Show("Hello Dude!"); }); 
            
            }
        }
    }

    public interface IToolsVIewModel
    {
        ICommand ShowAddCommand { get; }
    }
}
