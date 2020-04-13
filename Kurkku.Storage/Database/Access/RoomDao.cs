using Kurkku.Storage.Database.Data;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class RoomDao
    {

        /// <summary>
        /// Get the list of users' own rooms
        /// </summary>
        public static List<RoomData> GetUserRooms(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                RoomData roomDataAlias = null;
                return session.QueryOver<RoomData>(() => roomDataAlias).Where(() => roomDataAlias.OwnerId == userId).List() as List<RoomData>;
            }
        }

        /// <summary>
        /// Count the rooms the user has.
        /// </summary>
        public static int CountUserRooms(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<RoomData>().Where(x => x.OwnerId == userId).RowCount();
            }
        }

        /// <summary>
        /// Get the room model data
        /// </summary>
        /// <returns></returns>
        public static List<RoomModelData> GetModels()
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<RoomModelData>().List() as List<RoomModelData>;
            }
        }

        /// <summary>
        /// Get data for room
        /// </summary>
        public static RoomData GetRoomData(int roomId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<RoomData>().Where(x => x.Id == roomId).Take(1).SingleOrDefault();
            }
        }

        /// <summary>
        /// Save room data
        /// </summary>
        public static void SaveRoom(RoomData data)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(data);
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
        /// Reset all visitors
        /// </summary>
        public static void ResetVisitorCounts()
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<RoomData>().Where(x => x.UsersNow > 0 || x.UsersNow < 0).Update(x => new RoomData { UsersNow = 0 });
            }
        }

        /// <summary>
        /// Update users count
        /// </summary>
        public static void SetVisitorCount(int roomId, int visitorsNow)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<RoomData>().Where(x => x.Id == roomId).Update(x => new RoomData { UsersNow = visitorsNow });
            }
        }
    }
}
