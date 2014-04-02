using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLib
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
            _container.RegisterType<MapLib.ViewModel.IMapViewModel, MapLib.ViewModel.MapViewModel>(new ContainerControlledLifetimeManager());
            _regionManager.RegisterViewWithRegion(CommonLib.RegionNames.MAIN, typeof(View.MapView));
            _regionManager.RegisterViewWithRegion(CommonLib.RegionNames.FOOTER, typeof(View.StatusView));
        }
    }
}
