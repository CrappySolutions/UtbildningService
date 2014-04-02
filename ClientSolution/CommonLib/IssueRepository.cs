using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class IssueRepository : AwesomeService.IGeoDataServiceCallback
    {
        private static IssueRepository _instance;
        public static IssueRepository Current
        {
            get { return _instance ?? (_instance = new IssueRepository());}
        }

        private IssueRepository()
        {

        }

        public void IssueAdded(AwesomeService.IssueItem issue)
        {
            
        }
    }
}
