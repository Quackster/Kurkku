
using Kurkku.Storage.Database.Data;
using System;

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
    }
}
