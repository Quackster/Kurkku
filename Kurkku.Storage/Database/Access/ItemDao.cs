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

        public static List<ItemData> GetUserItems(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<ItemData>().List() as List<ItemData>;
            }
        }

        public static void SaveDefinition(ItemDefinitionData itemDefinition)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(itemDefinition);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public static void CreateItems(List<ItemData> items)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var itemData in items)
                            session.Save(itemData);

                        transaction.Commit();

                        foreach (var itemData in items)
                            session.Refresh(itemData);
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
