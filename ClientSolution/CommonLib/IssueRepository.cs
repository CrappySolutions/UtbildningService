using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class IssueRepository 
        : AwesomeService.IGeoDataServiceCallback
    {
        public sealed class IssueEventArgs: EventArgs
	    {
            public AwesomeService.IssueItem Issue { get; private set; }

            public IssueEventArgs(AwesomeService.IssueItem issue)
            {
                Issue = issue;
            }
        }

        public event Action<object, IssueEventArgs> IssueCreated;

        private static IssueRepository _instance;
        public static IssueRepository Current
        {
            get { return _instance ?? (_instance = new IssueRepository()); }
        }

        private IssueRepository()
        {

        }


        void AwesomeService.IGeoDataServiceCallback.IssueAdded(AwesomeService.IssueItem issue)
        {
            if (IssueCreated != null)
                IssueCreated(this, new IssueEventArgs(issue));
        }
    }
}
