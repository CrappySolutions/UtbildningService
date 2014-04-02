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
            _regionManager.RegisterViewWithRegion("Main", typeof(View.MapView));
        }
    }
}
