
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;

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
                    .And(() =>subscriptionDataAlias.ExpireDate > DateTime.Now ).SingleOrDefault();
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
        public static void CreateSubscription(SubscriptionData subscriptionData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(subscriptionData);
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
        public static void SaveSubscription(SubscriptionData subscriptionData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Refresh(subscriptionData);
                        session.Update(subscriptionData);
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
