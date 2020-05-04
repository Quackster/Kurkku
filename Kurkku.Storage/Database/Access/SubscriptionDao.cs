
using Kurkku.Storage.Database.Data;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class SubscriptionDao
    {
        /*public static bool ContainsSubscription(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                SubscriptionData subscriptionDataAlias = null;

                return session.QueryOver(() => subscriptionDataAlias)
                    .Where(() => subscriptionDataAlias.UserId == userId)
                    .And(() => subscriptionDataAlias.ExpireDate > DateTime.Now)
                    .List().Count > 0;
            }
        }*/

        /// <summary>
        /// Get subscription by user id
        /// </summary>
        public static SubscriptionData GetSubscription(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                SubscriptionData subscriptionDataAlias = null;

                return session.QueryOver(() => subscriptionDataAlias)
                    .Where(() => subscriptionDataAlias.UserId == userId)
                    /*.And(() =>subscriptionDataAlias.ExpireDate > DateTime.Now )*/.SingleOrDefault();
            }
        }

        /// <summary>
        /// Get subscription gifts
        /// </summary>
        public static List<SubscriptionGiftData> GetSubscriptionGifts()
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<SubscriptionGiftData>().List() as List<SubscriptionGiftData>;
            }
        }

        /// <summary>
        /// Create subscription by user id
        /// </summary>
        public static void UpdateSubscription(SubscriptionData subscriptionData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(subscriptionData);
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
        /// Save subscription by user id
        /// </summary>
        public static void SaveSubscriptionExpiry(int userId, DateTime expiry)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<SubscriptionData>().Where(x => x.UserId == userId).Update(x => new SubscriptionData { ExpireDate = expiry });
            }
        }

        /// <summary>
        /// Save club duration by user id
        /// </summary>
        public static void SaveSubscriptionAge(int userId, long clubAge, DateTime clubAgeLastUpdate)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Query<SubscriptionData>().Where(x => x.UserId == userId).Update(x => new SubscriptionData { SubscriptionAge = clubAge, SubscriptionAgeLastUpdated = clubAgeLastUpdate });
            }
        }

        /// <summary>
        /// Refreshes subscription data
        /// </summary>
        public static void Refresh(SubscriptionData data)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                session.Refresh(data);
            }
        }
    }
}
