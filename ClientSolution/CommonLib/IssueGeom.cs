using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
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
