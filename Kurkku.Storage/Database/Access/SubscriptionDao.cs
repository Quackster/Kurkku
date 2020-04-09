
using Kurkku.Storage.Database.Data;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;
using System;
using System.Collections.Generic;

namespace Kurkku.Storage.Database.Access
{
    public class SubscriptionDao
    {

        public static bool ContainsSubscription(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                SubscriptionData subscriptionDataAlias = null;

                return session.QueryOver(() => subscriptionDataAlias)
                    .Where(() => subscriptionDataAlias.UserId == userId)
                    .And(() => DateTime.Now > subscriptionDataAlias.ExpireDate)
                    .List().Count > 0;
            }
        }

        public static SubscriptionData GetSubscription(int userId)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                SubscriptionData subscriptionDataAlias = null;

                return session.QueryOver(() => subscriptionDataAlias)
                    .Where(() => subscriptionDataAlias.UserId == userId)
                    .And(() => DateTime.Now > subscriptionDataAlias.ExpireDate).SingleOrDefault();
            }
        }

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
