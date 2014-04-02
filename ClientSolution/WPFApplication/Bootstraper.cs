using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFApplication
{
    public class Bootstraper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return Container.TryResolve<WPFApplication.Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Container.RegisterType<INavigationResolver, NavigationResolver>();
            Container.RegisterType<DataService.IGeoDataService, DataService.GeoDataServiceClient>(new InjectionConstructor());
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();
            catalog.AddModule(typeof(Map.MapModule));
            catalog.AddModule(typeof(Issues.IssuesModule));
            return catalog;
        }
    }

    public static class RegionNames
    {
        public const string HEADER = "Header";
        public const string MAIN = "Main";
    }
}
