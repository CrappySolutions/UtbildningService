using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Ut
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GeoDataService 
        : IGeoDataService, IDisposable
    {
        public GeoDataService() { }

        bool IGeoDataService.AddIssue(IssueItem issue)
        {
            throw new NotImplementedException();
        }

        bool IGeoDataService.UpdateIssue(IssueItem issue)
        {
            throw new NotImplementedException();
        }

        bool IGeoDataService.RemoveIssueBy(int issueId)
        {
            throw new NotImplementedException();
        }

        ICollection<IssueItem> IGeoDataService.GetAllIssues()
        {
            throw new NotImplementedException();
        }

        IssueItem IGeoDataService.GetItemBy(int issueId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
