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
            //var issue = (DataService.IssueItem)value;
            //var bl = Newtonsoft.Json.JsonConvert.DeserializeObject<IssueGeom>(issue.WKT);
            //var p = new Pushpin() {  };
            //p.SetValue(MapControl.Map.LocationProperty, new Location(bl.coordinates.Last(), bl.coordinates.First()));
            //p.DataContext = issue;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        
    }

    public class IssueGeom
    {
        public string type { get; set; }

        public List<double> coordinates { get; set; }

        public IssueGeom() { }

        public IssueGeom(string type, IEnumerable<double> coords)
        {
            this.type = type;
            this.coordinates = new List<double>(coords);
        }
    }
}
