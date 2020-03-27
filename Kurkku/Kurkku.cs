using Kurkku.Util;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;
using Kurkku.Storage.Database;
using Kurkku.Storage.Database.Models;

namespace Kurkku
{
    class Kurkku
    {
        #region Fields

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Properties

        /// <summary>
        /// Get the logger instance.
        /// </summary>
        public static ILog Logger
        {
            get { return log; }
        }

        #endregion

        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            Console.Title = "Kurkku - Habbo Hotel Emulation";

            log.Info("Booting Kurkku - Written by Quackster");
            log.Info("Emulation of Habbo Hotel 2013 flash client");

            try
            {
                log.Warn("Attempting to connect to MySQL database");
                SessionFactoryBuilder.Instance.InitialiseSessionFactory(ServerConfig.Instance.ConnectionString);
                log.Info("Connection using Fluid NHibernate is successful!");

                using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
                {
                    var tasks = session.CreateCriteria(typeof(Test)).List<Test>();

                    foreach (var task in tasks)
                    {
                        Console.WriteLine(task.TestId);
                        Console.WriteLine(task.User);
                        Console.WriteLine(task.Room != null ? task.Room.Name : "null");
                        Console.WriteLine("--");
                    }

                    /*
                    using (var transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(new Test
                        {
                            TestId = "14",
                            User = "LOL"
                        });
                        session.SaveOrUpdate(new Test
                        {
                            TestId = "15",
                            User = "LOL"
                        });
                        transaction.Commit();
                    }

                    using (var transaction = session.BeginTransaction())
                    {
                        var t = session.QueryOver<Test>().Where(test => test.TestId == "loled").SingleOrDefault();

                        session.Delete(t);
                        transaction.Commit();
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

#if DEBUG
            Console.Read();
#endif

        }
    }
}
