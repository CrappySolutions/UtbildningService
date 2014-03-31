using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Map
{
    public class MapModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        public MapModule(IRegionManager manager, IUnityContainer container)
        {
            _regionManager = manager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<Map.ViewModels.IToolsVIewModel, Map.ViewModels.ToolsViewModel>();
            _regionManager.RegisterViewWithRegion("Header", typeof(Views.ToolsView));
            //_regionManager.RegisterViewWithRegion("Header", typeof(Views.StatusView));
            _regionManager.RegisterViewWithRegion("Main", typeof(Views.MapView));
        }
    }
}
