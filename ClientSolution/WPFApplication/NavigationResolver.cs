using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication
{
    public class NavigationResolver : INavigationResolver
    {
        public void ShowMap(bool showToolbar = false)
        {
            if(showToolbar)
                _regionManager.RequestNavigate(RegionNames.HEADER, new Uri("ToolsView", UriKind.Relative));
            _regionManager.RequestNavigate(RegionNames.MAIN, new Uri("MapView", UriKind.Relative));
        }

        public void RequestPosition()
        {
            var uriQuery = new UriQuery();
            uriQuery.Add("Positionate", "True");
            _regionManager.RequestNavigate(RegionNames.MAIN, new Uri("MapView" + uriQuery.ToString(), UriKind.Relative));
        }

        public void ShowAddIssueWithGeom(string geom)
        {
            var uriQuery = new UriQuery();
            uriQuery.Add("Geom", geom);
            _regionManager.RequestNavigate(RegionNames.MAIN, new Uri("AddView" + uriQuery.ToString(), UriKind.Relative));
        }

        public void ShowAddIssue(bool showToolbar = false)
        {
            if(showToolbar)
                _regionManager.Regions[RegionNames.HEADER].RequestNavigate("AddToolsView");
            _regionManager.RequestNavigate(RegionNames.MAIN, new Uri("AddView", UriKind.Relative));
        }

        private readonly IRegionManager _regionManager;
        public NavigationResolver(IRegionManager manger) 
        {
            _regionManager = manger;
        }
    }

    public interface INavigationResolver
    {
        void ShowMap(bool showToolbar = false);

        void RequestPosition();

        void ShowAddIssueWithGeom(string geom);

        void ShowAddIssue(bool showToolbar = false);
    }
}
