using Kurkku.Storage.Database.Data;
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
    }
}
