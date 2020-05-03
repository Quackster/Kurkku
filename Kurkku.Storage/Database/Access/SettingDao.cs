using Kurkku.Storage.Database.Data;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Storage.Database.Access
{
    public class SettingDao
    {
        public static void GetSettings(out Dictionary<string, string> settings)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                settings = session.QueryOver<SettingsData>().List().ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public static bool HasSetting(string key)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<SettingsData>().Where(x => x.Key == key).List().Count > 0;
            }
        }

        public static void SaveSetting(string key, string value)
        {
            using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(new SettingsData { Key = key, Value = value });
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
