using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ut.Data;

namespace Ut
{
    [ServiceContract(CallbackContract=typeof(IGeoDataCallback))]
    public interface IGeoDataService
    {
        [OperationContract(IsOneWay=true)]
        void AddIssue(IssueItem issue);

        [OperationContract]
        bool UpdateIssue(IssueItem issue);

        [OperationContract]
        bool RemoveIssueBy(int issueId);

        [OperationContract]
        ICollection<IssueItem> GetAllIssues();

        [OperationContract]
        IssueItem GetItemBy(int issueId);

    }

    public interface IGeoDataCallback
    {
        [OperationContract(IsOneWay = true)]
        void IssueAdded(IssueItem issue);
    }
}
