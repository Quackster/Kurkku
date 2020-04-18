using Kurkku.Storage.Database.Data;
using System.Collections.Generic;

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
