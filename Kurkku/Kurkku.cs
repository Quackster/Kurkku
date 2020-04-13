﻿using Kurkku.Util;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using Kurkku.Storage.Database;
using Kurkku.Network;
using Kurkku.Storage.Database.Access;
using Kurkku.Game;
using Kurkku.Messages;

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

        /// <summary>
        /// Get the official release supported
        /// </summary>
        public static string ClientVersion
        {
            get { return "RELEASE63-201302211227-193109692"; }
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
                tryDatabaseConnection();
                tryGameData();
                tryCreateServer();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

#if DEBUG
            Console.Read();
#endif

        }

        #region Private methods

        /// <summary>
        /// Test database connection
        /// </summary>
        private static void tryDatabaseConnection()
        {
            log.Warn("Attempting to connect to MySQL database");
            SessionFactoryBuilder.Instance.InitialiseSessionFactory(ServerConfig.Instance.ConnectionString);
            log.Info("Connection using Fluid NHibernate is successful!");
        }

        /// <summary>
        /// Load game data
        /// </summary>
        private static void tryGameData()
        {
            ValueManager.Instance.Load();
            RoomManager.Instance.Load();
            NavigatorManager.Instance.Load();
            MessageHandler.Instance.Load();

            RoomDao.ResetVisitorCounts();
        }

        /// <summary>
        /// Try and create server
        /// </summary>
        private static void tryCreateServer()
        {
            GameServer.Logger.Warn("Starting server");

            GameServer.Instance.CreateServer(ServerConfig.Instance.GetString("server", "ip"), ServerConfig.Instance.GetInt("server", "port"));
            GameServer.Instance.InitialiseServer();

            GameServer.Logger.Info($"Server is now listening on port: {GameServer.Instance.IpAddress}:{GameServer.Instance.Port}!");
        }

        public class Game
        {
        }

        #endregion
    }
}
