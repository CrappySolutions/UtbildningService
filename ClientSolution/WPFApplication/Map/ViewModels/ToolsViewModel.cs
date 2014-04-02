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
        private INavigationResolver _resolver;
        private Uri _addViewUri = new Uri("AddView", UriKind.Relative);
        public ToolsViewModel(INavigationResolver resolver)
        {
            _resolver = resolver;
        }

        public ICommand ShowAddCommand
        {
            get 
            {
                return new DelegateCommand(() => {
                    _resolver.ShowAddIssue(true);
                }); 
            }
        }
    }

    public interface IToolsVIewModel
    {
        ICommand ShowAddCommand { get; }
    }
}
