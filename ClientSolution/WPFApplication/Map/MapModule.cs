using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
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

        public MapModule(IRegionManager manager)
        {
            _regionManager = manager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("Header", typeof(Views.ToolsView));
            //_regionManager.RegisterViewWithRegion("Header", typeof(Views.StatusView));
            _regionManager.RegisterViewWithRegion("Main", typeof(Views.MapView));
        }
    }
}
