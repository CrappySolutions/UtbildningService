using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ut.Data
{
    public class Store : IDisposable
    {
        private static Configuration _cfg;

        private static HbmMapping CreateMapping()
        {
            var mapper = new ModelMapper();
            //Add the person mapping to the model mapper
            mapper.AddMappings(new List<System.Type> { typeof(IssueItemMapping), typeof(GeomMapping) });
            //Create and return a HbmMapping of the model mapping in code
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        static Store()
        {
            _cfg = new Configuration();
           // _cfg.Configure();
            _cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=.;Initial Catalog=UtbildningDatabas;Integrated Security=true;";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
            });
            _cfg.AddDeserializedMapping(CreateMapping(), null);
            //_cfg.AddAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }
        private readonly ISessionFactory _sessionFactory;
        public Store()
        {
            _sessionFactory = _cfg.BuildSessionFactory();
        }

        public void Add(IssueItem item)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    session.SaveOrUpdate(item.Geom);
                    session.SaveOrUpdate(item);
                    session.Transaction.Commit();
                }
            }
        }

        public void Update(IssueItem item)
        {

        }

        public void Delete(int issueId)
        {

        }

        public ICollection<IssueItem> GetAllIssues() 
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var items = session.CreateCriteria<IssueItem>().List<IssueItem>();

                    return items;
                }
            }
        }

        public void Dispose()
        {
            _sessionFactory.Dispose();
        }
    }
}
