using NHibernate.Mapping.ByCode.Conformist;
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
        public virtual int Id { get; set; }

        [DataMember]
        public virtual string Title { get; set; }

        [DataMember]
        public virtual string Content { get; set; }
        
        [DataMember]
        public virtual Geom Geom { get; set; }

        [DataMember]
        public virtual DateTime Created { get; set; }
    }

    internal class IssueItemMapping : ClassMapping<IssueItem>
    {
        public IssueItemMapping()
        {
            Schema("dbo");
            Table("Issue");
            Id(x => x.Id, m => m.Generator(NHibernate.Mapping.ByCode.Generators.Native));
            Property(x => x.Title);
            Property(x => x.Content);
            Property(x => x.Created);
            ManyToOne(x => x.Geom, mapper => mapper.Column("GeomId"));
        }
    }
}
