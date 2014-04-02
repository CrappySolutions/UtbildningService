using CommonLib;
using MapControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MapLib.Converters
{
    public class IssuePushPinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var issue = (CommonLib.AwesomeService.IssueItem)value;
            var bl = Newtonsoft.Json.JsonConvert.DeserializeObject<IssueGeom>(issue.WKT);
            var p = new Pushpin() { };
            p.SetValue(MapControl.Map.LocationProperty, new Location(bl.coordinates.Last(), bl.coordinates.First()));
            p.DataContext = issue;
            return p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        
    }

}
