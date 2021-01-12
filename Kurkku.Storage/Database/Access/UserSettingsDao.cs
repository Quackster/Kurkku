using Kurkku.Storage.Database.Data;
using NHibernate;
using System;

namespace Kurkku.Storage.Database.Access
{
    public class UserSettingsDao
    {
        /// <summary>
        /// Create player statistics
        /// </summary>
        public static void CreateOrUpdate(out PlayerSettingsData settingsData, int userId)
        {
            settingsData = new PlayerSettingsData
            {
                UserId = userId
            };

            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                bool rowExists = session.QueryOver<PlayerSettingsData>().Where(x => x.UserId == userId).RowCount() > 0;

                if (!rowExists)
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        try
                        {
                            session.SaveOrUpdate(settingsData);
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                        }
                    }
                } 
                else
                {
                    settingsData = session.QueryOver<PlayerSettingsData>().Where(x => x.UserId == userId).SingleOrDefault();
                }
            }
        }

        /// <summary>
        /// Save player statistics
        /// </summary>
        public static void Update(PlayerSettingsData settingsData)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(settingsData);
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
