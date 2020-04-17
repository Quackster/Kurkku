using Kurkku.Storage.Database.Data;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class CatalogueDao
    {
        public static CataloguePageData GetPageData(int id)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<CataloguePageData>().Where(x => x.Id == id).Take(1).SingleOrDefault();
            }
        }

        public static List<CataloguePageData> GetPages()
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<CataloguePageData>().List() as List<CataloguePageData>;
            }
        }
    }
}
