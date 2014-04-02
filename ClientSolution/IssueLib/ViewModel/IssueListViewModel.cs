using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
        private readonly IMessenger _messenger;
        public IssueListViewModel(CommonLib.AwesomeService.IGeoDataService service, IMessenger messenger)
        {
            _messenger = messenger;
            _messenger.Register<CommonLib.IssueGeom>(this, AddGeom);
            _service = service;
            Init();
            CommonLib.IssueRepository.Current.IssueCreated += IssueCreated;
        }

        private void AddGeom(CommonLib.IssueGeom geom)
        {
            _issueItem.Geom = new CommonLib.AwesomeService.Geom
            {
                Type = geom.type,
                Coordinates = Newtonsoft.Json.JsonConvert.SerializeObject(geom.coordinates)
            };
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
            get {return _issueItem;}
            set{_issueItem = value;}
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
                        _issueItem.Created = DateTime.Now;
                        _service.AddIssue(_issueItem);
                        _issueItem = new CommonLib.AwesomeService.IssueItem();
                    });

                }
                return _addIssueCommand;
            }
        }

        private ICommand _addPositionCommand;
        public ICommand AddPositionCommand
        {
            get
            {
                if (_addPositionCommand == null)
                {
                    _addPositionCommand = new RelayCommand(() =>
                    {
                        _messenger.Send<CommonLib.MapMode>(CommonLib.MapMode.Positionate);
                    });

                }
                return _addPositionCommand;
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
