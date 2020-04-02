using Kurkku.Storage.Database.Data;
using System.Collections.Generic;

namespace Kurkku.Storage.Database.Access
{
    public class MessengerDao
    {
        public static List<MessengerRequestData> GetRequests(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                MessengerRequestData messengerRequestAlias = null;
                PlayerData playerDataAlias = null;

                return session.QueryOver(() => messengerRequestAlias)
                    .JoinQueryOver(() => messengerRequestAlias.FriendData, () => playerDataAlias)
                    .Where(() => messengerRequestAlias.UserId == userId)
                    .List() as List<MessengerRequestData>;
            }
        }

        public static List<MessengerFriendData> GetFriends(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                MessengerFriendData messengerFriendAlias = null;
                PlayerData playerDataAlias = null;

                return session.QueryOver(() => messengerFriendAlias)
                    .JoinQueryOver(() => messengerFriendAlias.FriendData, () => playerDataAlias)
                    .Where(() => messengerFriendAlias.UserId == userId)
                    .List() as List<MessengerFriendData>;
            }
        }

        public static List<MessengerCategoryData> GetCategories(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                MessengerCategoryData messengerCategoryAlias = null;

                return session.QueryOver(() => messengerCategoryAlias)
                    .Where(() => messengerCategoryAlias.UserId == userId)
                    .List() as List<MessengerCategoryData>;
            }
        }
    }
}
