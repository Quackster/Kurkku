using Kurkku.Util;
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
using System.Threading;

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
            InteractionManager.Instance.Load();
            ItemManager.Instance.Load();
            CatalogueManager.Instance.Load();
            RoomManager.Instance.Load();
            NavigatorManager.Instance.Load();
            MessageHandler.Instance.Load();

            /*int defId = 2300;
            foreach (int fx in "1,10,11,12,13,14,15,16,17,18,19,2,20,21,22,23,24,25,26,27,28,29,3,4,5,6,7,8,9,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140".ToIntArray(','))
            {
                if (fx > 26)
                {
                    if (fx != 28 && fx != 29 && fx != 30 && fx != 33 && fx != 34 && fx != 35 && fx != 36 && fx != 37 && fx != 38 && fx != 39 && fx != 40 && fx != 41 && fx != 42 && fx != 43
                        && fx != 45 && fx != 46 && fx != 49 && fx != 50 && fx != 51 && fx != 52 && fx != 55 && fx != 56 && fx != 57 && fx != 58 && fx != 68 && fx != 77 && fx != 65 && fx != 96 && fx != 97 && fx != 98
                         && fx != 101 && fx != 102 && fx != 103 && fx != 95) {
                        defId++;
                        Console.WriteLine($"INSERT INTO `catalogue_items` (`sale_code`, `page_id`, `order_id`, `price_coins`, `price_pixels`, `hidden`, `amount`, `definition_id`, `item_specialspriteid`, `is_package`) VALUES ('avatar_effect{fx}', '60', 1, 0, 89, 0, 1, {defId}, '{fx}', 0);");
                        Console.WriteLine($"INSERT INTO `item_definitions` (`id`, `sprite`, `name`, `description`, `sprite_id`, `length`, `width`, `top_height`, `max_status`, `behaviour`, `interactor`, `is_tradable`, `is_recyclable`, `drink_ids`, `rental_time`) VALUES ({defId}, 'avatar_effect{fx}', NULL, NULL, {fx}, 0, 0, 0, '0', 'effect,requires_rights_for_interaction', 'default', 1, 1, '', -1);");
                    }
                }
            }*/

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

            while (true)
            {
                Thread.Sleep(100);
            }
        }

        public class Game
        {
        }

        #endregion
    }
}
