using Kurkku.Storage.Database.Data;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class TagDao
    {
        /// <summary>
        /// Delete tags for room
        /// </summary>
        public static void DeleteRoomTags(int roomId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<TagData>().Where(x => x.RoomId == roomId).Delete();
            }
        }

        /// <summary>
        /// Save the room tags
        /// </summary>
        /// <returns></returns>
        public static void SaveTag(TagData tagData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(tagData);
                        transaction.Commit();
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
