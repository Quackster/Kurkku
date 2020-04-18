﻿using Kurkku.Storage.Database.Data;
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

        public static List<CatalogueItemData> GetItems()
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<CatalogueItemData>().List() as List<CatalogueItemData>;
            }
        }

        public static List<CataloguePackageData> GetPackages()
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<CataloguePackageData>().List() as List<CataloguePackageData>;
            }
        }
    }
}
