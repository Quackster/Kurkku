using Kurkku.Storage.Database.Data;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class MessengerDao
    {
        /// <summary>
        /// Search messenger for names starting with the query
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get the requests for the user
        /// </summary>
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

        /// <summary>
        /// Get the friends for the user
        /// </summary>
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

        /// <summary>
        /// Get the messenger categories for the user
        /// </summary>
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

        /// <summary>
        /// Get if the user supports friend requests
        /// </summary>
        public static bool GetAcceptsFriendRequests(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                PlayerSettingsData settingsAlias = null;
                PlayerData playerDataAlias = null;

                return session.QueryOver(() => settingsAlias)
                    .JoinEntityAlias(() => playerDataAlias, () => settingsAlias.UserId == playerDataAlias.Id)
                    .Where(() => playerDataAlias.Id == userId && settingsAlias.FriendRequestsEnabled)
                    .List().Count > 0;
            }
        }

        /// <summary>
        /// Deletes friend requests
        /// </summary>
        public static void DeleteRequests(int userId, int friendId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<MessengerRequestData>().Where(x => 
                    (x.FriendId == friendId && x.UserId == userId) || 
                    (x.FriendId == userId && x.UserId == friendId))
                .Delete();
            }
        }

        /// <summary>
        /// Delete all requests by user id
        /// </summary>
        public static void DeleteAllRequests(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<MessengerRequestData>().Where(x =>
                    (x.FriendId == userId || x.UserId == userId))
                .Delete();
            }
        }

        /// <summary>
        /// Deletes friends
        /// </summary>
        public static void DeleteFriends(int userId, int friendId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<MessengerFriendData>().Where(x =>
                    (x.FriendId == friendId && x.UserId == userId) ||
                    (x.FriendId == userId && x.UserId == friendId))
                .Delete();
            }
        }

        /// <summary>
        /// Save a request
        /// </summary>
        public static void SaveRequest(MessengerRequestData messengerRequestData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(messengerRequestData);
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
        /// Save a request
        /// </summary>
        public static void SaveFriend(MessengerFriendData messengerFriendData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(messengerFriendData);
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
        /// Save a message
        /// </summary>
        public static void SaveMessage(MessengerChatData messengerChatData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(messengerChatData);
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
        /// Deletes friend requests
        /// </summary>
        public static void SetReadMessages(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<MessengerChatData>().Where(x => x.FriendId == userId && !x.IsRead).Update(x => new MessengerChatData { IsRead = true });
            }
        }

        /// <summary>
        /// Deletes friend requests
        /// </summary>
        public static List<MessengerChatData> GetUneadMessages(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<MessengerChatData>().Where(x => x.FriendId == userId && !x.IsRead).List() as List<MessengerChatData>;
            }
        }
    }
}
