using Kurkku.Storage.Database.Data;
using System.Collections.Generic;

namespace Kurkku.Storage.Database.Access
{
    public class ItemDao
    {
        /// <summary>
        /// Get list of all definition data
        /// </summary>
        public static List<ItemDefinitionData> GetDefinitions()
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<ItemDefinitionData>().List() as List<ItemDefinitionData>;
            }
        }

        /// <summary>
        /// Get list of all item data for user inventory
        /// </summary>
        public static List<ItemData> GetUserItems(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<ItemData>().Where(x => x.OwnerId == userId && x.RoomId == 0).List() as List<ItemData>;
            }
        }

        /// <summary>
        /// Get room items
        /// </summary>
        public static List<ItemData> GetRoomItems(int roomId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<ItemData>().Where(x => x.RoomId == roomId).List() as List<ItemData>;
            }
        }

        /// <summary>
        /// Save item definition
        /// </summary>
        /// <param name="itemDefinition"></param>
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

        /// <summary>
        /// Save item data
        /// </summary>
        public static void SaveItem(ItemData itemData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(itemData);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }


        /// <summary>
        /// Create items and refresh it with their filled in database ID's
        /// </summary>
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

        /// <summary>
        /// Create item and refresh it with their filled in database ID's
        /// </summary>
        public static void CreateItem(ItemData item)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(item);
                        transaction.Commit();
                        session.Refresh(item);
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
