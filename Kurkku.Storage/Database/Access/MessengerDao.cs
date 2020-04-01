using System;
using System.Linq;
using System.Collections.Generic;
using Kurkku.Storage.Database.Data.Messenger;
using Kurkku.Storage.Database.Data.Player;
using NHibernate.Linq;

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
    }
}
