using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ut.Data;

namespace Ut
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GeoDataService 
        : IGeoDataService, IDisposable
    {
        private readonly Data.Store _store;

        public GeoDataService() { _store = new Store(); }

        bool IGeoDataService.AddIssue(IssueItem issue)
        {
            try
            {
                _store.Add(issue);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
            
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
            return _store.GetAllIssues();
        }

        IssueItem IGeoDataService.GetItemBy(int issueId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _store.Dispose();
        }
    }
}
