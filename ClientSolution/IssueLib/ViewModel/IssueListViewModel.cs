using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueLib.ViewModel
{
    public class IssueListViewModel : ObservableCollection<CommonLib.AwesomeService.IssueItem>, IIssueListViewModel
    {
        private readonly CommonLib.AwesomeService.IGeoDataService _service;
        public IssueListViewModel(CommonLib.AwesomeService.IGeoDataService service)
        {
            _service = service;
            Init();
            CommonLib.IssueRepository.Current.IssueCreated += IssueCreated;
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

        private void IssueCreated(object sender, CommonLib.IssueRepository.IssueEventArgs args)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
            {
                Add(args.Issue);
            }));
        }
    }

    public interface IIssueListViewModel
    {
    }
}
