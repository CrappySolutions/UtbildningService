using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
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
        static Store()
        {
            _cfg = new Configuration();
            _cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=.;Initial Catalog=UtbildningDatabas;Integrated Security=true;";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
            });
            _cfg.AddAssembly(System.Reflection.Assembly.GetExecutingAssembly());
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
                       session.SaveOrUpdate(item);
                       session.Transaction.Commit();
                       //var customers = session.CreateCriteria<IssueItem>().List<IssueItem>();
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
                    return session.CreateCriteria<IssueItem>().List<IssueItem>();
                }
            }
        }

        public void Dispose()
        {
            _sessionFactory.Dispose();
        }
    }
}
