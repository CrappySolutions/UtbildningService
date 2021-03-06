﻿using GalaSoft.MvvmLight.Messaging;
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
            Container.RegisterType<IMessenger, Messenger>(new ContainerControlledLifetimeManager());
            Container.RegisterInstance(CommonLib.IssueRepository.Current);
            var clientConst = new InjectionConstructor(new System.ServiceModel.InstanceContext(CommonLib.IssueRepository.Current));
            Container.RegisterType<CommonLib.AwesomeService.IGeoDataService, CommonLib.AwesomeService.GeoDataServiceClient>(new ContainerControlledLifetimeManager(), clientConst);
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();
            catalog.AddModule(typeof(IssueLib.IssueModule));
            catalog.AddModule(typeof(MapLib.MapModule));
            catalog.AddModule(typeof(NavigationLib.NavigationModule));
            return catalog;
        }
    }


}
