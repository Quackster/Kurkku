using Kurkku.Storage.Database.Data;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using System;
using System.Collections.Generic;

namespace Kurkku.Storage.Database.Access
{
    public class MessengerDao
    {
        public static List<PlayerData> SearchMessenger(string query, int ignoreUserId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                PlayerData playerDataAlias = null;

                return session.QueryOver<PlayerData>(() => playerDataAlias)
                                        //.Where(Restrictions.On<PlayerData>(x => x.Name).IsInsensitiveLike(query, MatchMode.Start))
                    .WhereRestrictionOn(() => playerDataAlias.Name).IsLike(query, MatchMode.Start)
                    .And(() => playerDataAlias.Id != ignoreUserId)
                    .Take(30)
                    .List() as List<PlayerData>;
            }
        }

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

        public static bool GetAcceptsFriendRequests(int userId)
        {
            PlayerSettingsData settingsAlias = null;
            PlayerData playerDataAlias = null;

            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver(() => settingsAlias)
                    .JoinEntityAlias(() => playerDataAlias, () => settingsAlias.UserId == playerDataAlias.Id)
                    .Where(() => playerDataAlias.Id == userId && settingsAlias.FriendRequestsEnabled)
                    .List().Count > 0;
            }
        }
    }
}
