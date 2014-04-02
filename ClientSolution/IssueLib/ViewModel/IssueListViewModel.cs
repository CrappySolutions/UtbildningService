using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IssueLib.ViewModel
{
    public class IssueListViewModel : ObservableCollection<CommonLib.AwesomeService.IssueItem>, IIssueListViewModel
    {
        private readonly CommonLib.AwesomeService.IGeoDataService _service;
        public IssueListViewModel(CommonLib.AwesomeService.IGeoDataService service)
        {
            _service = service;
            Init();
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
                        Add(issue);
                    }));
                }
            }
            catch (Exception)
            {
                
            }
        }


        private CommonLib.AwesomeService.IssueItem _issueItem = new CommonLib.AwesomeService.IssueItem();
        public CommonLib.AwesomeService.IssueItem IssueItem
        {
            get
            {
                return _issueItem;
            }
            set
            {
                _issueItem = value;
            }
        }

        private ICommand _addIssueCommand;
        public ICommand AddIssueCommand
        {
            get
            {
                if (_addIssueCommand == null)
                {
                    _addIssueCommand = new RelayCommand(() =>
                    {
                        _service.AddIssue(_issueItem);
                        _issueItem = new CommonLib.AwesomeService.IssueItem();
                    });

                }
                return _addIssueCommand;
            }
        }
    }

    public interface IIssueListViewModel
    {
    }
}
