using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Issues
{
    public class IssuesModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        public IssuesModule(IRegionManager manager, IUnityContainer container)
        {
            _regionManager = manager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<object, Views.AddView>("AddView");
            _container.RegisterType<object, Views.AddToolsView>("AddToolsView");
            _container.RegisterType<ViewModels.IAddViewModel, ViewModels.AddViewModel>(new ContainerControlledLifetimeManager());
            _regionManager.RegisterViewWithRegion(RegionNames.HEADER, typeof(Views.AddToolsView));
            _regionManager.RegisterViewWithRegion(RegionNames.MAIN, typeof(Views.AddView));
        }
    }
}
