using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.AdoNet;
using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Kurkku.Storage.Database
{
    public class SessionFactoryBuilder
    {
        #region Fields

        private static readonly SessionFactoryBuilder m_SessionFactoryBuilder = new SessionFactoryBuilder();
        private ISessionFactory m_SessionFactory;

        #endregion

        #region Properties

        public static SessionFactoryBuilder Instance
        {
            get { return m_SessionFactoryBuilder; }
        }

        public ISessionFactory SessionFactory
        {
            get { return m_SessionFactory; }
        }

        #endregion

        #region Private fields

        private ISessionFactory BuildSessionFactory(string connectionString)
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                /*.ExposeConfiguration(c => c.DataBaseIntegration(prop => // https://stackoverflow.com/questions/28721221/fluent-nhibernate-cant-set-the-batch-size
                {
                    prop.BatchSize = 50;
                    prop.Batcher<MySqlClientBatchingBatcherFactory>();

                }))*/
                .BuildSessionFactory();
        }

        #endregion

        #region Public methods

        public void InitialiseSessionFactory(string connectionString) 
        {
            if (m_SessionFactory == null)
                m_SessionFactory = BuildSessionFactory(connectionString);
        }

        #endregion
    }
}
