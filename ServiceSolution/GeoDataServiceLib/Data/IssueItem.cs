using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ut.Data
{
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
