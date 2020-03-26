using Kurkku.Util;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

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
            log.Info("Attempting to connect to MySQL database");

            try
            {

            }
            catch
            {

            }

#if DEBUG
            Console.Read();
#endif

        }
    }
}
