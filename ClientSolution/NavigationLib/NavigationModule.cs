using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationLib
{
    public class NavigationModule : IModule
  {
    private readonly IRegionManager _regionManager;
    private readonly IUnityContainer _container;

    public NavigationModule(IRegionManager manager, IUnityContainer container)
    {
      _regionManager = manager;
      _container = container;
    }

    public void Initialize()
    {

    }
  }
}
