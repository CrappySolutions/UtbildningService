using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Ut
{
    [ServiceContract]
    public interface IGeoDataService
    {
        [OperationContract]
        bool AddIssue(IssueItem issue);

        [OperationContract]
        bool UpdateIssue(IssueItem issue);

        [OperationContract]
        bool RemoveIssueBy(int issueId);

        [OperationContract]
        ICollection<IssueItem> GetAllIssues();

        [OperationContract]
        IssueItem GetItemBy(int issueId);

    }

    [DataContract]
    public class IssueItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string WKT { get; set; }

        [DataMember]
        public DateTime Created { get; set; }
    }
}
