using Kurkku.Storage.Database.Data;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class ItemDao
    {
        public static List<ItemDefinitionData> GetDefinitions()
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<ItemDefinitionData>().List() as List<ItemDefinitionData>;
            }
        }
    }
}
